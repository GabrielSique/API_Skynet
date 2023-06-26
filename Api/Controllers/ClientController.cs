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
    public class ClientController : ControllerBase
    {
        private readonly IClientService _client;
        private readonly ILogService _logService;
        private readonly IParseService _parseService;
        private readonly IMapper _mapper;
        public ClientController(IClientService client, ILogService logService, IParseService parseService, IMapper mapper)
        {
            _client = client;
            _logService = logService;
            _parseService = parseService;
            _mapper = mapper;
        }

        [HttpGet("GetAllClients")]
        public async Task<IActionResult> GetAllClients()
        {
            _logService.SaveLogApp($"Request {nameof(ClientController)} - {nameof(GetAllClients)} ", LogType.Information);
            var response = await _client.GetAllClients();
            _logService.SaveLogApp($"Response {nameof(ClientController)} - {nameof(GetAllClients)} : {_parseService.Serialize(response)} ", LogType.Information);

            return Ok(response);
        }
        [HttpGet("GetAllClientsByFilter")]
        public async Task<IActionResult> GetAllClientsByFilter([FromBody] UsersDto request)
        {
            _logService.SaveLogApp($"Request {nameof(ClientController)} - {nameof(GetAllClientsByFilter)} ", LogType.Information);
            var response = await _client.GetAllClientsByFilter(request.userName);
            _logService.SaveLogApp($"Response {nameof(ClientController)} - {nameof(GetAllClientsByFilter)} : {_parseService.Serialize(response)} ", LogType.Information);

            return Ok(response);
        }

        [ServiceFilter(typeof(ValidationFilter))]
        [HttpPost("AddClient")]
        public async Task<IActionResult> AddClient([FromBody] ClientDto request)
        {
            _logService.SaveLogApp($"Request {nameof(ClientController)} - {nameof(AddClient)} ", LogType.Information);
            var client = _mapper.Map<Client>(request);
            var response = await _client.AddClient(client);
            _logService.SaveLogApp($"Response {nameof(ClientController)} - {nameof(AddClient)} : {_parseService.Serialize(response)} ", LogType.Information);
            return Ok(response);
        }

        [ServiceFilter(typeof(ValidationFilter))]
        [HttpPut("UpdateClient")]
        public async Task<IActionResult> UpdateClient([FromBody] ClientDto request)
        {
            _logService.SaveLogApp($"Request {nameof(ClientController)} - {nameof(UpdateClient)} ", LogType.Information);
            var client = _mapper.Map<Client>(request);
            var response = await _client.UpdateClient(client);
            _logService.SaveLogApp($"Response {nameof(ClientController)} - {nameof(UpdateClient)} : {_parseService.Serialize(response)} ", LogType.Information);
            return Ok(response);
        }

        [HttpDelete("RemoveClient")]
        public async Task<IActionResult> RemoveClient([FromBody] ClientDto request)
        {
            _logService.SaveLogApp($"Request {nameof(ClientController)} - {nameof(RemoveClient)} ", LogType.Information);
            var client = _mapper.Map<Client>(request);
            var response = await _client.RemoveClient(client.idClient, client.statusClient);
            _logService.SaveLogApp($"Response {nameof(ClientController)} - {nameof(RemoveClient)} : {_parseService.Serialize(response)} ", LogType.Information);
            return Ok(response);
        }

    }
}
