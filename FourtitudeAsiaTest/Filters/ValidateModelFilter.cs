using FourtitudeAsiaTest.Loggers;
using FourtitudeAsiaTest.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace FourtitudeAsiaTest.Middleware
{
    public class ValidateModelFilter : IActionFilter
    {
        private readonly IAppLogger<ValidateModelFilter> _logger;

        public ValidateModelFilter(IAppLogger<ValidateModelFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var customResponse = new BaseResponse
                {
                    Result = 0,
                    Resultmessage = context.ModelState.Select(x => x.Value?.Errors.FirstOrDefault()?.ErrorMessage).FirstOrDefault()
                };

                context.Result = new JsonResult(customResponse)
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };

                _logger.LogWarning($"Invalid model state detected, req:{JsonConvert.SerializeObject(customResponse)}");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
