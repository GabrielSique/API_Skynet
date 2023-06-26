using Core.Enumerations;
using Core.Intefaces;
using Core.Models;
using Core.Models.Configuration;
using Core.Models.Dtos;
using Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UserService : IUsersService
    {
        private static readonly string _storedProcedure = "sp_insertUsers";
        private readonly ILogService _logService;
        private readonly ISkynetRepository _skynetRepository;
        private readonly IConfigurationService _configurationService;
        private readonly ICryptoService _cryptoService;
        private readonly MessagesDefault _messagesDefault;

        public UserService(ISkynetRepository skynetRepository, ILogService logService, IConfigurationService configurationService, ICryptoService cryptoService)
        {
            _configurationService = configurationService;
            _skynetRepository = skynetRepository;
            _logService = logService;
            _cryptoService = cryptoService;
            _messagesDefault = _configurationService.Get<MessagesDefault>(Configuration.MessagesDefault);

        }

        public async Task<Response<List<Users>>> GetAllUsers()
        {
            Response<List<Users>> response = new();
            List<Users> users = new();
            try
            {
                Dictionary<string, dynamic> bdParameters = new();
                bdParameters.Add("@P_OPERATION ", "GAU");
                bdParameters.Add("@P_PASSWORD_USER", null);
                bdParameters.Add("@P_FIRST_NAME", null);
                bdParameters.Add("@P_SURNAME", null);
                bdParameters.Add("@P_SECOND_SURNAME", null);
                bdParameters.Add("@P_DIRECTION", null);
                bdParameters.Add("@P_PHONE_NUMBER", null);
                bdParameters.Add("@P_DNI", null);
                bdParameters.Add("@P_EMAIL", null);
                bdParameters.Add("@P_BUSINESS_NAME", null);
                bdParameters.Add("@P_NIT", null);
                bdParameters.Add("@P_ID_ROL", null);
                bdParameters.Add("@P_USER_ID", null);
                bdParameters.Add("@P_SEARCH", null);
                bdParameters.Add("@P_STATUS_USER", null);
                var responseBd = await _skynetRepository.CallSPData(_storedProcedure, bdParameters);

                response.Code = responseBd.Code;
                if (responseBd.Code == ResponseCode.Success)
                {
                    foreach (DataRow dr in responseBd.Data.Rows)
                    {
                        users.Add(new Users
                        {
                            idUser = (int)dr["ID_USER"],
                            passwordUser = (string)dr["PASSWORD_USER"],
                            firstName = (string)dr["FIRST_NAME"],
                            surName = (string)dr["SURNAME"],
                            secondSurName = (string)dr["SECOND_SURNAME"],
                            direction = (string)dr["DIRECTION"],
                            phoneNumber = (int)dr["PHONE_NUMBER"],
                            dni = (string)dr["DNI"],
                            email = (string)dr["EMAIL"],
                            businessName = (string)dr["BUSINESS_NAME"],
                            nit = (int)dr["NIT"],
                            statusUser = (string)dr["STATUS_USER"],
                            idRol = (int)dr["ID_ROL"],
                            rolName = (string)dr["ROL_NAME"],
                            userName = (string)dr["USER_NAME"],
                            statusName = (string)dr["NAME_STATUS"]
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
                _logService.SaveLogApp($"{nameof(GetAllUsers)} {nameof(Exception)} {ex.Message} | {ex.StackTrace}", LogType.Error);
                response.Description = _messagesDefault.FatalErrorMessage;
            }
            return response;
        }

        public async Task<Response<List<Users>>> GetAllUsersByFilter(string filter)
        {
            Response<List<Users>> response = new();
            List<Users> users = new();
            try
            {
                Dictionary<string, dynamic> bdParameters = new();
                bdParameters.Add("@P_OPERATION ", "SRU");
                bdParameters.Add("@P_PASSWORD_USER", null);
                bdParameters.Add("@P_FIRST_NAME", null);
                bdParameters.Add("@P_SURNAME", null);
                bdParameters.Add("@P_SECOND_SURNAME", null);
                bdParameters.Add("@P_DIRECTION", null);
                bdParameters.Add("@P_PHONE_NUMBER", null);
                bdParameters.Add("@P_DNI", null);
                bdParameters.Add("@P_EMAIL", null);
                bdParameters.Add("@P_BUSINESS_NAME", null);
                bdParameters.Add("@P_NIT", null);
                bdParameters.Add("@P_ID_ROL", null);
                bdParameters.Add("@P_USER_ID", null);
                bdParameters.Add("@P_SEARCH", filter);
                bdParameters.Add("@P_STATUS_USER", null);

                var responseBd = await _skynetRepository.CallSPData(_storedProcedure, bdParameters);

                response.Code = responseBd.Code;
                if (responseBd.Code == ResponseCode.Success)
                {
                    foreach (DataRow dr in responseBd.Data.Rows)
                    {
                        users.Add(new Users
                        {
                            idUser = (int)dr["ID_USER"],
                            passwordUser = (string)dr["PASSWORD_USER"],
                            firstName = (string)dr["FIRST_NAME"],
                            surName = (string)dr["SURNAME"],
                            secondSurName = (string)dr["SECOND_SURNAME"],
                            direction = (string)dr["DIRECTION"],
                            phoneNumber = (int)dr["PHONE_NUMBER"],
                            dni = (string)dr["DNI"],
                            email = (string)dr["EMAIL"],
                            businessName = (string)dr["BUSINESS_NAME"],
                            nit = (int)dr["NIT"],
                            statusUser = (string)dr["STATUS_USER"],
                            idRol = (int)dr["ID_ROL"],
                            rolName = (string)dr["ROL_NAME"],
                            userName = (string)dr["USER_NAME"],
                            statusName = (string)dr["NAME_STATUS"]
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
                _logService.SaveLogApp($"{nameof(GetAllUsersByFilter)} {nameof(Exception)} {ex.Message} | {ex.StackTrace}", LogType.Error);
                response.Description = _messagesDefault.FatalErrorMessage;
            }
            return response;
        }

        public async Task<Response<string>> AddUser(Users user)
        {
            Response<string> response = new();
            try
            {
    
                Dictionary<string, dynamic> parameters = new();
                parameters.Add("@P_OPERATION ", "INS");
                parameters.Add("@P_PASSWORD_USER", user.passwordUser);
                parameters.Add("@P_FIRST_NAME", user.firstName);
                parameters.Add("@P_SURNAME", user.surName);
                parameters.Add("@P_SECOND_SURNAME", user.secondSurName);
                parameters.Add("@P_DIRECTION", user.direction);
                parameters.Add("@P_PHONE_NUMBER", user.phoneNumber);
                parameters.Add("@P_DNI", user.dni);
                parameters.Add("@P_EMAIL", user.email);
                parameters.Add("@P_BUSINESS_NAME", user.businessName);
                parameters.Add("@P_NIT", user.nit);
                parameters.Add("@P_ID_ROL", user.idRol);
                parameters.Add("@P_USER_ID", null);
                parameters.Add("@P_SEARCH", null);
                parameters.Add("@P_STATUS_USER", null);

                var responseBD = await _skynetRepository.CallSP(_storedProcedure, parameters);

                response.Code = responseBD.Code;
                response.Description = "Usuario ingresado correctamente";


            }
            catch (Exception ex)
            {
                response.Code = ResponseCode.FatalError;
                _logService.SaveLogApp($"[{nameof(AddUser)} - {nameof(Exception)}] {ex.Message} | {ex.StackTrace}", LogType.Error);
                response.Description = _messagesDefault.FatalErrorMessage;
            }
            return response;
        }

        public async Task<Response<string>> UpdateUser(Users user)
        {
            Response<string> response = new();
            try
            {

                Dictionary<string, dynamic> parameters = new();
                parameters.Add("@P_OPERATION ", "UPD");
                parameters.Add("@P_PASSWORD_USER", user.passwordUser);
                parameters.Add("@P_FIRST_NAME", user.firstName);
                parameters.Add("@P_SURNAME", user.surName);
                parameters.Add("@P_SECOND_SURNAME", user.secondSurName);
                parameters.Add("@P_DIRECTION", user.direction);
                parameters.Add("@P_PHONE_NUMBER", user.phoneNumber);
                parameters.Add("@P_DNI", user.dni);
                parameters.Add("@P_EMAIL", user.email);
                parameters.Add("@P_BUSINESS_NAME", user.businessName);
                parameters.Add("@P_NIT", user.nit);
                parameters.Add("@P_ID_ROL", user.idRol);
                parameters.Add("@P_USER_ID", user.idUser);
                parameters.Add("@P_SEARCH", null);
                parameters.Add("@P_STATUS_USER", user.statusUser);

                var responseBD = await _skynetRepository.CallSP(_storedProcedure, parameters);

                response.Code = responseBD.Code;
                response.Description = "Usuario actualizado correctamente";


            }
            catch (Exception ex)
            {
                response.Code = ResponseCode.FatalError;
                _logService.SaveLogApp($"[{nameof(AddUser)} - {nameof(Exception)}] {ex.Message} | {ex.StackTrace}", LogType.Error);
                response.Description = _messagesDefault.FatalErrorMessage;
            }
            return response;
        }

        public async Task<Response<string>> RemoveUser(int USER_ID,string STATUS_USER)
        {
            Response<string> response = new();
            try
            {

                Dictionary<string, dynamic> parameters = new();
                parameters.Add("@P_OPERATION ", "DIU");
                parameters.Add("@P_PASSWORD_USER", null);
                parameters.Add("@P_FIRST_NAME", null);
                parameters.Add("@P_SURNAME", null);
                parameters.Add("@P_SECOND_SURNAME", null);
                parameters.Add("@P_DIRECTION", null);
                parameters.Add("@P_PHONE_NUMBER", null);
                parameters.Add("@P_DNI", null);
                parameters.Add("@P_EMAIL", null);
                parameters.Add("@P_BUSINESS_NAME", null);
                parameters.Add("@P_NIT", null);
                parameters.Add("@P_ID_ROL", null);
                parameters.Add("@P_USER_ID", USER_ID);
                parameters.Add("@P_SEARCH", null);
                parameters.Add("@P_STATUS_USER", STATUS_USER);

                var responseBD = await _skynetRepository.CallSP(_storedProcedure, parameters);

                response.Code = responseBD.Code;
                response.Description = "Usuario eliminado correctamente";


            }
            catch (Exception ex)
            {
                response.Code = ResponseCode.FatalError;
                _logService.SaveLogApp($"[{nameof(AddUser)} - {nameof(Exception)}] {ex.Message} | {ex.StackTrace}", LogType.Error);
                response.Description = _messagesDefault.FatalErrorMessage;
            }
            return response;
        }

        public async Task<Response<List<Users>>> GetAllTechnicals()
        {
            Response<List<Users>> response = new();
            List<Users> users = new();
            try
            {
                Dictionary<string, dynamic> bdParameters = new();
                bdParameters.Add("@P_OPERATION ", "GAT");
                bdParameters.Add("@P_PASSWORD_USER", null);
                bdParameters.Add("@P_FIRST_NAME", null);
                bdParameters.Add("@P_SURNAME", null);
                bdParameters.Add("@P_SECOND_SURNAME", null);
                bdParameters.Add("@P_DIRECTION", null);
                bdParameters.Add("@P_PHONE_NUMBER", null);
                bdParameters.Add("@P_DNI", null);
                bdParameters.Add("@P_EMAIL", null);
                bdParameters.Add("@P_BUSINESS_NAME", null);
                bdParameters.Add("@P_NIT", null);
                bdParameters.Add("@P_ID_ROL", null);
                bdParameters.Add("@P_USER_ID", null);
                bdParameters.Add("@P_SEARCH", null);
                bdParameters.Add("@P_STATUS_USER", null);
                var responseBd = await _skynetRepository.CallSPData(_storedProcedure, bdParameters);

                response.Code = responseBd.Code;
                if (responseBd.Code == ResponseCode.Success)
                {
                    foreach (DataRow dr in responseBd.Data.Rows)
                    {
                        users.Add(new Users
                        {
                            idUser = (int)dr["ID_USER"],
                            passwordUser = (string)dr["PASSWORD_USER"],
                            firstName = (string)dr["FIRST_NAME"],
                            surName = (string)dr["SURNAME"],
                            secondSurName = (string)dr["SECOND_SURNAME"],
                            direction = (string)dr["DIRECTION"],
                            phoneNumber = (int)dr["PHONE_NUMBER"],
                            dni = (string)dr["DNI"],
                            email = (string)dr["EMAIL"],
                            businessName = (string)dr["BUSINESS_NAME"],
                            nit = (int)dr["NIT"],
                            statusUser = (string)dr["STATUS_USER"],
                            idRol = (int)dr["ID_ROL"],
                            rolName = (string)dr["ROL_NAME"],
                            userName = (string)dr["USER_NAME"],
                            statusName = (string)dr["NAME_STATUS"]
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
                _logService.SaveLogApp($"{nameof(GetAllUsers)} {nameof(Exception)} {ex.Message} | {ex.StackTrace}", LogType.Error);
                response.Description = _messagesDefault.FatalErrorMessage;
            }
            return response;
        }

        public async Task<Response<List<Users>>> GetAllSupervisors()
        {
            Response<List<Users>> response = new();
            List<Users> users = new();
            try
            {
                Dictionary<string, dynamic> bdParameters = new();
                bdParameters.Add("@P_OPERATION ", "GAS");
                bdParameters.Add("@P_PASSWORD_USER", null);
                bdParameters.Add("@P_FIRST_NAME", null);
                bdParameters.Add("@P_SURNAME", null);
                bdParameters.Add("@P_SECOND_SURNAME", null);
                bdParameters.Add("@P_DIRECTION", null);
                bdParameters.Add("@P_PHONE_NUMBER", null);
                bdParameters.Add("@P_DNI", null);
                bdParameters.Add("@P_EMAIL", null);
                bdParameters.Add("@P_BUSINESS_NAME", null);
                bdParameters.Add("@P_NIT", null);
                bdParameters.Add("@P_ID_ROL", null);
                bdParameters.Add("@P_USER_ID", null);
                bdParameters.Add("@P_SEARCH", null);
                bdParameters.Add("@P_STATUS_USER", null);
                var responseBd = await _skynetRepository.CallSPData(_storedProcedure, bdParameters);

                response.Code = responseBd.Code;
                if (responseBd.Code == ResponseCode.Success)
                {
                    foreach (DataRow dr in responseBd.Data.Rows)
                    {
                        users.Add(new Users
                        {
                            idUser = (int)dr["ID_USER"],
                            passwordUser = (string)dr["PASSWORD_USER"],
                            firstName = (string)dr["FIRST_NAME"],
                            surName = (string)dr["SURNAME"],
                            secondSurName = (string)dr["SECOND_SURNAME"],
                            direction = (string)dr["DIRECTION"],
                            phoneNumber = (int)dr["PHONE_NUMBER"],
                            dni = (string)dr["DNI"],
                            email = (string)dr["EMAIL"],
                            businessName = (string)dr["BUSINESS_NAME"],
                            nit = (int)dr["NIT"],
                            statusUser = (string)dr["STATUS_USER"],
                            idRol = (int)dr["ID_ROL"],
                            rolName = (string)dr["ROL_NAME"],
                            userName = (string)dr["USER_NAME"],
                            statusName = (string)dr["NAME_STATUS"]
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
                _logService.SaveLogApp($"{nameof(GetAllUsers)} {nameof(Exception)} {ex.Message} | {ex.StackTrace}", LogType.Error);
                response.Description = _messagesDefault.FatalErrorMessage;
            }
            return response;
        }
    }
}
