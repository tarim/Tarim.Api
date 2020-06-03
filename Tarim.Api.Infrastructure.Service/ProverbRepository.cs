// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProverbsRepository.cs" company="Tarim Lab">
//   Copyright (c) Tarim Lab. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Tarim.Api.Infrastructure.Common;
using Tarim.Api.Infrastructure.Common.Enums;
using Tarim.Api.Infrastructure.DataProvider;
using Tarim.Api.Infrastructure.Interface;
using Tarim.Api.Infrastructure.Model.Proverbs;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;

namespace Tarim.Api.Infrastructure.Service
{
    public class ProverbRepository : BaseRepository, IProverbRepository
    {
        private readonly ILogger<ProverbRepository> logger;
        public ProverbRepository(IConnection connection,ILogger<ProverbRepository> log) : base(connection)
        {
            logger = log;
        }

        public async Task<Result<IList<Proverb>>> GetProverbs(int pageNumber)
        {
            var result = new Result<IList<Proverb>> { Object = new List<Proverb>() };
            await GetResultAsync("GET_PROVERBS_LIST",
               
                rdReader =>
                {
                    result.Object.Read(rdReader);
                    return result;
                },
                 GetParameter("pageNumber_in", pageNumber, MySqlDbType.Int32));
            return result;
            
        }

         public async Task<Result<IList<Proverb>>> GetDailyProverb(int pageSize)
        {
            var result = new Result<IList<Proverb>> { Object = new List<Proverb>() };
            await GetResultAsync("GET_DAILY_PROVERB",
               
                rdReader =>
                {
                    result.Object.Read(rdReader);
                    return result;
                },
                 GetParameter("size_in", pageSize, MySqlDbType.Int32));
            return result;
            
        }

        public async Task<Result<Proverb>> GetProverb(int proverbId)
        {
            
                var result = new Result<Proverb> { Object = new Proverb() };
                await GetResultAsync("GET_PROVERB",
                    rdReader =>
                    {
                        result.Object.Read(rdReader);
                        return result;
                    },
                    GetParameter("proverb_recid_in", proverbId, MySqlDbType.Int32));
                return result;
           
        }

        public async Task<Result<Proverb>> UpdateProverb(Proverb proverb)
        {
            var result = new Result<Proverb> { Object = proverb, Status = ExecuteStatus.Error };
            await ExecuteNonQueryAsync("UPDATE_PROVERB",
                GetParameter("id_in", proverb.Id, MySqlDbType.Int32),
                GetParameter("content_in", proverb.Content, MySqlDbType.VarChar),
                GetParameter("updated_by_in", proverb.UserRecid, MySqlDbType.Int32)
            );
            result.Status = ExecuteStatus.Success;
            return result;
        }

      public async Task<Result<Proverb>> AddProverb(Proverb proverb)
        {
            var result = new Result<Proverb> { Object = proverb };
            var insertId = GetParameter("id_out",MySqlDbType.Int32,10);
            await ExecuteNonQueryAsync("ADD_PROVERB",
                GetParameter("content_in",proverb.Content,MySqlDbType.VarChar),
                GetParameter("description_in", proverb.Description, MySqlDbType.VarChar),
                GetParameter("user_recid_in", proverb.UserRecid, MySqlDbType.Int32),
                insertId
            );
            result.Object.Id =Convert.ToInt32(insertId.Value);
            result.Status = result.Object.Id > 0 ? ExecuteStatus.Success : ExecuteStatus.Failed; 
            return result;
        }

        public async Task<Result<int>> DeleteProverb(int id)
        {
            var result = new Result<int> { Object = id, Status = ExecuteStatus.Error };
            await ExecuteNonQueryAsync("DELETE_PROVERB",
                GetParameter("id_in", id, MySqlDbType.Int32)
            );
            result.Status = ExecuteStatus.Success;
            return result;
        }

     
    }
}
