using HueFestivalTicket.Models;

namespace HueFestivalTicket.Data
{
    public class AccountDTO
    {
        public string? Username { get; set; }
        public string? Password { get; set; }

        public static AccountDTO FromAccount(Account account)
        {
            return new AccountDTO
            {
                Username = account.Username,
                Password = account.Password
            };
        }
    }
}
