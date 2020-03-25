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
        Task<ApiResponse<UpsInformation>> getHistoryUpsInfornations(int upID, DateTimeOffset start, DateTimeOffset end);
        Task<ApiResponse<User>> SignIn(string username, string password);
        Task<ApiResponse<UpLimit>> getUpLimitHistory(int upID, DateTimeOffset start, DateTimeOffset end);
        Task<ApiResponse<UpLimit>> getUpLimit(int upID);
        Task<ApiResponse<AlarmsInfo>> getAlarms();
    }
}
