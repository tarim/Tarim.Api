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
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Tarim.Api.Infrastructure.Common;
using Tarim.Api.Infrastructure.Common.Enums;
using Tarim.Api.Infrastructure.DataProvider;
using Tarim.Api.Infrastructure.Interface;
using Tarim.Api.Infrastructure.Model.Products;

namespace Tarim.Api.Infrastructure.Service
{
    public class ProductsRepository : BaseRepository, IProductsRepository
    {
        private readonly ILogger<ProductsRepository> logger;
        public ProductsRepository(IConnection connection, ILogger<ProductsRepository> log):base(connection)
        {
            logger = log;
        }

      public async  Task<Result<Product>> AddProduct(Product product,int userRecid)
        {
            var result = new Result<Product> { Object = product };
            var insertId = GetParameter("id_out", MySqlDbType.Int32, 10);
            await ExecuteNonQueryAsync("ADD_PRODUCT",
                GetParameter("name_in", product.Name, MySqlDbType.VarChar),
                GetParameter("product_type_in", product.ProductType, MySqlDbType.VarChar),
                GetParameter("price_in", product.Price, MySqlDbType.Double),
                GetParameter("sku_in", product.Sku, MySqlDbType.VarChar),
                GetParameter("media_url_in", product.MediaUrl, MySqlDbType.VarChar),
                GetParameter("description_in", product.Description, MySqlDbType.Text),
                GetParameter("user_recid_in", userRecid, MySqlDbType.Int32),
                insertId
            );
            result.Object.Id = Convert.ToInt32(insertId.Value);
            result.Status = result.Object.Id > 0 ? ExecuteStatus.Success : ExecuteStatus.Failed;
            return result;
        }

       public async Task<Result<int>> DeleteProduct(int id)
        {
            var result = new Result<int> { Object = id, Status = ExecuteStatus.Error };
            await ExecuteNonQueryAsync("DELETE_PRODUCT",
                GetParameter("id_in", id, MySqlDbType.Int32)
            );
            result.Status = ExecuteStatus.Success;
            return result;
        }

       public async Task<Result<IList<Product>>> GetAllProducts()
        {
            var products = new Result<IList<Product>> { Object = new List<Product>() };
            await GetResultAsync("GET_ALL_PRODUCT",

                rdReader =>
                {
                    products.Object.Read(rdReader);
                    return products;
                });

            return products;
        }

        Task<Result<Product>> IProductsRepository.GetProduct(int id)
        {
            throw new NotImplementedException();
        }

       public async Task<Result<IList<Product>>> GetProducts(int pageNumber)
        {
            var products = new Result<IList<Product>> { Object = new List<Product>() };
            await GetResultAsync("GET_PRODUCT_LIST",

                rdReader =>
                {
                    products.Object.Read(rdReader);
                    return products;
                },
                 GetParameter("pageNumber_in", pageNumber, MySqlDbType.Int32));
          
            return products;
        }

      public async  Task<Result<Product>> UpdateProduct(Product product)
        {
            var result = new Result<Product> { Object = product, Status = ExecuteStatus.Error };
            await ExecuteNonQueryAsync("UPDATE_PRODUCT",
                GetParameter("id_in", product.Id, MySqlDbType.Int32),
                GetParameter("name_in", product.Name, MySqlDbType.VarChar),
                GetParameter("product_type_in", product.ProductType, MySqlDbType.VarChar),
                GetParameter("price_in", product.Price, MySqlDbType.Double),
                GetParameter("sku_in", product.Sku, MySqlDbType.VarChar),
                GetParameter("media_url_in", product.MediaUrl, MySqlDbType.VarChar),
                GetParameter("description_in", product.Description, MySqlDbType.VarChar)
            );
            result.Status = ExecuteStatus.Success;
            return result;
        }
    }
}
