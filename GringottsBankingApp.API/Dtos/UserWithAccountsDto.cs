using System.Collections.Generic;

namespace GringottsBankingApp.API.Dtos
{
    public class UserWithAccountsDto : UserDto
    {
        public ICollection<AccountDto> Accounts { get; set; }
    }
}
