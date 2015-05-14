using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeCenter.CommonDomain;

namespace HomeCenter.CommonDomain
{
    public interface IHandles<T> where T : IDomainEvent
    {
        void Handle(T args);
    }
}
