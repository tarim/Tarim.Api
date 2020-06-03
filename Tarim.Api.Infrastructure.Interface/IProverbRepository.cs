// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProverbRepository.cs" company="Tarim Lab">
//   Copyright (c) Tarim Lab. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Tarim.Api.Infrastructure.Common;
using Tarim.Api.Infrastructure.Model.Proverbs;

namespace Tarim.Api.Infrastructure.Interface
{
    public interface IProverbRepository
    {
        Task<Result<IList<Proverb>>> GetProverbs(int pageNumber);

        Task<Result<IList<Proverb>>> GetDailyProverb(int pageSize);

        Task<Result<Proverb>> GetProverb(int proverbId);

        Task<Result<Proverb>> AddProverb(Proverb proverb);

        Task<Result<Proverb>> UpdateProverb(Proverb proverb);

        Task<Result<int>> DeleteProverb(int id);

    }
}
