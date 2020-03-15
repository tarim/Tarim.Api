using System;
namespace Tarim.Api.Infrastructure.Common.Enums
{
    public enum GenderType
    {
        Oghul=0,
        Qiz=1,
        Ortaq=2
    }

    public enum ProfileType
    {
        Basic=0,
        Advanced=1,
        Admin=2,
        Owner=3
    }

    public enum StatusType
    {
        Registered=0,
        Active=1,
        Hold=2,
        Disabled=3,
        Deleted=4
    }

    public enum OriginationType
    {
        Uyghurche = 0,
        Erepche = 1,
        Parische = 2,
        Bashqa = 3
    }
}
