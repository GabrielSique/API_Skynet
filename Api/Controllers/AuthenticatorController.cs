using Core.Enumerations;
using Core.Intefaces;
using Core.Models.Dtos;
using Infraestructure.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Api.Controllers
{
    [Route("/api/[Controller]")]
    [ApiController]
    public class AuthenticatorController : ControllerBase
    {
        private readonly IAuthenticatorService _authenticator;
        private readonly ILogService _logService;
        private readonly IParseService _parseService;
        public AuthenticatorController(IAuthenticatorService authenticator, ILogService logService, IParseService parseService/*, IMapper mapper*/)
        {
            _authenticator = authenticator;
            _logService = logService;
            _parseService = parseService;
            //_mapper = mapper;
        }
            
        //[ServiceFilter(typeof(AuthenticationFilter))]
        [ServiceFilter(typeof(ValidationFilter))]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AuthenticatorDto request)
        {
            _logService.SaveLogApp($"Request {nameof(AuthenticatorController)} - {nameof(Login)} ", LogType.Information);
            var response = await _authenticator.Login(request.email,request.password);
            _logService.SaveLogApp($"Response {nameof(AuthenticatorController)} - {nameof(Login)} : {_parseService.Serialize(response)} ", LogType.Information);

            return Ok(response);
        }
    }
}
