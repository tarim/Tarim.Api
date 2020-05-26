// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITipsRepository.cs" company="Tarim Lab">
//   Copyright (c) Tarim Lab. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Tarim.Api.Infrastructure.Common;
using Tarim.Api.Infrastructure.Model.Tips;

namespace Tarim.Api.Infrastructure.Interface
{
    public interface ITipsRepository
    {
        Task<Result<IList<Tip>>> GetTips(int pageNumber);

        Task<Result<Tip>> GetTip(int tipId);

        Task<Result<Tip>> AddTip(Tip tip);

        Task<Result<Tip>> UpdateTip(Tip tip);

        Task<Result<int>> DeleteTip(int id);

     //   Task<Result<TipAction>> AddTipAction(TipAction tipAction);

    }
}
