using System;
namespace Tarim.Api.Infrastructure.Common.Enums
{
    public enum SexType
    {
        Male=0,
        Female=1,
        Both=2
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
}
