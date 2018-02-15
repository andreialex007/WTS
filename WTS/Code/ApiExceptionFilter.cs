using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WTS.BL.Exceptions;

namespace WTS.Code
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidationException ex)
            {
                context.Result = new JsonResult(new
                {
                    ex.Errors,
                    ex.AllErrors,
                    HasErrors = true
                });

                context.HttpContext.Response.StatusCode = 500;
            }
            base.OnException(context);
        }
    }
}
