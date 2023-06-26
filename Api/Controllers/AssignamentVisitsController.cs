using AutoMapper;
using Core.Enumerations;
using Core.Intefaces;
using Core.Models.Dtos;
using Core.Models.Entities;
using Infraestructure.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("/api/[Controller]")]
    [ApiController]
    public class AssignamentVisitsController : ControllerBase
    {
        private readonly IAssignmentVisitsService _avisits;
        private readonly ILogService _logService;
        private readonly IParseService _parseService;
        private readonly IMapper _mapper;
        public AssignamentVisitsController(IAssignmentVisitsService avisits, ILogService logService, IParseService parseService, IMapper mapper)
        {
            _avisits = avisits;
            _logService = logService;
            _parseService = parseService;
            _mapper = mapper;
        }

        [ServiceFilter(typeof(ValidationFilter))]
        [HttpPost("AddVisit")]
        public async Task<IActionResult> AddVisit([FromBody] AssignmentVisitsDto request)
        {
            _logService.SaveLogApp($"Request {nameof(AssignamentVisitsController)} - {nameof(AddVisit)} ", LogType.Information);
            var visit = _mapper.Map<AssignmentVisits>(request);
            var response = await _avisits.AddVisit(visit);
            _logService.SaveLogApp($"Response {nameof(AssignamentVisitsController)} - {nameof(AddVisit)} : {_parseService.Serialize(response)} ", LogType.Information);
            return Ok(response);
        }

        [HttpGet("GetAllVisits")]
        public async Task<IActionResult> GetAllVisits()
        {
            _logService.SaveLogApp($"Request {nameof(AssignamentVisitsController)} - {nameof(GetAllVisits)} ", LogType.Information);
            var response = await _avisits.GetAllVisits();
            _logService.SaveLogApp($"Response {nameof(AssignamentVisitsController)} - {nameof(GetAllVisits)} : {_parseService.Serialize(response)} ", LogType.Information);

            return Ok(response);
        }

        [ServiceFilter(typeof(ValidationFilter))]
        [HttpPut("UpdateVisit")]
        public async Task<IActionResult> UpdateVisit([FromBody] AssignmentVisitsDto request)
        {
            _logService.SaveLogApp($"Request {nameof(AssignamentVisitsController)} - {nameof(UpdateVisit)} ", LogType.Information);
            var visit = request;
            var response = await _avisits.UpdateVisit(visit);
            _logService.SaveLogApp($"Response {nameof(AssignamentVisitsController)} - {nameof(UpdateVisit)} : {_parseService.Serialize(response)} ", LogType.Information);
            return Ok(response);
        }
    }
}
