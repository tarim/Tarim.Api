

using Microsoft.Extensions.Logging;

namespace Karluks.API.Infrastructure.DataProvider
{
    public abstract class Base
    {
        protected readonly ILogger<Base> Log;
        protected Base(ILogger<Base> log)
        {
            
            Log = log;
        }

    }
}
