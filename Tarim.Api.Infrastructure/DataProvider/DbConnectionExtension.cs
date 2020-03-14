using System;
using System.Collections.Generic;
using System.Data;
using Tarim.Api.Infrastructure.Common.Enums;
using Tarim.Api.Infrastructure.Model.names;

namespace Tarim.Api.Infrastructure.DataProvider
{
    public static partial class DbConnectionExtension
    {
        public static void Read(this IList<UyghurName> objList, IDataReader rdReader)
        {
            while (rdReader.Read())
            {
                var obj = new UyghurName();
                obj.ReadAll(rdReader);
                objList.Add(obj);
            }

        }
        private static void ReadAll(this UyghurName obj, IDataReader rdReader)
        {
            obj.Id = rdReader.GetInt("id");
            obj.Name = rdReader.GetString("name");
            obj.Origin = rdReader.GetString("origin");
            obj.Sex = rdReader.GetEnum<SexType>("sex");
            obj.IsFamilyName = rdReader.GetBoolean("family_name");
            obj.Description = rdReader.GetString("description");
            
        }
    }
}
