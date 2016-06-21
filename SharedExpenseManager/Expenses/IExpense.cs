using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharedExpenseManager.Users;

namespace SharedExpenseManager.Expenses
{
    interface IExpense // Use for transactions
    {
        User UserPayer { get; }
        List<User> UsersInTransaction { get; }
        DateTime Timestamp { get; }
        double Amount { get; }
    }
}
