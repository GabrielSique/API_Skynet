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
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _users;
        private readonly ILogService _logService;
        private readonly IParseService _parseService;
        private readonly IMapper _mapper;
        public UsersController(IUsersService users, ILogService logService, IParseService parseService, IMapper mapper)
        {
            _users = users;
            _logService = logService;
            _parseService = parseService;
            _mapper = mapper;
        }
        //[ServiceFilter(typeof(AuthenticationFilter))]
        //[ServiceFilter(typeof(ValidationFilter))]
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            _logService.SaveLogApp($"Request {nameof(AuthenticatorController)} - {nameof(GetAllUsers)} ", LogType.Information);
            var response = await _users.GetAllUsers();
            _logService.SaveLogApp($"Response {nameof(AuthenticatorController)} - {nameof(GetAllUsers)} : {_parseService.Serialize(response)} ", LogType.Information);

            return Ok(response);
        }
        [HttpGet("GetAllUsersByFilter")]
        public async Task<IActionResult> GetAllUsersByFilter([FromBody] UsersDto request)
        {
            _logService.SaveLogApp($"Request {nameof(AuthenticatorController)} - {nameof(GetAllUsersByFilter)} ", LogType.Information);
            var response = await _users.GetAllUsersByFilter(request.userName);
            _logService.SaveLogApp($"Response {nameof(AuthenticatorController)} - {nameof(GetAllUsersByFilter)} : {_parseService.Serialize(response)} ", LogType.Information);

            return Ok(response);
        }

        [ServiceFilter(typeof(ValidationFilter))]
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] UsersDto request)
        {
            _logService.SaveLogApp($"Request {nameof(UsersController)} - {nameof(AddUser)} ", LogType.Information);
            var user = _mapper.Map<Users>(request);
            var response = await _users.AddUser(user);
            _logService.SaveLogApp($"Response {nameof(UsersController)} - {nameof(AddUser)} : {_parseService.Serialize(response)} ", LogType.Information);
            return Ok(response);
        }

        [ServiceFilter(typeof(ValidationFilter))]
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UsersDto request)
        {
            _logService.SaveLogApp($"Request {nameof(UsersController)} - {nameof(UpdateUser)} ", LogType.Information);
            var user = _mapper.Map<Users>(request);
            var response = await _users.UpdateUser(user);
            _logService.SaveLogApp($"Response {nameof(UsersController)} - {nameof(UpdateUser)} : {_parseService.Serialize(response)} ", LogType.Information);
            return Ok(response);
        }

        [HttpDelete("RemoveUser")]
        public async Task<IActionResult> RemoveUser([FromBody] UsersDto request)
        {
            _logService.SaveLogApp($"Request {nameof(UsersController)} - {nameof(RemoveUser)} ", LogType.Information);
            var user = _mapper.Map<Users>(request);
            var response = await _users.RemoveUser(request.idUser, user.statusUser);
            _logService.SaveLogApp($"Response {nameof(UsersController)} - {nameof(RemoveUser)} : {_parseService.Serialize(response)} ", LogType.Information);
            return Ok(response);
        }

        [HttpGet("GetAllTechnicals")]
        public async Task<IActionResult> GetAllTechnicals()
        {
            _logService.SaveLogApp($"Request {nameof(AuthenticatorController)} - {nameof(GetAllTechnicals)} ", LogType.Information);
            var response = await _users.GetAllTechnicals();
            _logService.SaveLogApp($"Response {nameof(AuthenticatorController)} - {nameof(GetAllTechnicals)} : {_parseService.Serialize(response)} ", LogType.Information);

            return Ok(response);
        }

        [HttpGet("GetAllSupervisors")]
        public async Task<IActionResult> GetAllSupervisors()
        {
            _logService.SaveLogApp($"Request {nameof(AuthenticatorController)} - {nameof(GetAllSupervisors)} ", LogType.Information);
            var response = await _users.GetAllSupervisors();
            _logService.SaveLogApp($"Response {nameof(AuthenticatorController)} - {nameof(GetAllSupervisors)} : {_parseService.Serialize(response)} ", LogType.Information);

            return Ok(response);
        }

    }
}
