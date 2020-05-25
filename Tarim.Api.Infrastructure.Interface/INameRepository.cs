// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INameRepository.cs" company="Tarim Lab">
//   Copyright (c) Tarim Lab. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Tarim.Api.Infrastructure.Common;
using Tarim.Api.Infrastructure.Model.Name;

namespace Tarim.Api.Infrastructure.Interface
{
    public interface INameRepository
    {
        Task<Result<IList<UyghurName>>> GetUyghurName(int pageNumber);

        Task<Result<UyghurName>> GetUyghurName(string name);

        Task<Result<UyghurName>> AddUyghurName(UyghurName uyghurName);

        Task<Result<UyghurName>> UpdateUyghurName(UyghurName uyghurName);

        Task<Result<int>> DeleteUyghurName(int id);

        Task<Result<NameAction>> AddNameAction(NameAction name);

        Task<Result<IList<TopName>>> GetTopNames();

        Task<Result<NameGenderCount>> GetNameStatistics();
    }
}
