﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using enertect.Core.Data.Models;
using enertect.Core.Data.Models.Ups;
using enertect.Core.Helpers;
using enertect.Core.Services.Interfaces;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace enertect.Core.Services
{
    public class ApiService : IApiService
    {
        #region Properties
        public User User { get; set; }
        public String EndPoint { get; set; }
        #endregion

        #region EndPoints
        const string UPSIN_ENDPOINT = "Upsinformations/Get";
        const string LIMIT_ENDPOINT = "upsinformations/GetUpsLimits?upsId=";
        const string HISTORY_ENDPOINT = "upsinformations/GetTrendingHistory?date=01/12/2019&endDate=01/03/2020&upsId=1";
        const string ALARM_ENDPOINT = "alarms/Get";
        const string SITE_DETAIL_ENDPOINT = "upsinformations/GetSiteDetails";
        const string SIGN_IN_ENDPOINT = AppConstant.API_ENDPOINT + "auth/login";
        #endregion

        public ApiService()
        {
            var userPre = Preferences.Get(AppConstant.USER_TOKEN, "");
            if(userPre.Length > 0)
            {
                this.User = JsonConvert.DeserializeObject<User>(userPre);
                this.EndPoint = User.ApiEndpoint;
            }
        }

        public async Task<ApiResponse<Site>> getSiteDetail()
        {
            return await DoGet<Site>($"{EndPoint}{SITE_DETAIL_ENDPOINT}");
        }

        public async Task<ApiResponse<UpsInformation>> getHistoryUpsInfornations(int upID, DateTimeOffset start, DateTimeOffset end)
        {
        
            var url = $"{EndPoint}{HISTORY_ENDPOINT}".SetQueryParams(new
            {
                date = start.ToString("MM/dd/yyyy"),
                endDate = end.ToString("MM/dd/yyyy"),
                upsId = upID
            });

            return await DoGetList<UpsInformation>(url);
        }

        public async Task<ApiResponse<UpsInformation>> getUpsInfornations()
        {
            return await DoGetList<UpsInformation>($"{EndPoint}{UPSIN_ENDPOINT}");
        }

        public async Task<ApiResponse<UpLimit>> getUpLimitHistory(int upID, DateTimeOffset start, DateTimeOffset end)
        {
            var url = $"{EndPoint}{LIMIT_ENDPOINT}".SetQueryParams(new
            {
                date = start.ToString("MM/dd/yyyy"),
                endDate = end.ToString("MM/dd/yyyy"),
                upsId = upID
            });
            return await DoGet<UpLimit>(url);
        }

        public async Task<ApiResponse<UpLimit>> getUpLimit(int upID)
        {
            return await DoGet<UpLimit>($"{EndPoint}{LIMIT_ENDPOINT}{upID}");
        }

        public async Task<ApiResponse<User>> SignIn(string username, string password)
        {
            var body = new Dictionary<string, string>
            {
                { "Username", username },
                { "Password", password }
            };
            return await DoPost<User>(SIGN_IN_ENDPOINT, body);
        }

        public async Task<ApiResponse<AlarmsInfo>> getAlarms()
        {
            return await DoGet<AlarmsInfo>($"{EndPoint}{ALARM_ENDPOINT}");
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
                var res = await url.WithOAuthBearerToken(User.Token).GetJsonAsync<List<T>>();

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
                var res = await url.WithOAuthBearerToken(User.Token).GetJsonAsync<T>();

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
