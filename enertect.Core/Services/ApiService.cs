using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using enertect.Core.Data.Models;
using enertect.Core.Helpers;
using enertect.Core.Services.Interfaces;
using Flurl.Http;

namespace enertect.Core.Services
{
    public class ApiService : IApiService
    {

        #region EndPoints
        const string SIGN_IN_ENDPOINT = AppConstant.API_ENDPOINT + "/Login/Login";
        #endregion

        public async Task<ApiResponse<User>> SignIn(String username, String password)
        {
            var body = new Dictionary<string, string>
            {
                { "Username", username },
                { "Password", password }
            };
            return await DoPost<User>(SIGN_IN_ENDPOINT, body);
        }

        #region Methods
        protected async Task<ApiResponse<T>> DoPost<T>(string url, object data)
        {
            ApiResponse<T> result = null;
            try
            {
                var res = await url.PostJsonAsync(data).ReceiveJson<T>();

                result = new ApiResponse<T>()
                {
                    IsSuccess = true,
                    ResponseStatusCode = 200,
                    ResponseObject = res
                };

            }
            catch (FlurlHttpTimeoutException fhte)
            {
                result = new ApiResponse<T>()
                {
                    IsSuccess = false,
                    Errors = new List<string>() { "Timeout" },
                    ResponseStatusCode = fhte.Call.HttpStatus.HasValue ? (int)fhte.Call.HttpStatus.Value : 500
                };
            }
            catch (FlurlHttpException fhx)
            {
                result = new ApiResponse<T>()
                {
                    IsSuccess = false,
                    Errors = new List<string>() { fhx.Message },
                    ResponseStatusCode = fhx.Call.HttpStatus.HasValue ? (int)fhx.Call.HttpStatus.Value : 500
                };
            }

            catch (Exception ex)
            {
                result = new ApiResponse<T>()
                {
                    IsSuccess = false,
                    Errors = new List<string>() { ex.Message },
                    ResponseStatusCode = 500
                };
            }

            return result;
        }
        #endregion
    }
}
