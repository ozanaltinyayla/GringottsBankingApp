using GringottsBankingApp.API.Dtos;
using GringottsBankingApp.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace GringottsBankingApp.API.Filters
{
    public class UserNotFoundFilter : ActionFilterAttribute
    {
        private readonly IUserService _userService;

        public UserNotFoundFilter(IUserService userService)
        {
            _userService = userService;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var id = (int)context.ActionArguments.Values.FirstOrDefault();

            var user = await _userService.GetByIdAsync(id);

            if (user != null)
            {
                await next();
            }

            var errorDto = new ErrorDto();

            errorDto.Status = 404;

            errorDto.Errors.Add($"The user with Id = {id} was not found in the database");

            context.Result = new NotFoundObjectResult(errorDto);
        }
    }
}
