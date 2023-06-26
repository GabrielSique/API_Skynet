using Core.Enumerations;
using Core.Intefaces;
using Core.Models;
using Core.Models.Configuration;
using Core.Models.Dtos;
using Core.Models.Entities;
using System.Data;
using System.Net.Sockets;

namespace Core.Services
{
    public class AssignamentVisitService : IAssignmentVisitsService
    {
        private static readonly string _storedProcedure = "sp_assignament_visits";
        private readonly ILogService _logService;
        private readonly ISkynetRepository _skynetRepository;
        private readonly IConfigurationService _configurationService;
        private readonly ICryptoService _cryptoService;
        private readonly MessagesDefault _messagesDefault;
        public AssignamentVisitService(ISkynetRepository skynetRepository, ILogService logService, IConfigurationService configurationService, ICryptoService cryptoService)
        {
            _configurationService = configurationService;
            _skynetRepository = skynetRepository;
            _logService = logService;
            _cryptoService = cryptoService;
            _messagesDefault = _configurationService.Get<MessagesDefault>(Configuration.MessagesDefault);

        }
        public async Task<Response<string>> AddVisit(AssignmentVisits avisit)
        {
            Response<string> response = new();
            try
            {

                Dictionary<string, dynamic> parameters = new();
                parameters.Add("@P_OPERATION ", "INS");
                parameters.Add("@P_ID_VISIT_ASSIGNED", null);
                parameters.Add("@P_ID_TECHNICAL", avisit.idTechnical);
                parameters.Add("@P_ID_CLIENT", avisit.idClient);
                parameters.Add("@P_UBICATION", avisit.ubication);
                parameters.Add("@P_REASON_VISIT", avisit.reasonVisit);
                parameters.Add("@P_STATUS_VISIT", avisit.statusVisit);
                parameters.Add("@P_VISIT_SCHEDULE", avisit.visitSchedule);
                parameters.Add("@P_ID_SUPERVISOR", avisit.idSupervisor);
                parameters.Add("@P_ARRIVAL_VISIT", avisit.arrivalVisit);
                parameters.Add("@P_VISIT_FINISHED", avisit.visitFinished);

                var responseBD = await _skynetRepository.CallSP(_storedProcedure, parameters);

                response.Code = responseBD.Code;
                response.Description = "Asignación ingresada correctamente";


            }
            catch (Exception ex)
            {
                response.Code = ResponseCode.FatalError;
                _logService.SaveLogApp($"[{nameof(AddVisit)} - {nameof(Exception)}] {ex.Message} | {ex.StackTrace}", LogType.Error);
                response.Description = _messagesDefault.FatalErrorMessage;
            }
            return response;
        }

        public async Task<Response<List<AssignmentVisitsDto>>> GetAllVisits()
        {
            Response<List<AssignmentVisitsDto>> response = new();
            List<AssignmentVisitsDto> users = new();
            try
            {
                Dictionary<string, dynamic> bdParameters = new();
                bdParameters.Add("@P_OPERATION ", "GAV");
                bdParameters.Add("@P_ID_VISIT_ASSIGNED", null);
                bdParameters.Add("@P_ID_TECHNICAL", null);
                bdParameters.Add("@P_ID_CLIENT", null);
                bdParameters.Add("@P_UBICATION", null);
                bdParameters.Add("@P_REASON_VISIT", null);
                bdParameters.Add("@P_STATUS_VISIT", null);
                bdParameters.Add("@P_VISIT_SCHEDULE", null);
                bdParameters.Add("@P_ID_SUPERVISOR", null);
                bdParameters.Add("@P_ARRIVAL_VISIT", null);
                bdParameters.Add("@P_VISIT_FINISHED", null);
                var responseBd = await _skynetRepository.CallSPData(_storedProcedure, bdParameters);

                response.Code = responseBd.Code;
                if (responseBd.Code == ResponseCode.Success)
                {
                    foreach (DataRow dr in responseBd.Data.Rows)
                    {
                        users.Add(new AssignmentVisitsDto
                        {
                            idVisitAssigned = (int)dr["ID_VISIT_ASSIGNED"],
                            idTechnical = (int)dr["ID_TECHNICAL"],
                            idClient = (int)dr["ID_CLIENT"],
                            ubication = (string)dr["UBICATION"],
                            reasonVisit = (string)dr["REASON_VISIT"],
                            statusVisit = (int)dr["STATUS_VISIT"],
                            visitSchedule = Convert.ToDateTime(dr["VISIT_SCHEDULE"]).ToString(),
                            idSupervisor = (int)dr["ID_SUPERVISOR"],
                            arrivalVisit = Convert.ToDateTime(dr["ARRIVAL_VISIT"]).ToString(),
                            visitFinished = Convert.ToDateTime(dr["VISIT_FINISHED"]).ToString(),
                            registerDate = Convert.ToDateTime(dr["REGISTER_DATE"]).ToString(),
                            nameTechnical = (string)dr["NAME_TECHNICAL"],
                            nameSupervisor = (string)dr["NAME_SUPERVISOR"],
                            nameClient = (string)dr["NAME_CLIENT"],
                            nameStatusVisit = (string)dr["NAME_STATUS"]
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
                _logService.SaveLogApp($"{nameof(GetAllVisits)} {nameof(Exception)} {ex.Message} | {ex.StackTrace}", LogType.Error);
                response.Description = _messagesDefault.FatalErrorMessage;
            }
            return response;
        }

        public Task<Response<List<AssignmentVisits>>> GetAllVisitsByFilter(string filter)
        {
            throw new NotImplementedException();
        }

        public Task<Response<string>> RemoveVisit(int idAssignamentVisit, int statusVisit)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<string>> UpdateVisit(AssignmentVisitsDto avisit)
        {
            Response<string> response = new();
            try
            {

                Dictionary<string, dynamic> parameters = new();
                parameters.Add("@P_OPERATION ", "UPD");
                parameters.Add("@P_ID_VISIT_ASSIGNED", avisit.idVisitAssigned);
                parameters.Add("@P_ID_TECHNICAL", avisit.idTechnical);
                parameters.Add("@P_ID_CLIENT", avisit.idClient);
                parameters.Add("@P_UBICATION", avisit.ubication);
                parameters.Add("@P_REASON_VISIT", avisit.reasonVisit);
                parameters.Add("@P_STATUS_VISIT", avisit.statusVisit);
                parameters.Add("@P_VISIT_SCHEDULE", avisit.visitSchedule);
                parameters.Add("@P_ID_SUPERVISOR", avisit.idSupervisor);
                parameters.Add("@P_ARRIVAL_VISIT", avisit.arrivalVisit);
                parameters.Add("@P_VISIT_FINISHED", avisit.visitFinished);

                var responseBD = await _skynetRepository.CallSP(_storedProcedure, parameters);

                response.Code = responseBD.Code;
                response.Description = "Asignación de visita actualizada correctamente";


            }
            catch (Exception ex)
            {
                response.Code = ResponseCode.FatalError;
                _logService.SaveLogApp($"[{nameof(UpdateVisit)} - {nameof(Exception)}] {ex.Message} | {ex.StackTrace}", LogType.Error);
                response.Description = _messagesDefault.FatalErrorMessage;
            }
            return response;
        }
    }
}
