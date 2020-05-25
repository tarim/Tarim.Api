// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NameRepository.cs" company="Tarim Lab">
//   Copyright (c) Tarim Lab. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Tarim.Api.Infrastructure.Common;
using Tarim.Api.Infrastructure.Common.Enums;
using Tarim.Api.Infrastructure.DataProvider;
using Tarim.Api.Infrastructure.Interface;
using Tarim.Api.Infrastructure.Model.Name;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;

namespace Tarim.Api.Infrastructure.Service
{
    public class NameRepository : BaseRepository, INameRepository
    {
        private readonly ILogger<NameRepository> logger;
        public NameRepository(IConnection connection,ILogger<NameRepository> log) : base(connection)
        {
            logger = log;
        }

        public async Task<Result<IList<UyghurName>>> GetUyghurName(int pageNumber)
        {
            var uyghurNames = new Result<IList<UyghurName>> { Object = new List<UyghurName>() };
            await GetResultAsync("GET_NAME_LIST",
               
                rdReader =>
                {
                    uyghurNames.Object.Read(rdReader);
                    return uyghurNames;
                },
                 GetParameter("pageNumber_in", pageNumber, MySqlDbType.Int32));
            return uyghurNames;
            
        }

        public async Task<Result<UyghurName>> GetUyghurName(string name)
        {
            
                var uyghurName = new Result<UyghurName> { Object = new UyghurName() };
                await GetResultAsync("GET_NAME",
                    rdReader =>
                    {
                        uyghurName.Object.Read(rdReader);
                        return uyghurName;
                    },
                    GetParameter("name_latin_in", name, MySqlDbType.VarChar));
                return uyghurName;
           
        }

        public async Task<Result<UyghurName>> UpdateUyghurName(UyghurName uyghurName)
        {
            var result = new Result<UyghurName> { Object = uyghurName, Status = ExecuteStatus.Error };
            await ExecuteNonQueryAsync("UPDATE_NAME",
                GetParameter("id_in", uyghurName.Id, MySqlDbType.Int32),
                GetParameter("name_ug_in", uyghurName.NameUg, MySqlDbType.VarChar),
                GetParameter("name_latin_in", uyghurName.NameLatin, MySqlDbType.VarChar),
                GetParameter("origin_in", uyghurName.Origin, MySqlDbType.Enum),
                GetParameter("gender_in", uyghurName.Gender, MySqlDbType.Enum),
                GetParameter("related_name_in", uyghurName.RelatedName, MySqlDbType.VarChar),
                GetParameter("is_surname_in", uyghurName.IsFamilyName, MySqlDbType.Byte),
                GetParameter("description_in", uyghurName.Description, MySqlDbType.VarChar)
            );
            result.Status = ExecuteStatus.Success;
            return result;
        }

      public async Task<Result<UyghurName>> AddUyghurName(UyghurName uyghurName)
        {
            var result = new Result<UyghurName> { Object = uyghurName };
            var insertId = GetParameter("id_out",MySqlDbType.Int32,10);
            await ExecuteNonQueryAsync("ADD_NAME",
                GetParameter("name_ug_in",uyghurName.NameUg,MySqlDbType.VarChar),
                GetParameter("name_latin_in", uyghurName.NameLatin, MySqlDbType.VarChar),
                GetParameter("origin_in", uyghurName.Origin, MySqlDbType.Enum),
                GetParameter("gender_in", uyghurName.Gender, MySqlDbType.Enum),
                GetParameter("related_name_in", uyghurName.RelatedName, MySqlDbType.VarChar),
                GetParameter("is_surname_in", uyghurName.IsFamilyName, MySqlDbType.Byte),
                GetParameter("description_in", uyghurName.Description, MySqlDbType.VarChar),
                insertId
            );
            result.Object.Id =Convert.ToInt32(insertId.Value);
            result.Status = result.Object.Id > 0 ? ExecuteStatus.Success : ExecuteStatus.Failed; 
            return result;
        }

        public async Task<Result<int>> DeleteUyghurName(int id)
        {
            var result = new Result<int> { Object = id, Status = ExecuteStatus.Error };
            await ExecuteNonQueryAsync("DELETE_NAME",
                GetParameter("id_in", id, MySqlDbType.Int32)
            );
            result.Status = ExecuteStatus.Success;
            return result;
        }

        public async Task<Result<NameAction>> AddNameAction(NameAction name)
        {
            var result = new Result<NameAction> { Object = name };
            var insertId = GetParameter("id_out", MySqlDbType.Int32, 10);
            await ExecuteNonQueryAsync("ADD_NAME_ACTION",
                GetParameter("name_recid_in", name.NameId, MySqlDbType.Int32),
                GetParameter("action_in", name.ActionType, MySqlDbType.Enum),
                GetParameter("user_ip_in", name.UserIp, MySqlDbType.VarChar),
                insertId
            );
            
            result.Object.Id = Convert.ToInt32(insertId.Value);
            result.Status = result.Object.Id > 0 ? ExecuteStatus.Success : ExecuteStatus.Failed;
            return result;
        }

        public async Task<Result<IList<TopName>>> GetTopNames()
        {
            var result = new Result<IList<TopName>> { Object = new List<TopName>() };
            await GetResultAsync("TOP_10_NAMES",
                rdReader =>
                {
                    result.Object.Read(rdReader);
                    return result;
                });
            return result;
        }

        public async Task<Result<NameGenderCount>> GetNameStatistics()
        {
            var result = new Result<NameGenderCount> { Object = new NameGenderCount() };
            await GetResultAsync("GET_NAME_STATISTICS",
                rdReader =>
                {
                    result.Object.Read(rdReader);
                    return result;
                });
            return result;
        }
    }
}
