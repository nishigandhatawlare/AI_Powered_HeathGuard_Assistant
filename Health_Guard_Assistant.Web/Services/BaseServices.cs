using Health_Guard_Assistant.Web.Models;
using Health_Guard_Assistant.Web.Services.IServices;
using static Health_Guard_Assistant.Web.Utility.SD;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Serilog;

namespace Health_Guard_Assistant.Web.Services
{
    public class BaseServices : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProvider _tokenProvider;


        public BaseServices(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _tokenProvider = tokenProvider;
        }

        public async Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true)
        {
            Log.Information("Sending request to URL: {Url} with method: {Method}", requestDto.Url, requestDto.ApiType);

            try
            {
                // Create the HttpClient using the factory
                HttpClient client = _httpClientFactory.CreateClient("HealthGuardWebAppApi");

                // Create the HttpRequestMessage
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");

                //token 
                if (withBearer) {
                    var token = _tokenProvider.GetToken();
                    message.Headers.Add("Authorization", $"Bearer {token}");
                }

                // Set the request URI
                message.RequestUri = new Uri(requestDto.Url);

                // Add request body content if available
                if (requestDto.Data != null)
                {
                    var jsonData = JsonConvert.SerializeObject(requestDto.Data);
                    message.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    Log.Debug("Request body: {RequestBody}", jsonData);
                }

                // Set the HTTP method based on the ApiType
                message.Method = requestDto.ApiType switch
                {
                    ApiType.Post => HttpMethod.Post,
                    ApiType.Delete => HttpMethod.Delete,
                    ApiType.Put => HttpMethod.Put,
                    _ => HttpMethod.Get
                };

                // Send the HTTP request
                HttpResponseMessage? apiResponse = await client.SendAsync(message);
                Log.Information("Received response with status code: {StatusCode}", apiResponse.StatusCode);

                // Handle different HTTP status codes
                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        Log.Warning("Not Found: {Url}", requestDto.Url);
                        return new ResponseDto { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        Log.Warning("Access Denied: {Url}", requestDto.Url);
                        return new ResponseDto { IsSuccess = false, Message = "Access Denied" };
                    case HttpStatusCode.Unauthorized:
                        Log.Warning("Unauthorized: {Url}", requestDto.Url);
                        return new ResponseDto { IsSuccess = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        Log.Error("Internal Server Error: {Url}", requestDto.Url);
                        return new ResponseDto { IsSuccess = false, Message = "Internal Server Error" };
                    default:
                        if (apiResponse.IsSuccessStatusCode)
                        {
                            var apiContent = await apiResponse.Content.ReadAsStringAsync();
                            Log.Debug("Response content: {ApiContent}", apiContent);

                            var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);

                            if (apiResponseDto != null)
                            {
                                apiResponseDto.IsSuccess = true;
                                apiResponseDto.Message = "Request successful!";
                                return apiResponseDto;
                            }
                            else
                            {
                                Log.Error("Failed to deserialize response content.");
                                return new ResponseDto { IsSuccess = false, Message = "Failed to deserialize response" };
                            }
                        }
                        else
                        {
                            Log.Warning("Unexpected status code: {StatusCode}", apiResponse.StatusCode);
                            return new ResponseDto { IsSuccess = false, Message = "Unexpected status code: " + apiResponse.StatusCode };
                        }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while sending the request to {Url}", requestDto.Url);
                return new ResponseDto
                {
                    Message = ex.Message.ToString(),
                    IsSuccess = false
                };
            }
        }
    }
}
