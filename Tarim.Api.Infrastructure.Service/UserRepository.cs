﻿using System;
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

        public async Task<Result<User>> FindUser(string userEmail)
        {
            var user = new Result<User> { Object = new User() };
            await GetResultAsync("GET_USER",
                rdReader =>
                {
                    user.Object.Read(rdReader);
                    if (user.Object.Id > 0) user.Status = Common.Enums.ExecuteStatus.Success;
                    return user;
                },
                GetParameter("email_in", userEmail, MySql.Data.MySqlClient.MySqlDbType.VarChar));

            return user;
        }


    }
}
