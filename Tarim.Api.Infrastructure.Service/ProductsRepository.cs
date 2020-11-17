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
        public ProductsRepository(IConnection connection, ILogger<ProductsRepository> log) : base(connection)
        {
            logger = log;
        }

        /// <summary>
        /// Add product to store
        /// </summary>
        /// <param name="product"></param>
        /// <param name="userRecid"></param>
        /// <returns></returns>
        public async Task<Result<Product>> AddProduct(Product product, int userRecid)
        {
            var result = new Result<Product> { Object = product };
            var insertId = GetParameter("id_out", MySqlDbType.Int32, 10);
            await ExecuteNonQueryAsync("ADD_PRODUCT",
                GetParameter("name_in", product.ProductName, MySqlDbType.VarChar),
                GetParameter("product_type_in", product.ProductType, MySqlDbType.VarChar),
                GetParameter("price_in", product.Price, MySqlDbType.VarChar),
                GetParameter("sku_in", product.Sku, MySqlDbType.VarChar),
                GetParameter("media_file_in", product.MediaFile, MySqlDbType.VarChar),
                GetParameter("description_in", product.Description, MySqlDbType.Text),
                GetParameter("user_recid_in", userRecid, MySqlDbType.Int32),
                insertId
            );
            result.Object.Id = Convert.ToInt32(insertId.Value);
            result.Status = result.Object.Id > 0 ? ExecuteStatus.Success : ExecuteStatus.Failed;
            return result;
        }

        /// <summary>
        /// Add Today's Special product
        /// </summary>
        /// <param name="product"></param>
        /// <param name="userRecid"></param>
        /// <returns></returns>
        public async Task<Result<SpecialProduct>> AddTodaySpecial(SpecialProduct product, int userRecid)
        {
            var result = new Result<SpecialProduct> { Object = product };
            var insertId = GetParameter("id_out", MySqlDbType.Int32, 10);
            await ExecuteNonQueryAsync("ADD_TODAY_SPECIAL",
                GetParameter("name_in", product.ProductName, MySqlDbType.VarChar),
                GetParameter("product_type_in", product.ProductType, MySqlDbType.VarChar),
                GetParameter("price_in", product.Price, MySqlDbType.VarChar),
                GetParameter("sku_in", product.Sku, MySqlDbType.VarChar),
                GetParameter("media_file_in", product.MediaFile, MySqlDbType.VarChar),
                GetParameter("description_in", product.Description, MySqlDbType.Text),
                GetParameter("user_recid_in", userRecid, MySqlDbType.Int32),
                insertId
            );
            result.Object.Id = Convert.ToInt32(insertId.Value);
            result.Status = result.Object.Id > 0 ? ExecuteStatus.Success : ExecuteStatus.Failed;
            return result;
        }

        /// <summary>
        /// Delete Product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteProduct(int id)
        {
            var result = new Result<int> { Object = id, Status = ExecuteStatus.Error };
            await ExecuteNonQueryAsync("DELETE_PRODUCT",
                GetParameter("id_in", id, MySqlDbType.Int32)
            );
            result.Status = ExecuteStatus.Success;
            return result;
        }

        /// <summary>
        /// Delete Today's Special product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteTodaySpecial(int id)
        {
            var result = new Result<int> { Object = id, Status = ExecuteStatus.Error };
            await ExecuteNonQueryAsync("DELETE_TODAY_SPECIAL",
                GetParameter("id_in", id, MySqlDbType.Int32)
            );
            result.Status = ExecuteStatus.Success;
            return result;
        }

        /// <summary>
        /// Get all store products
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Get all today's special products
        /// </summary>
        /// <returns></returns>
        public async Task<Result<IList<SpecialProduct>>> GetTodaySpecials()
        {
            var products = new Result<IList<SpecialProduct>> { Object = new List<SpecialProduct>() };
            await GetResultAsync("GET_TODAY_SPECIAL_LIST",

                rdReader =>
                {
                    products.Object.Read(rdReader);
                    return products;
                });

            return products;
        }


        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<Result<Product>> UpdateProduct(Product product)
        {
            var result = new Result<Product> { Object = product, Status = ExecuteStatus.Error };
            await ExecuteNonQueryAsync("UPDATE_PRODUCT",
                GetParameter("id_in", product.Id, MySqlDbType.Int32),
                GetParameter("name_in", product.ProductName, MySqlDbType.VarChar),
                GetParameter("product_type_in", product.ProductType, MySqlDbType.VarChar),
                GetParameter("price_in", product.Price, MySqlDbType.VarChar),
                GetParameter("sku_in", product.Sku, MySqlDbType.VarChar),
                GetParameter("media_file_in", product.MediaFile, MySqlDbType.Text),
                GetParameter("description_in", product.Description, MySqlDbType.VarChar)
            );
            result.Status = ExecuteStatus.Success;
            return result;
        }

        /// <summary>
        /// Update today's special product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<Result<Product>> UpdateTodaySpecial(Product product)
        {
            var result = new Result<Product> { Object = product, Status = ExecuteStatus.Error };
            await ExecuteNonQueryAsync("UPDATE_PRODUCT",
                GetParameter("id_in", product.Id, MySqlDbType.Int32),
                GetParameter("name_in", product.ProductName, MySqlDbType.VarChar),
                GetParameter("product_type_in", product.ProductType, MySqlDbType.VarChar),
                GetParameter("price_in", product.Price, MySqlDbType.VarChar),
                GetParameter("sku_in", product.Sku, MySqlDbType.VarChar),
                GetParameter("media_file_in", product.MediaFile, MySqlDbType.Text),
                GetParameter("description_in", product.Description, MySqlDbType.VarChar)
            );
            result.Status = ExecuteStatus.Success;
            return result;
        }
    }
}
