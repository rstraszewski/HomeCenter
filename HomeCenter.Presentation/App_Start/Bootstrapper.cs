using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Web.Mvc;
using HomeCenter.AuthenticationDomain;
using HomeCenter.Communication;
using HomeCenter.DeviceDomain;
using HomeCenter.DeviceDomain.DomainLayer.RepositoryInterface;
using HomeCenter.DeviceDomain.Service.ServiceLayer;
using HomeCenter.HistoryDomain;
using HomeCenter.RepositoryLayer;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Quartz;
using Quartz.Unity;
using Unity.Mvc4;
using System.Web;
using HomeCenter.Database;
using HomeCenter.CommonDomain;

namespace HomeCenter
{
    public static class Bootstrapper
    {
        public static IUnityContainer Container { get; set; }
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            Container = container;
            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();//.AddNewExtension<Quartz.Unity.QuartzUnityExtension>();
            
            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IDeviceService, DeviceService>();
            container.RegisterType<IDeviceTypeControlService, DeviceTypeControlService>();
            container.RegisterType<IBuildingControlService, BuildingControlService>();
            container.RegisterType<ICreateControlUiService, CreateControlUiService>();
            container.RegisterType<IDeviceRepository, DeviceRepository>();
            container.RegisterType<IBuildingRepository, BuildingRepository>();
            container.RegisterType<IGroupRepository, GroupRepository>();
            container.RegisterType<IDeviceTypeRepository, DeviceTypeRepository>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IAuthenticationService, AuthenticationService>();
            container.RegisterType<IDomainEvents, DomainEvents>();
            container.RegisterType<IDomainLoggerService, DomainLoggerService>();
            container.RegisterType<IHistoryLogRepository, HistoryLogRepository>();
            container.RegisterType<IDeviceRelationRepository, DeviceRelationRepository>();
            //container.RegisterType<ISerialPortConnection, SerialPortConnection>(new ContainerControlledLifetimeManager());
            //container.RegisterType<IWebSocket, WebSocket>(new ContainerControlledLifetimeManager());
            container.RegisterType<HomeCenterDb>(new PerRequestLifetimeManager());
            container.RegisterType<ISerialPortConnection, SerialPortConnection>();
            container.RegisterType<IWebSocket, WebSocket>();
            container.RegisterType<IDbContext, HomeCenterDb>(new InjectionConstructor());
            container.RegisterType<IHandles<DeviceStateHasChangedFromUi>, DeviceStateHasChangedFromUIHandler>("DeviceStateHasChangedFromUi");
            container.RegisterType<IHandles<DeviceStateHasChangedFromSerialPort>, DeviceStateHasChangedFromSerialPortHandler>("DeviceStateHasChangedFromSerialPort");
            container.RegisterType<IHandles<DeviceStateHasChanged>, DeviceStateHasChangedHandler>("DeviceStateHasChanged");
            container.RegisterType<IHandles<StartDeviceJob>, StartDeviceJobHandler>("StartDeviceJob");
            container.RegisterType<ISchedulerFactory, UnitySchedulerFactory>(new HomeCenter.ContainerControlledLifetimeManager());
            container.RegisterType<IScheduler>(new InjectionFactory(c => c.Resolve<ISchedulerFactory>().GetScheduler()));
            container.RegisterType<IJobScheduler, JobScheduler>(new HomeCenter.ContainerControlledLifetimeManager());
            container.RegisterType<IDeviceJobRepository, DeviceJobRepository>();
            container.RegisterType<IAddressRepository, AddressRepository>();
        }


    }

    public abstract class SynchronizedLifetimeManager : LifetimeManager, IRequiresRecovery
    {
        private object lockObj = new object();
        private volatile bool hasMonitorEntered;
        /// <summary>
        /// Retrieve a value from the backing store associated with this Lifetime policy.
        /// </summary>
        /// <returns>the object desired, or null if no such object is currently stored.</returns>
        /// <remarks>Calls to this method acquire a lock which is released only if a non-null value
        /// has been set for the lifetime manager.</remarks>
        public override object GetValue()
        {
            lock (lockObj)
                
            Monitor.Enter(lockObj);
            hasMonitorEntered = true;//inside of synchronised section

            object result = SynchronizedGetValue();
            if (result != null)
            {
                Monitor.Exit(lockObj);
            }
            return result;
        }

        /// <summary>
        /// Performs the actual retrieval of a value from the backing store associated
        /// with this Lifetime policy.
        /// </summary>
        /// <returns>the object desired, or null if no such object is currently stored.</returns>
        /// <remarks>This method is invoked by <see cref="SynchronizedLifetimeManager.GetValue"/>
        /// after it has acquired its lock.</remarks>
        protected abstract object SynchronizedGetValue();

        /// <summary>
        /// Stores the given value into backing store for retrieval later.
        /// </summary>
        /// <param name="newValue">The object being stored.</param>
        /// <remarks>Setting a value will attempt to release the lock acquired by
        /// <see cref="SynchronizedLifetimeManager.GetValue"/>.</remarks>
        public override void SetValue(object newValue)
        {
            SynchronizedSetValue(newValue);
            TryExit();
        }

        /// <summary>
        /// Performs the actual storage of the given value into backing store for retrieval later.
        /// </summary>
        /// <param name="newValue">The object being stored.</param>
        /// <remarks>This method is invoked by <see cref="SynchronizedLifetimeManager.SetValue"/>
        /// before releasing its lock.</remarks>
        protected abstract void SynchronizedSetValue(object newValue);

        /// <summary>
        /// Remove the given object from backing store.
        /// </summary>
        public override void RemoveValue()
        {
        }

        /// <summary>
        /// A method that does whatever is needed to clean up
        /// as part of cleaning up after an exception.
        /// </summary>
        /// <remarks>
        /// Don't do anything that could throw in this method,
        /// it will cause later recover operations to get skipped
        /// and play real havok with the stack trace.
        /// </remarks>
        public void Recover()
        {
            TryExit();
        }

        private void TryExit()
        {
            if (hasMonitorEntered)//inside of synchronised section if true, therefore thread safe
            {
                hasMonitorEntered = false;
                Monitor.Exit(lockObj);
            }
        }
    }


    public class PerRequestLifetimeManager : LifetimeManager
    {
        private readonly object _key = new object();

        public override object GetValue()
        {
            if (HttpContext.Current != null &&
                HttpContext.Current.Items.Contains(_key))
                return HttpContext.Current.Items[_key];
            else
                return null;
        }

        public override void RemoveValue()
        {
            if (HttpContext.Current != null)
                HttpContext.Current.Items.Remove(_key);
        }

        public override void SetValue(object newValue)
        {
            if (HttpContext.Current != null)
                HttpContext.Current.Items[_key] = newValue;
        }
    }

    public class ContainerControlledLifetimeManager : HomeCenter.SynchronizedLifetimeManager, IDisposable
    {
        private object value;

        /// <summary>
        /// Retrieve a value from the backing store associated with this Lifetime policy.
        /// </summary>
        /// <returns>the object desired, or null if no such object is currently stored.</returns>
        protected override object SynchronizedGetValue()
        {
            return this.value;
        }

        /// <summary>
        /// Stores the given value into backing store for retrieval later.
        /// </summary>
        /// <param name="newValue">The object being stored.</param>
        protected override void SynchronizedSetValue(object newValue)
        {
            this.value = newValue;
        }

        /// <summary>
        /// Remove the given object from backing store.
        /// </summary>
        public override void RemoveValue()
        {
            this.Dispose();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this); // shut FxCop up
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.value != null)
            {
                if (this.value is IDisposable)
                {
                    ((IDisposable)this.value).Dispose();
                }
                this.value = null;
            }
        }
    }
}