using Core.Enumerations;
using Core.Intefaces;
using Core.Models;
using Core.Models.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Infraestructure.Filters
{
    public class AuthenticationFilter : IActionFilter
    {
        private readonly IConfigurationService _configurationService;

        private readonly MessagesDefault _messagesDefault;
        private readonly Security _security;
        public AuthenticationFilter(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
            _messagesDefault = _configurationService.Get<MessagesDefault>(Configuration.MessagesDefault);
            _security = _configurationService.Get<Security>(Configuration.Security);
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            string authKey = context.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == "KeyAuth").Value.FirstOrDefault();

            Response<string> responseFailure = new() { Code = ResponseCode.Error, Description = _messagesDefault.AuthInvalidMessage };

            if (string.IsNullOrEmpty(authKey))
            {
                context.Result = new OkObjectResult(responseFailure);
                return;
            }
            if (authKey != _security.AuthKey)
            {
                context.Result = new OkObjectResult(responseFailure);
                return;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}
