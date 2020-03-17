using System;
using System.Threading.Tasks;
using enertect.Core.Data.Models;
using enertect.Core.Data.Models.Ups;
using enertect.Core.Helpers;

namespace enertect.Core.Services.Interfaces
{
    public interface IApiService
    {
        Task<ApiResponse<UpsInformation>> getUpsInfornations();
    }
}
