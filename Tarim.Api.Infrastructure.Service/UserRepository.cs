using System;
using System.Threading.Tasks;
using Tarim.Api.Infrastructure.Common;
using Tarim.Api.Infrastructure.DataProvider;
using Tarim.Api.Infrastructure.Interface;
using Tarim.Api.Infrastructure.Model;
using Microsoft.Extensions.Logging;

namespace Tarim.Api.Infrastructure.Service
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly ILogger<UserRepository> logger;
        public UserRepository(IConnection connection, ILogger<UserRepository> log) : base(connection)
        {
            logger = log;
        }

        public Task<Result<User>> FindUser(string userEmail)
        {
            throw new NotImplementedException();
        }
    }
}
