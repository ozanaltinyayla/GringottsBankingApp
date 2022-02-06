namespace GringottsBankingApp.API.Dtos
{
    public class AccountsWithUserDto : AccountDto
    {
        public UserDto User { get; set; }
    }
}
