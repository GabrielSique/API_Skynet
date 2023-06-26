using Core.Enumerations;
using Core.Intefaces;
using Core.Models;
using Core.Models.Configuration;
using Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ClientService : IClientService
    {
        private static readonly string _storedProcedure = "sp_clients";
        private readonly ILogService _logService;
        private readonly ISkynetRepository _skynetRepository;
        private readonly IConfigurationService _configurationService;
        private readonly ICryptoService _cryptoService;
        private readonly MessagesDefault _messagesDefault;
        public ClientService(ISkynetRepository skynetRepository, ILogService logService, IConfigurationService configurationService, ICryptoService cryptoService)
        {
            _configurationService = configurationService;
            _skynetRepository = skynetRepository;
            _logService = logService;
            _cryptoService = cryptoService;
            _messagesDefault = _configurationService.Get<MessagesDefault>(Configuration.MessagesDefault);

        }
        public async Task<Response<string>> AddClient(Client client)
        {
            Response<string> response = new();
            try
            {

                Dictionary<string, dynamic> parameters = new();
                parameters.Add("@P_OPERATION ", "INS");
                parameters.Add("@P_FIRST_NAME", client.firstName);
                parameters.Add("@P_SURNAME", client.surName);
                parameters.Add("@P_SECOND_SURNAME", client.secondSurName);
                parameters.Add("@P_DIRECTION", client.direction);
                parameters.Add("@P_PHONE_NUMBER", client.phoneNumber);
                parameters.Add("@P_DNI", client.dni);
                parameters.Add("@P_EMAIL", client.email);
                parameters.Add("@P_BUSINESS_NAME", client.businessName);
                parameters.Add("@P_NIT", client.nit);
                parameters.Add("@P_CLIENT_ID", null);
                parameters.Add("@P_SEARCH", null);
                parameters.Add("@P_STATUS_CLIENT", null);

                var responseBD = await _skynetRepository.CallSP(_storedProcedure, parameters);

                response.Code = responseBD.Code;
                response.Description = "Cliente ingresado correctamente";


            }
            catch (Exception ex)
            {
                response.Code = ResponseCode.FatalError;
                _logService.SaveLogApp($"[{nameof(AddClient)} - {nameof(Exception)}] {ex.Message} | {ex.StackTrace}", LogType.Error);
                response.Description = _messagesDefault.FatalErrorMessage;
            }
            return response;
        }

        public async Task<Response<List<Client>>> GetAllClients()
        {
            Response<List<Client>> response = new();
            List<Client> users = new();
            try
            {
                Dictionary<string, dynamic> bdParameters = new();
                bdParameters.Add("@P_OPERATION ", "GAU");
                bdParameters.Add("@P_FIRST_NAME", null);
                bdParameters.Add("@P_SURNAME", null);
                bdParameters.Add("@P_SECOND_SURNAME", null);
                bdParameters.Add("@P_DIRECTION", null);
                bdParameters.Add("@P_PHONE_NUMBER", null);
                bdParameters.Add("@P_DNI", null);
                bdParameters.Add("@P_EMAIL", null);
                bdParameters.Add("@P_BUSINESS_NAME", null);
                bdParameters.Add("@P_NIT", null);
                bdParameters.Add("@P_CLIENT_ID", null);
                bdParameters.Add("@P_SEARCH", null);
                bdParameters.Add("@P_STATUS_CLIENT", null);
                var responseBd = await _skynetRepository.CallSPData(_storedProcedure, bdParameters);

                response.Code = responseBd.Code;
                if (responseBd.Code == ResponseCode.Success)
                {
                    foreach (DataRow dr in responseBd.Data.Rows)
                    {
                        users.Add(new Client
                        {
                            idClient = (int)dr["ID_CLIENT"],
                            firstName = (string)dr["FIRST_NAME"],
                            surName = (string)dr["SURNAME"],
                            secondSurName = (string)dr["SECOND_SURNAME"],
                            direction = (string)dr["DIRECTION"],
                            phoneNumber = (int)dr["PHONE_NUMBER"],
                            dni = (string)dr["DNI"],
                            email = (string)dr["EMAIL"],
                            businessName = (string)dr["BUSINESS_NAME"],
                            nit = (int)dr["NIT"],
                            statusClient = (string)dr["STATUS_CLIENT"],
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
                _logService.SaveLogApp($"{nameof(GetAllClients)} {nameof(Exception)} {ex.Message} | {ex.StackTrace}", LogType.Error);
                response.Description = _messagesDefault.FatalErrorMessage;
            }
            return response;
        }

        public async Task<Response<List<Client>>> GetAllClientsByFilter(string filter)
        {
            Response<List<Client>> response = new();
            List<Client> users = new();
            try
            {
                Dictionary<string, dynamic> bdParameters = new();
                bdParameters.Add("@P_OPERATION ", "SRU");
                bdParameters.Add("@P_FIRST_NAME", null);
                bdParameters.Add("@P_SURNAME", null);
                bdParameters.Add("@P_SECOND_SURNAME", null);
                bdParameters.Add("@P_DIRECTION", null);
                bdParameters.Add("@P_PHONE_NUMBER", null);
                bdParameters.Add("@P_DNI", null);
                bdParameters.Add("@P_EMAIL", null);
                bdParameters.Add("@P_BUSINESS_NAME", null);
                bdParameters.Add("@P_NIT", null);
                bdParameters.Add("@P_CLIENT_ID", null);
                bdParameters.Add("@P_SEARCH", filter);
                bdParameters.Add("@P_STATUS_CLIENT", null);

                var responseBd = await _skynetRepository.CallSPData(_storedProcedure, bdParameters);

                response.Code = responseBd.Code;
                if (responseBd.Code == ResponseCode.Success)
                {
                    foreach (DataRow dr in responseBd.Data.Rows)
                    {
                        users.Add(new Client
                        {
                            idClient = (int)dr["ID_CLIENT"],
                            firstName = (string)dr["FIRST_NAME"],
                            surName = (string)dr["SURNAME"],
                            secondSurName = (string)dr["SECOND_SURNAME"],
                            direction = (string)dr["DIRECTION"],
                            phoneNumber = (int)dr["PHONE_NUMBER"],
                            dni = (string)dr["DNI"],
                            email = (string)dr["EMAIL"],
                            businessName = (string)dr["BUSINESS_NAME"],
                            nit = (int)dr["NIT"],
                            statusClient = (string)dr["STATUS_CLIENT"],
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
                _logService.SaveLogApp($"{nameof(GetAllClientsByFilter)} {nameof(Exception)} {ex.Message} | {ex.StackTrace}", LogType.Error);
                response.Description = _messagesDefault.FatalErrorMessage;
            }
            return response;
        }

        public async Task<Response<string>> RemoveClient(int ID_CLIENT, string STATUS_CLIENT)
        {
            Response<string> response = new();
            try
            {

                Dictionary<string, dynamic> parameters = new();
                parameters.Add("@P_OPERATION ", "DIU");
                parameters.Add("@P_FIRST_NAME", null);
                parameters.Add("@P_SURNAME", null);
                parameters.Add("@P_SECOND_SURNAME", null);
                parameters.Add("@P_DIRECTION", null);
                parameters.Add("@P_PHONE_NUMBER", null);
                parameters.Add("@P_DNI", null);
                parameters.Add("@P_EMAIL", null);
                parameters.Add("@P_BUSINESS_NAME", null);
                parameters.Add("@P_NIT", null);
                parameters.Add("@P_USER_ID", ID_CLIENT);
                parameters.Add("@P_SEARCH", null);
                parameters.Add("@P_STATUS_USER", STATUS_CLIENT);

                var responseBD = await _skynetRepository.CallSP(_storedProcedure, parameters);

                response.Code = responseBD.Code;
                response.Description = "Cliente eliminado correctamente";


            }
            catch (Exception ex)
            {
                response.Code = ResponseCode.FatalError;
                _logService.SaveLogApp($"[{nameof(RemoveClient)} - {nameof(Exception)}] {ex.Message} | {ex.StackTrace}", LogType.Error);
                response.Description = _messagesDefault.FatalErrorMessage;
            }
            return response;
        }

        public async Task<Response<string>> UpdateClient(Client client)
        {
            Response<string> response = new();
            try
            {

                Dictionary<string, dynamic> parameters = new();
                parameters.Add("@P_OPERATION ", "UPD");
                parameters.Add("@P_FIRST_NAME", client.firstName);
                parameters.Add("@P_SURNAME", client.surName);
                parameters.Add("@P_SECOND_SURNAME", client.secondSurName);
                parameters.Add("@P_DIRECTION", client.direction);
                parameters.Add("@P_PHONE_NUMBER", client.phoneNumber);
                parameters.Add("@P_DNI", client.dni);
                parameters.Add("@P_EMAIL", client.email);
                parameters.Add("@P_BUSINESS_NAME", client.businessName);
                parameters.Add("@P_NIT", client.nit);
                parameters.Add("@P_CLIENT_ID", client.idClient);
                parameters.Add("@P_SEARCH", null);
                parameters.Add("@P_STATUS_CLIENT", client.statusClient);

                var responseBD = await _skynetRepository.CallSP(_storedProcedure, parameters);

                response.Code = responseBD.Code;
                response.Description = "Usuario actualizado correctamente";


            }
            catch (Exception ex)
            {
                response.Code = ResponseCode.FatalError;
                _logService.SaveLogApp($"[{nameof(UpdateClient)} - {nameof(Exception)}] {ex.Message} | {ex.StackTrace}", LogType.Error);
                response.Description = _messagesDefault.FatalErrorMessage;
            }
            return response;
        }
    }
}
