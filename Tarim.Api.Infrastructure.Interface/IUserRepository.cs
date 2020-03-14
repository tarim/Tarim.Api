using System;
using System.Threading.Tasks;
using Tarim.Api.Infrastructure.Common;
using Tarim.Api.Infrastructure.Model;

namespace Tarim.Api.Infrastructure.Interface
{
    public interface IUserRepository
    {
        Task<Result<User>> FindUser(string userEmail);
    }
}
