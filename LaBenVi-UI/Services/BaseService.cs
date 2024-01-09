using static LaBenVi_UI.Utilities.Static_Details;
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


        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

        }


        public async Task<ResponseDto?> SendAsync(RequestDto requestVM)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("LaBenviAPI");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");

                message.RequestUri = new Uri(requestVM.Url);
                if (requestVM.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestVM.Data), Encoding.UTF8, "application/json");
                }

                HttpResponseMessage? apiResponse = null;

                switch (requestVM.ApiAction)
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
