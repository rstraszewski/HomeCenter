///<reference path="../../../Scripts/typings/requirejs/require.d.ts" />
///<reference path="../../../Scripts/typings/jqueryui/jqueryui.d.ts" />
///<reference path="../../../Scripts/TypeLite.Net4.d.ts" /> 
///<reference path="../../../Scripts/Enums.ts" /> 
///<reference path="../../Shared/Common.ts" />
///<reference path="../../Services/DeviceService.ts" />


module HomeCenter {
    export interface IDeviceControlController extends Common.IScope {
        buildingList: IBuilding[];
        deviceList: IDevice[];
        sliderConfig;
        slideValue: number;
        promise: ng.IPromise<any>
    }

    export class DeviceControlController extends Common.Controller {
        scope: IDeviceControlController;
        deviceService: DeviceService;
        deviceUpdateInterval: ng.IPromise<ng.IIntervalService>;
        interval: ng.IIntervalService;
        slidingFlag: boolean;
        webSocket: WebSocket;
        static $inject = ['$scope', 'deviceService', '$interval'];

        constructor($scope: IMainControllerScope, deviceService: DeviceService, $interval: ng.IIntervalService) {
            super($scope);

            this.interval = $interval;
            this.deviceService = deviceService;
            this.getConstructorAjax();
            this.initiateDevice();
            this.slidingFlag = false;

            //this.webSocket = new WebSocket("ws://156.17.230.169:4649/WebSite");
            this.webSocket = new WebSocket("ws://" + window.location.hostname + ":4649/WebSite");
           
            this.webSocket.onclose = () => {
                this.startDeviceInterval();
                this.webSocket = new WebSocket("ws://localhost:4649/WebSite");
            };
            this.webSocket.onmessage = (event) => {
                //alert(event.data);
                this.refreshDevice(JSON.parse(event.data));
            };
            this.startDeviceInterval();
        }

        refreshDevice(device: IDevice) {
            var tempArray = this.scope.deviceList;
            
            this.scope.deviceList.forEach((value: IDevice, index) => {
                if (device.Id == value.Id) {
                    //alert(device.Name);
                    //tempArray[index] = device;
                    console.log(device.Id + " " + device.DeviceState);
                    value.DeviceState = device.DeviceState;
                    //tempArray[index].DeviceState = device.DeviceState;
                }
            });
            //this.scope.deviceList = tempArray;
            this.scope.$apply();
        }

        getConstructorAjax(): void {
            this.scope.promise = this.deviceService.getBuildings().success(result => this.scope.buildingList = result);
        }

        initiateDevice() {
            this.scope.promise = this.deviceService.getDevicesForUI().success(result => {
                    this.scope.deviceList = result;
            });
        }

        startDeviceInterval() {
            this.deviceService.getDevicesForUI().success(result => {
                if (this.slidingFlag == false) {
                    this.scope.deviceList = result;

                }
                if (this.webSocket.readyState != 1)
                    setTimeout(() => this.startDeviceInterval(), 3000);
            });

        }

        getDevicesInRoom(room: IRoom): IDevice[] {
            var devicesInRoom: IDevice[] = [];
            if (this.scope.deviceList)
                this.scope.deviceList.forEach((device) => (device.RoomId == room.Id) ? devicesInRoom.push(device) : {});
            return devicesInRoom;
        }

        switchDeviceState(device: IDevice) {
            if (device.DeviceState > 0) {
                this.deviceService.updateDeviceState(device.Id, device.MinValue);
                device.DeviceState = device.MinValue;
            } else {
                this.deviceService.updateDeviceState(device.Id, device.MaxValue);
                device.DeviceState = device.MaxValue;
            }
        }

        stop(device: IDevice): void {
            this.deviceService.updateDeviceState(device.Id, device.DeviceState).success(() => {
                //this.startDeviceInterval();
            });
            //this.startDeviceInterval();
            this.slidingFlag = false;
        }

        start(): void {
            this.slidingFlag = true;
            //this.interval.cancel(this.deviceUpdateInterval);
        }

        roomContainsDevice(roomId: number): any {
            var isDeviceInRoom: boolean = false;
            if (this.scope.deviceList) {
                this.scope.deviceList.forEach((device) => {
                    if (device.RoomId == roomId) {
                        isDeviceInRoom = true;
                    }

                });
            }
            return isDeviceInRoom;
        }
    }
}