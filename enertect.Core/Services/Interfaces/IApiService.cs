using System;
using System.Threading.Tasks;
using enertect.Core.Data.Models;
using enertect.Core.Helpers;

namespace enertect.Core.Services.Interfaces
{
    public interface IApiService
    {
        Task<ApiResponse<User>> SignIn(String username, String password);
    }
}
