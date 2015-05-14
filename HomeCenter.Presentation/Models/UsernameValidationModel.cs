using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TypeLite;

namespace HomeCenter.Models
{
        [TsClass(Name = "IUsernameValidationModel", Module = "HomeCenter")]
        public class UsernameValidationModel
        {
            public string Message { get; set; }
            public bool IsCorrect { get; set; }
        }
}