using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using FluentValidation;
using molecule.infrastructure.data.interfaces.MoleculesException;

namespace molecules.api.Filter
{
    /// <summary>
    /// Will postprocess the exception of a controller action
    /// </summary>
    public class MoleculesExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// process the exception
        /// </summary>
        /// <param name="context">The exception context</param>
        public void OnException(ExceptionContext context)
        {
            GetLogger(context)?.LogError(context.Exception, "An exception was handle by the global exception handler");
            if ( context.Exception is ValidationException validationException)
            {
                context.Result = new UnprocessableEntityObjectResult(new ServiceValidationError()
                {
                    ValidationErrors = validationException
                                        .Errors
                                        .Select(x => new ServiceValidationErrorItem(x.ErrorMessage,x.PropertyName))
                                        .ToList()
                });
            }
            else if (context.Exception is MoleculesResourceNotFoundException moleculesResourceNotFoundException)
            {
                context.Result = new NotFoundObjectResult(new ServiceError()
                {
                    DisplayMessage = "The requested resource was not found"
                });
            }
            else
            {
                context.Result = new ObjectResult(new ServiceError())
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            
            context.ExceptionHandled = true;
        }


        private ILogger<MoleculesExceptionFilter>? GetLogger(ExceptionContext context)
        {
            var loggerFactory = context.HttpContext.RequestServices.GetService(typeof(ILoggerFactory)) as ILoggerFactory;
            return loggerFactory?.CreateLogger<MoleculesExceptionFilter>();
        }
    }
}
