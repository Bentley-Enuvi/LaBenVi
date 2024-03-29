﻿using static LaBenVi_UI.Utilities.Static_Details;
using System.Net;
using System.Text;
using LaBenVi_UI.Models;
using Newtonsoft.Json;
using LaBenVi_UI.Services.IServices;
using System.Security.AccessControl;

namespace LaBenVi_UI.Services
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProvider _tokenProvider;


        public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
        {
            _httpClientFactory = httpClientFactory;
            _tokenProvider = tokenProvider;
        }


        public async Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("LaBenviAPI");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");

                //token
                if(withBearer)
                {
                    var token = _tokenProvider.GetToken();
                    message.Headers.Add("Authorization", $"Bearer {token}");
                }

                message.RequestUri = new Uri(requestDto.Url);
                if (requestDto.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                }

                HttpResponseMessage? apiResponse = null;

                switch (requestDto.ApiAction)
                {
                    case ApiAction.POST:
                        message.Method = HttpMethod.Post;
                        break;

                    case ApiAction.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;

                    case ApiAction.PUT:
                        message.Method = HttpMethod.Put;
                        break;

                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                apiResponse = await client.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not Found" };  //Error 404

                    case HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Access Denied" }; //Error 403

                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized" }; //Error 401

                    case HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Internal Server Error" }; //Error 500

                    default:
                        var responseData = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(responseData);
                        return apiResponseDto;

                }
            }
            catch (Exception ex)
            {
                var vM = new ResponseDto
                {
                    Message = ex.Message.ToString(),
                    IsSuccess = false
                };
                return vM;
            }


        }
    }
}
