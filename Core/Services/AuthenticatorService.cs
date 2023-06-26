using Core.Enumerations;
using Core.Intefaces;
using Core.Models;
using Core.Models.Configuration;
using Core.Models.Dtos;
using System.Data;

namespace Core.Services
{
    public class AuthenticatorService : IAuthenticatorService
    {
        private static readonly string _storedProcedure = "sp_authenticator";
        private readonly ILogService _logService;
        private readonly ISkynetRepository _skynetRepository;
        private readonly IConfigurationService _configurationService;
        private readonly ICryptoService _cryptoService;
        private readonly MessagesDefault _messagesDefault;

        public AuthenticatorService(ISkynetRepository skynetRepository, ILogService logService, IConfigurationService configurationService, ICryptoService cryptoService)
        {
            _configurationService = configurationService;
            _skynetRepository = skynetRepository;
            _logService = logService;
            _cryptoService = cryptoService;
            _messagesDefault = _configurationService.Get<MessagesDefault>(Configuration.MessagesDefault);

        }
        public async Task<Response<AuthenticatorDto>> Login(string EMAIL, string PASSWORD)
        {
            Response<AuthenticatorDto> response = new();
            try
            {
                Dictionary<string, dynamic> parameters = new();
                parameters.Add("@P_OPERATION", "LOGIN");
                parameters.Add("@P_EMAIL", EMAIL);
                parameters.Add("@P_PASSWORD", PASSWORD);
                var responseBd = await _skynetRepository.CallSPData(_storedProcedure, parameters);

                response.Code = responseBd.Code;
                if (responseBd.Code == ResponseCode.Success)
                {
                    if (responseBd.Data.Rows.Count > 0)
                    {
                        DataRow dr = responseBd.Data.Rows[0];
                        response.Data = new AuthenticatorDto
                        {
                            email = (string)dr["EMAIL"],
                            nameUser = (string)dr["USER_NAME"],
                            rolName = (string)dr["ROL_NAME"]


                        };
                    }
                    response.Code = ResponseCode.Success;
                }
                else
                {
                    response.Code = ResponseCode.Error;
                    response.Description = responseBd.Description;
                }
                


            }
            catch (Exception ex)
            {
                response.Code = ResponseCode.FatalError;
                _logService.SaveLogApp($"{nameof(Login)} {nameof(Exception)} {ex.Message} | {ex.StackTrace}", LogType.Error);
                response.Description = _messagesDefault.FatalErrorMessage;
            }
            return response;
        }
    }
}
