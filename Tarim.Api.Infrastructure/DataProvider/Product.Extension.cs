//
//  Author:
//    Nur Karluk ns.karluk@gmail.com
//
//  Copyright (c) 2020, (c) Tarim Lab
//
//  All rights reserved.
//
//  Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
//
//     * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in
//       the documentation and/or other materials provided with the distribution.
//     * Neither the name of the [ORGANIZATION] nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
//
//  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
//  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
//  LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
//  A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
//  CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
//  EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
//  PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
//  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
//  LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
//  NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
//  SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
using System;
using System.Collections.Generic;
using System.Data;
using Tarim.Api.Infrastructure.Model.Products;

namespace Tarim.Api.Infrastructure.DataProvider
{
    public static partial class DbConnectionExtension
    {
        public static void Read(this IList<Product> objList, IDataReader rdReader)
        {
            while (rdReader.Read())
            {
                var obj = new Product();
                obj.ReadAll(rdReader);
                objList.Add(obj);
            }

        }

        public static void Read(this Product obj, IDataReader rdReader)
        {
            while (rdReader.Read())
            {
                obj.ReadAll(rdReader);
            }

        }



        private static void ReadAll(this Product obj, IDataReader rdReader)
        {
            obj.Id = rdReader.GetInt("recid");
            obj.ProductName = rdReader.GetString("name");

            obj.Price = rdReader.GetString("price");
            obj.ProductType = rdReader.GetString("product_type");
            obj.MediaFile = rdReader.GetString("media_file");
            obj.Sku = rdReader.GetString("sku");
            obj.CreatedDate = rdReader.GetNullableDateTime("rec_create_datetime").Value;
            obj.Description = rdReader.GetString("description");

        }


        public static void Read(this IList<SpecialProduct> objList, IDataReader rdReader)
        {
            while (rdReader.Read())
            {
                var obj = new SpecialProduct();
                obj.ReadAll(rdReader);
                objList.Add(obj);
            }

        }

        public static void Read(this SpecialProduct obj, IDataReader rdReader)
        {
            while (rdReader.Read())
            {
                obj.ReadAll(rdReader);
            }

        }

        private static void ReadAll(this SpecialProduct obj, IDataReader rdReader)
        {
            obj.Id = rdReader.GetInt("recid");
            obj.ProductName = rdReader.GetString("name");
            obj.SpecialPrice = rdReader.GetString("special_price");
            obj.Price = rdReader.GetString("price");
            obj.ProductType = rdReader.GetString("product_type");
            obj.MediaFile = rdReader.GetString("media_file");
            obj.Sku = rdReader.GetString("sku");
            obj.CreatedDate = rdReader.GetNullableDateTime("rec_create_datetime").Value;
            obj.Description = rdReader.GetString("description");

        }


    }
}
