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
using Tarim.Api.Infrastructure.Common;
using Tarim.Api.Infrastructure.Model.Products;

namespace Tarim.Api.Infrastructure.Interface
{
    public interface IProductsRepository
    {
        Task<Result<IList<Product>>> GetAllProducts();

       // Task<Result<IList<Product>>> GetProducts(int pageNumber);

      //  Task<Result<Product>> GetProduct(int id);

        Task<Result<Product>> AddProduct(Product product,int userRecid);

        Task<Result<Product>> UpdateProduct(Product product);

        Task<Result<int>> DeleteProduct(int id);

        Task<Result<IList<Product>>> GetTodaySpecials();

        Task<Result<Product>> AddTodaySpecial(Product product, int userRecid);

        Task<Result<Product>> UpdateTodaySpecial(Product product);

        Task<Result<int>> DeleteTodaySpecial(int id);

    }
}
