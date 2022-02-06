using GringottsBankingApp.API.Dtos;
using GringottsBankingApp.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace GringottsBankingApp.API.Filters
{
    public class AccountNotFoundFilter : ActionFilterAttribute
    {
        private readonly IAccountService _accountService;

        public AccountNotFoundFilter(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var id = (int)context.ActionArguments.Values.FirstOrDefault();

            var account = await _accountService.GetByIdAsync(id);

            if (account != null)
            {
                await next();
            }

            var errorDto = new ErrorDto();

            errorDto.Status = 404;

            errorDto.Errors.Add($"The account with Id = {id} was not found in the database");

            context.Result = new NotFoundObjectResult(errorDto);
        }
    }
}
