using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCenter.AuthenticationDomain.Model.DomainLayer.Enums
{
    public enum AuthenticationRoles
    {
        [Description("Administrator")]
        Administrator,
        [Description("User")]
        User,
        [Description("Builder")]
        Builder,
        [Description("Scheduler")]
        Scheduler,
        [Description("AccountManager")]
        AccountManager
    }
}
