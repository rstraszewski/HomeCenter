﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCenter.CommonDomain
{
    public interface IGenericEntity<T>
    {
        T Id { get; set; }
    }
}
