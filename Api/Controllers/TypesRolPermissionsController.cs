using AutoMapper;
using Core.Enumerations;
using Core.Intefaces;
using Core.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace Api.Controllers
{
    [Route("/api/[Controller]")]
    [ApiController]
    public class TypesRolPermissionsController : ControllerBase
    {
        private readonly ITypesRolePermissions _roles;
        private readonly ILogService _logService;
        private readonly IParseService _parseService;
        private readonly IMapper _mapper;
        public TypesRolPermissionsController(ITypesRolePermissions roles, ILogService logService, IParseService parseService, IMapper mapper)
        {
            _roles = roles;
            _logService = logService;
            _parseService = parseService;
            _mapper = mapper;
        }
        [HttpGet("GetAllTypesRoles")]
        public async Task<IActionResult> GetAllTypesRoles()
        {
            _logService.SaveLogApp($"Request {nameof(AuthenticatorController)} - {nameof(GetAllTypesRoles)} ", LogType.Information);
            var response = await _roles.GetAllTypesRoles();
            _logService.SaveLogApp($"Response {nameof(AuthenticatorController)} - {nameof(GetAllTypesRoles)} : {_parseService.Serialize(response)} ", LogType.Information);

            return Ok(response);
        }
    }
}
