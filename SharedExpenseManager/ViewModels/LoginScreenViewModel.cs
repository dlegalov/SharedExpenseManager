using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SharedExpenseManager.ViewModels
{
    public class LoginScreenViewModel : ViewModelBase
    {
        private string m_loginName;
        private string m_password;

        public string LoginName
        {
            get 
            {
                return m_loginName;
            }
            set 
            {
                m_loginName = value;
                this.OnPropertyChanged("LoginName");
            }
        }

        public DelegateCommand LoginCommand
        {
            get
            {
                return new DelegateCommand(passwordBox =>
                {
                    if (!(passwordBox is PasswordBox))
                    {
                        return;
                    }
                    var password = ((PasswordBox)passwordBox).Password;
                    //MessageBox.Show(LoginName + Environment.NewLine + password);
                    //AppController.ChangeView(AppController.AppView.UserScreen);
                    AppController.Login(m_loginName, password);
                });
            }
        }

        public DelegateCommand NewUserCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    AppController.ChangeView(AppController.AppView.NewUserSetup);
                });
            }
        }
    }
}
