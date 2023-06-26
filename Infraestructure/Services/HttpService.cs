using Core.Enumerations;
using Core.Intefaces;
using Core.Models.Configuration;
using Core.Models.HttpService;
using System.Text;

namespace Infraestructure.Services
{
    public class HttpService : IHttpService
    {
        private readonly ILogService _logService;
        private readonly IParseService _parseService;
        private readonly IConfigurationService _configurationService;
        private readonly MessagesDefault _messagesDefault;

        public HttpService(ILogService logService, IConfigurationService configurationService, IParseService parseService)
        {
            _logService = logService;
            _configurationService = configurationService;
            _parseService = parseService;

            _messagesDefault = _configurationService.Get<MessagesDefault>(Configuration.MessagesDefault);
        }
        public async Task<HttpServiceResponse<T>> POST<T>(HttpServiceRequest request)
        {
            HttpServiceResponse<T> response = new HttpServiceResponse<T>();
            try
            {

                HttpClientHandler clientHandler = new()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };

                using HttpClient httpClient = new(clientHandler);
                httpClient.BaseAddress = request.Uri;

                foreach (KeyValuePair<string, string> result in request.Headers)
                {
                    httpClient.DefaultRequestHeaders.Add(result.Key, result.Value);
                }

                StringContent content = new(request.Body, Encoding.UTF8, "application/json");

                using var responseAPI = await httpClient.PostAsync($"{request.Method}", content);
                string apiResponseString = await responseAPI.Content.ReadAsStringAsync();

                if (responseAPI.IsSuccessStatusCode)
                {
                    response.Code = ResponseCode.Success;
                    response.Data = _parseService.Deserealize<T>(apiResponseString);
                }
                else
                {
                    _logService.SaveLogApp($"[POST - HTTP CODE EXCEPTION] {apiResponseString}", LogType.Error);
                    response.Code = ResponseCode.FatalError;
                    response.Description = _messagesDefault.FatalErrorMessage;
                }

            }
            catch (TimeoutException ex)
            {
                _logService.SaveLogApp($"[POST - TimeoutException] {ex.Message} | {ex.StackTrace}", LogType.Error);
                response.Code = ResponseCode.Timeout;
                response.Description = _messagesDefault.TimeoutMessage;
            }
            catch (Exception ex)
            {
                _logService.SaveLogApp($"[POST - Exception] {ex.Message} | {ex.StackTrace}", LogType.Error);
                response.Code = ResponseCode.FatalError;
                response.Description = _messagesDefault.FatalErrorMessage;
            }
            return response;
        }
    }
}
