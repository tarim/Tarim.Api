using System;
using System.Collections.Generic;
using System.Data;
using Tarim.Api.Infrastructure.Common.Enums;
using Tarim.Api.Infrastructure.Model;
using Tarim.Api.Infrastructure.Model.Name;
using Tarim.Api.Infrastructure.Model.Tips;
using Tarim.Api.Infrastructure.Model.Proverbs;

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

        public static void Read(this UyghurName obj, IDataReader rdReader)
        {
            while (rdReader.Read())
            {  
                obj.ReadAll(rdReader); 
            }

        }

        public static void Read(this NameGenderCount obj, IDataReader rdReader)
        {
            while (rdReader.Read())
            {
                obj.ReadAll(rdReader);
            }

        }

        public static void Read(this IList<TopName> objList, IDataReader rdReader)
        {
            while (rdReader.Read())
            {
                var obj = new TopName();
                obj.ReadAll(rdReader);
                objList.Add(obj);
            }

        }

        public static void Read(this IList<Tip> objList, IDataReader rdReader)
        {
            while (rdReader.Read())
            {
                var obj = new Tip();
                obj.ReadAll(rdReader);
                objList.Add(obj);
            }

        }

        public static void Read(this Tip obj, IDataReader rdReader)
        {
            while (rdReader.Read())
            {  
                obj.ReadAll(rdReader); 
            }

        }

        public static void Read(this IList<Proverb> objList, IDataReader rdReader)
        {
            while (rdReader.Read())
            {
                var obj = new Proverb();
                obj.ReadAll(rdReader);
                objList.Add(obj);
            }

        }

        public static void Read(this Proverb obj, IDataReader rdReader)
        {
            while (rdReader.Read())
            {  
                obj.ReadAll(rdReader); 
            }

        }

        private static void ReadAll(this UyghurName obj, IDataReader rdReader)
        {
            obj.Id = rdReader.GetInt("recid");
            obj.NameUg = rdReader.GetString("name_ug");
           
            obj.NameLatin = rdReader.GetString("name_latin");
            obj.RelatedName = rdReader.GetString("related_name");
            obj.Origin = rdReader.GetEnum<OriginType>("origin");
            obj.Gender = rdReader.GetEnum<GenderType>("gender");
            obj.IsFamilyName = rdReader.GetBoolean("is_surname");
            obj.Description = rdReader.GetString("description");
            
        }

        public static void Read(this IList<User> objList, IDataReader rdReader)
        {
            while (rdReader.Read())
            {
                var obj = new User();
                obj.ReadAll(rdReader);
                objList.Add(obj);
            }

        }

        public static void Read(this User obj, IDataReader rdReader)
        {
            while (rdReader.Read())
            {
               
                obj.ReadAll(rdReader);
               
            }

        }
        private static void ReadAll(this User obj, IDataReader rdReader)
        {
            obj.Id = rdReader.GetInt("recid");
            obj.FirstName = rdReader.GetString("first_name");
            obj.LastName = rdReader.GetString("last_name");
            obj.Email = rdReader.GetString("email");
            obj.Status = rdReader.GetEnum<StatusType>("status");
            obj.Profile = rdReader.GetEnum<ProfileType>("role");
            obj.Phone = rdReader.GetString("phone");
            obj.Description = rdReader.GetString("description");

        }

        private static void ReadAll(this NameGenderCount obj, IDataReader rdReader)
        {
            obj.Male = rdReader.GetInt("MaleCount");
            obj.Female = rdReader.GetInt("FemaleCount");
            obj.Unisex = rdReader.GetInt("UnisexCount");
            obj.Total = rdReader.GetInt("Total");
            obj.Like = rdReader.GetInt("likeCount");
            obj.Love = rdReader.GetInt("LoveCount");
            obj.MyName = rdReader.GetInt("MyNameCount");

        }

        private static void ReadAll(this TopName obj, IDataReader rdReader)
        {
            obj.Id = rdReader.GetInt("recid");
            obj.NameUg = rdReader.GetString("name");
            obj.NameLatin = rdReader.GetString("nameLatin");
            obj.LikeCount = rdReader.GetInt("likeCount");
            obj.LoveCount = rdReader.GetInt("LoveCount");
            obj.MyNameCount = rdReader.GetInt("MyNameCount");

        }

        /// Read Tip
        private static void ReadAll(this Tip obj, IDataReader rdReader)
        {
            obj.Id = rdReader.GetInt("recid");
            obj.Title = rdReader.GetString("title");
           
            obj.Summary = rdReader.GetString("summary");
            obj.Content = rdReader.GetString("content");
            obj.Private = rdReader.GetBoolean("private");
            obj.Category = rdReader.GetEnum<TipsType>("category");
            obj.Source = rdReader.GetString("source");
            obj.UserName = rdReader.GetString("first_name");
            
        }

        ///
        // Read Uyghur Proverb
        ///
        private static void ReadAll(this Proverb obj, IDataReader rdReader)
        {
            obj.Id = rdReader.GetInt("recid");
            obj.Content = rdReader.GetString("content");
            obj.Description = rdReader.GetString("description");
            obj.UserName = rdReader.GetString("first_name");
            
        }
    }
}
