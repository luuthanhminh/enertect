using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using enertect.Core.Data.Models;
using enertect.Core.Data.Models.Ups;
using enertect.Core.Helpers;
using enertect.Core.Services.Interfaces;
using Flurl.Http;

namespace enertect.Core.Services
{
    public class ApiService : IApiService
    {

        #region EndPoints
        const string UPSIN_ENDPOINT = AppConstant.API_ENDPOINT + "Upsinformations/Get";
        #endregion

        public async Task<ApiResponse<UpsInformation>> getUpsInfornations()
        {
            return await DoGetList<UpsInformation>(UPSIN_ENDPOINT);
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

        protected async Task<ApiResponse<T>> DoGetList<T>(string url)
        {
            ApiResponse<T> result = null;
            try
            {
                var res = await url.GetJsonAsync<IList<T>>();

                result = new ApiResponse<T>()
                {
                    IsSuccess = true,
                    ResponseStatusCode = 200,
                    ResponseListObject = res
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

        protected async Task<ApiResponse<T>> DoGet<T>(string url)
        {
            ApiResponse<T> result = null;
            try
            {
                var res = await url.GetJsonAsync<T>();

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
