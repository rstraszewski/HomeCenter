using System.ComponentModel;
using TypeLite;

namespace HomeCenter.DeviceDomain
{
    [TsEnum(Name = "StateRelation", Module = "HomeCenter")]
    public enum StateRelation
    {
        [Description("Is greater then")]
        GreaterThen,
        [Description("Is lesser then")]
        LesserThen,
        [Description("Is equal to")]
        EqualTo,
        [Description("Is greater then or equal to")]
        GreaterThenOrEqualTo,
        [Description("Is lesser then or equal to")]
        LesserThenOrEqualto
    }
}