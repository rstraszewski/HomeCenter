using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace HomeCenter.CommonDomain
{
    public class DomainEvents : IDomainEvents
    {
        //private static IUnityContainer _container = new UnityContainer(); //as before

       /* public static void Initialise(IUnityContainer container)
        {
            _container = container;
        }*/

        private readonly IUnityContainer _container;
        public DomainEvents(IUnityContainer container)
        {
            _container = container;
        }

        public void Raise<T>(T args) where T : IDomainEvent
        {
            Debug.WriteLine("Raise.");
            if (_container != null)
            {
                foreach (var handler in _container.ResolveAll<IHandles<T>>())
                {
                    handler.Handle(args);
                }
            }
        }

    }

    public interface IDomainEvents
    {
        void Raise<T>(T args) where T : IDomainEvent;
    }
}
