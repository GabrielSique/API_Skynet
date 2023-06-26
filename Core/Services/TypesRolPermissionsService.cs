using Core.Enumerations;
using Core.Intefaces;
using Core.Models;
using Core.Models.Configuration;
using Core.Models.Dtos;
using Core.Models.Entities;
using System.Data;

namespace Core.Services
{
    public class TypesRolPermissionsService : ITypesRolePermissions
    {
        private static readonly string _storedProcedure = "sp_roles";
        private readonly ILogService _logService;
        private readonly ISkynetRepository _skynetRepository;
        private readonly IConfigurationService _configurationService;
        private readonly ICryptoService _cryptoService;
        private readonly MessagesDefault _messagesDefault;

        public TypesRolPermissionsService(ISkynetRepository skynetRepository, ILogService logService, IConfigurationService configurationService, ICryptoService cryptoService)
        {
            _configurationService = configurationService;
            _skynetRepository = skynetRepository;
            _logService = logService;
            _cryptoService = cryptoService;
            _messagesDefault = _configurationService.Get<MessagesDefault>(Configuration.MessagesDefault);

        }

        public async Task<Response<List<TypesRolPermissions>>> GetAllTypesRoles()
        {
            Response<List<TypesRolPermissions>> response = new();
            List<TypesRolPermissions> users = new();
            try
            {
                Dictionary<string, dynamic> bdParameters = new();
                bdParameters.Add("@P_OPERATION ", "GAR");
                //bdParameters.Add("@P_PASSWORD_USER", null);
                //bdParameters.Add("@P_FIRST_NAME", null);
                //bdParameters.Add("@P_SURNAME", null);
                //bdParameters.Add("@P_SECOND_SURNAME", null);
                //bdParameters.Add("@P_DIRECTION", null);
                //bdParameters.Add("@P_PHONE_NUMBER", null);
                //bdParameters.Add("@P_DNI", null);
                //bdParameters.Add("@P_EMAIL", null);
                //bdParameters.Add("@P_BUSINESS_NAME", null);
                //bdParameters.Add("@P_NIT", null);
                //bdParameters.Add("@P_ID_ROL", null);
                //bdParameters.Add("@P_USER_ID", null);
                //bdParameters.Add("@P_SEARCH", null);
                //bdParameters.Add("@P_STATUS_USER", null);
                var responseBd = await _skynetRepository.CallSPData(_storedProcedure, bdParameters);

                response.Code = responseBd.Code;
                if (responseBd.Code == ResponseCode.Success)
                {
                    foreach (DataRow dr in responseBd.Data.Rows)
                    {
                        users.Add(new TypesRolPermissions
                        {
                            idRol = (int)dr["ID_ROL"],
                            rolName = (string)dr["ROL_NAME"],
                            createUser = (string)dr["CREATE_USER"],
                            updateUser = (string)dr["UPDATE_USER"],
                            readUser = (string)dr["READ_USERS"],
                            createVisit = (string)dr["CREATE_VISIT"],
                            updateVisit = (string)dr["UPDATE_VISIT"],
                            reportClient = (string)dr["REPORT_CLIENT"],
                            reportSuper = (string)dr["REPORT_SUPER"],
                            sendEmail = (string)dr["SEND_EMAIL"]
                        });
                    }
                    response.Data = users;
                    response.Code = ResponseCode.Success;
                }
                else
                    response.Description = responseBd.Description;

            }
            catch (Exception ex)
            {
                response.Code = ResponseCode.FatalError;
                _logService.SaveLogApp($"{nameof(GetAllTypesRoles)} {nameof(Exception)} {ex.Message} | {ex.StackTrace}", LogType.Error);
                response.Description = _messagesDefault.FatalErrorMessage;
            }
            return response;
        }
    }
}
