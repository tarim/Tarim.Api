// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TipsRepository.cs" company="Tarim Lab">
//   Copyright (c) Tarim Lab. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Tarim.Api.Infrastructure.Common;
using Tarim.Api.Infrastructure.Common.Enums;
using Tarim.Api.Infrastructure.DataProvider;
using Tarim.Api.Infrastructure.Interface;
using Tarim.Api.Infrastructure.Model.Tips;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;

namespace Tarim.Api.Infrastructure.Service
{
    public class TipsRepository : BaseRepository, ITipsRepository
    {
        private readonly ILogger<TipsRepository> logger;
        public TipsRepository(IConnection connection,ILogger<TipsRepository> log) : base(connection)
        {
            logger = log;
        }

        public async Task<Result<IList<Tip>>> GetTips()
        {
            var result = new Result<IList<Tip>> { Object = new List<Tip>() };
            await GetResultAsync("GET_ALL_TIPS",
               
                rdReader =>
                {
                    result.Object.Read(rdReader);
                    return result;
                });
            return result;
            
        }

        public async Task<Result<IList<Tip>>> GetTips(int pageNumber)
        {
            var result = new Result<IList<Tip>> { Object = new List<Tip>() };
            await GetResultAsync("GET_TIPS_LIST",
               
                rdReader =>
                {
                    result.Object.Read(rdReader);
                    return result;
                },
                 GetParameter("pageNumber_in", pageNumber, MySqlDbType.Int32));
            return result;
            
        }

        public async Task<Result<Tip>> GetTip(int tipId)
        {
            
                var result = new Result<Tip> { Object = new Tip() };
                await GetResultAsync("GET_TIP",
                    rdReader =>
                    {
                        result.Object.Read(rdReader);
                        return result;
                    },
                    GetParameter("tip_recid_in", tipId, MySqlDbType.Int32));
                return result;
           
        }

        public async Task<Result<Tip>> UpdateTip(Tip tip)
        {
            var result = new Result<Tip> { Object = tip, Status = ExecuteStatus.Error };
            await ExecuteNonQueryAsync("UPDATE_TIP",
                GetParameter("id_in", tip.Id, MySqlDbType.Int32),
                GetParameter("title_in", tip.Title, MySqlDbType.VarChar),
                GetParameter("summary_in", tip.Summary, MySqlDbType.VarChar),
                GetParameter("category_in", tip.Category, MySqlDbType.Enum),
                GetParameter("content_in", tip.Content, MySqlDbType.Text),
                GetParameter("source_in", tip.Source, MySqlDbType.VarChar),
                GetParameter("privacy_in", tip.Private, MySqlDbType.Byte),
                GetParameter("updated_by_in", tip.UserRecid, MySqlDbType.Int32)
            );
            result.Status = ExecuteStatus.Success;
            return result;
        }

      public async Task<Result<Tip>> AddTip(Tip tip)
        {
            var result = new Result<Tip> { Object = tip };
            var insertId = GetParameter("id_out",MySqlDbType.Int32,10);
            await ExecuteNonQueryAsync("ADD_TIP",
                GetParameter("title_in",tip.Title,MySqlDbType.VarChar),
                GetParameter("summary_in", tip.Summary, MySqlDbType.VarChar),
                GetParameter("category_in", tip.Category, MySqlDbType.Enum),
                GetParameter("content_in", tip.Content, MySqlDbType.Text),
                GetParameter("source_in", tip.Source, MySqlDbType.VarChar),
                GetParameter("user_recid_in", tip.UserRecid, MySqlDbType.Int32),
                insertId
            );
            result.Object.Id =Convert.ToInt32(insertId.Value);
            result.Status = result.Object.Id > 0 ? ExecuteStatus.Success : ExecuteStatus.Failed; 
            return result;
        }

        public async Task<Result<int>> DeleteTip(int id)
        {
            var result = new Result<int> { Object = id, Status = ExecuteStatus.Error };
            await ExecuteNonQueryAsync("DELETE_TIP",
                GetParameter("id_in", id, MySqlDbType.Int32)
            );
            result.Status = ExecuteStatus.Success;
            return result;
        }

     
    }
}
