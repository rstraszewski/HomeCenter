using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HomeCenter.CommonDomain;
using HomeCenter.DeviceDomain;
using Microsoft.Practices.ObjectBuilder2;
using ServiceStack.Text;

namespace HomeCenter.Communication
{
    public class SerialPortConnection : ISerialPortConnection
    {

        private readonly IDomainEvents _domainEvents;
        private readonly IDeviceService _deviceService;
        public SerialPortConnection(IDomainEvents domainEvents, IDeviceService deviceService)
        {
            _domainEvents = domainEvents;
            _deviceService = deviceService;
        }


        public static BlockingCollection<DeviceDtoOutput> BlockingCollection { get; set; }
        public SerialPort SerialPort { get; set; }

        public void AddDeviceToUpdate(DeviceDtoOutput device)
        {
            if (BlockingCollection != null)
                BlockingCollection.Add(device);
        }

        public void Initialise()
        {
            BlockingCollection = new BlockingCollection<DeviceDtoOutput>();
            var serialPortName = "";
            try
            {

                var serialPortNames = SerialPort.GetPortNames();
                if (serialPortNames.Length != 0)
                {

                    serialPortName = serialPortNames.FirstOrDefault(s => s.Contains("COM4") || s.Contains("ttyAMA0"));
                    if (serialPortName == null)
                    {
                        Console.WriteLine("SerialPort name is null");
                        return;
                    }
                    Console.WriteLine("Serial port name: " + serialPortName);
                    // Debug.WriteLine("Serial port name: " + serialPortName);
                    SerialPort = new SerialPort(serialPortName, 115200);
                    //Debug.WriteLine("Serial port name: " + serialPortName);
                    Console.WriteLine("Is open: " + SerialPort.IsOpen);
                    if (!SerialPort.IsOpen)
                    {
                        SerialPort.Open();
                        Console.WriteLine("Is open after opening: " + SerialPort.IsOpen);
                        // Debug.WriteLine("Is open after opening: " + SerialPort.IsOpen);
                        Thread.Sleep(500);
                    }
                    //SerialPort.DataReceived += serialPort_DataReceived;
                    var t1 = new Thread(() =>
                    {
                        if (SerialPort.IsOpen)
                        {
                            var devices = _deviceService.GetAllDevices();
                            
                            foreach (var device in devices)
                            {
                                {
                                    try
                                    {

                                    
                                    if (device.Changeable == "True")
                                        SerialPort.WriteLine(string.Format("{0};{1};{2};{3};{4};{5}", device.Address, 0, 1, 0, 24, device.DeviceState));
                                    else if (device.Changeable == "False")
                                        SerialPort.WriteLine(string.Format("{0};{1};{2};{3};{4};{5}", device.Address, 0, 2, 0, 24, ""));
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.InnerException);
                                        Console.WriteLine(e.Message);
                                    }
                                }
                            }

                        }
                    });
                    t1.Start();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                Console.WriteLine(e.Message);
                // Debug.WriteLine(e.InnerException);
                //Debug.WriteLine(e.Message);
            }
            var t = new Thread(() =>
            {
                Console.WriteLine("Start writing thread.");
                foreach (var data in BlockingCollection.GetConsumingEnumerable())
                {
                    Console.WriteLine("Got data to write");
                    try
                    {
                        if (SerialPort != null && SerialPort.IsOpen)
                        {
                            Console.WriteLine("Sending: " + string.Format("{0};{1};{2};{3};{4};{5}", data.Address, 0, 1, 0, 24, data.DeviceState));
                            SerialPort.WriteLine(string.Format("{0};{1};{2};{3};{4};{5}", data.Address, 0, 1, 0, 24, data.DeviceState));
                        }
                           
                    }
                    catch (Exception e)
                    {

                        Console.WriteLine(e.Message);
                        Console.WriteLine(e.InnerException);
                    }

                }

            });
            var t2 = new Thread(serialPort_ReadData);
            t.Start();
            t2.Start();
        }

        void serialPort_ReadData()
        {
            Console.WriteLine("Start reading Thread. Serial port IsOpen: " + SerialPort.IsOpen);
            while (SerialPort.IsOpen)
            {

                if (SerialPort.BytesToRead > 0)
                {
                    Console.WriteLine("Starting reading.");
                    var deviceSerialized = "";
                    Console.WriteLine("Starting reading.");
                    deviceSerialized = SerialPort.ReadLine();

                    Console.WriteLine(deviceSerialized);
                    var device = new DeviceDtoSimple();
                    var msg = deviceSerialized.Split(';');
                    Console.WriteLine("Msg length: " + msg.Length);
                    if (msg.Length == 6)
                    {
                        try
                        {
                            Console.WriteLine("Msg[2]: {0}, msg[4]: {1}", int.Parse(msg[2]),int.Parse(msg[4]));
                            if (int.Parse(msg[2]) == 1 && int.Parse(msg[4]) == 24)
                            {
                                Console.WriteLine("Msg[0]: {0}, msg[5]: {1}", int.Parse(msg[0]), int.Parse(msg[5]));
                                _deviceService.UpdateDeviceStateFromSerialPort(int.Parse(msg[0]), int.Parse(msg[5]));
                            }


                            if (int.Parse(msg[4]) == 3 && int.Parse(msg[2]) == 3)
                            {
                                Console.WriteLine("Msg[4]: {0}, msg[2]: {1}", int.Parse(msg[4]), int.Parse(msg[2]));
                                //_deviceService.UpdateDeviceStateFromSerialPort(int.Parse(msg[0]), int.Parse(msg[5]));
                                var address = _deviceService.GetAddresForPhisicalDevice();
                                SerialPort.WriteLine(string.Format("{0};{1};{2};{3};{4};{5}", 255, 0, 3, 0, 4, address));
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.InnerException);
                            Console.WriteLine(e.Message);
                        }
                    }

                }
                Thread.Sleep(200);
            }
        }
    }

    public interface ISerialPortConnection
    {
        void AddDeviceToUpdate(DeviceDtoOutput device);
        void Initialise();
    }
}
