using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedExpenseManager.ViewModels
{
    public class UserScreenViewModel : ViewModelBase
    {
        public bool AdminTabVisibility
        {
            get
            {
                return AppController.AdminUserFlag;
            }
        }
    }
}
