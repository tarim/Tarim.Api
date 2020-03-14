// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INameRepository.cs" company="Tarim Lab">
//   Copyright (c) Tarim Lab. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Tarim.Api.Infrastructure.Common;
using Tarim.Api.Infrastructure.Model.names;

namespace Tarim.Api.Infrastructure.Interface
{
    public interface INameRepository
    {
        Task<Result<IList<UyghurName>>> GetUyghurName();

        Task<Result<UyghurName>> GetUyghurName(string name);

        Task<Result<UyghurName>> AddUyghurName(UyghurName uyghurName);

        Task<Result<UyghurName>> UpdateUyghurName(UyghurName uyghurName);

        Task<Result<int>> DeleteUyghurName(int id);
    }
}
