using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;

namespace SharedExpenseManager.Users
{
    interface IUser // Keep data for user here, but track debts only in data storage
    {
        long Id { get; } // For long term, make seperate id class, to make sure right value is passed. Should remain static even if other user properites change over time
        string FirstName { get; }
        string LastName { get; }
        string LoginName { get; }
        string Email { get; }
        //SecureString Password { get; }
        string Password { get; }
    }
}
