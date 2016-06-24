using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharedExpenseManager.Expenses;

namespace SharedExpenseManager.ViewModels
{
    public class UserScreenViewModel : ViewModelBase
    {
        private List<Expense> m_expenseList;

        public UserScreenViewModel()
        {
            m_expenseList = new List<Expense>();
        }

        public bool AdminTabVisibility
        {
            get
            {
                return AppController.AdminUserFlag;
            }
        }

        public List<Expense> ExpenseList
        {

        }
    }
}
