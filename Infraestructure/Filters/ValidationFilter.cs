using Core;
using Core.Enumerations;
using Core.Intefaces;
using Core.Models;
using Core.Models.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace Infraestructure.Filters
{
    public class ValidationFilter : IActionFilter
    {
        private readonly IConfigurationService _configurationService;

        private readonly MessagesDefault _messagesDefault;
        public ValidationFilter(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
            _messagesDefault = _configurationService.Get<MessagesDefault>(Configuration.MessagesDefault);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new OkObjectResult(new Response<string> { Code = ResponseCode.Error, Description = _messagesDefault.InvalidParameters });
                return;
            }

        }
    }
}
