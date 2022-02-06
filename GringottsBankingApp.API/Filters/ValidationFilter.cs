using GringottsBankingApp.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace GringottsBankingApp.API.Filters
{
    public class ValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                ErrorDto errorDto = new();

                errorDto.Status = 400;

                IEnumerable<ModelError> modelError = context.ModelState.Values.SelectMany(v => v.Errors);

                modelError.ToList().ForEach(x =>
                {
                    errorDto.Errors.Add(x.ErrorMessage);
                });

                context.Result = new BadRequestObjectResult(errorDto);
            }
        }
    }
}
