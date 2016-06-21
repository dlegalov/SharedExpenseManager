using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SharedExpenseManager.ViewModels
{
    class NewUserSetupViewModel : ViewModelBase
    {
        private string m_firstName;
        private string m_lastName;
        private string m_loginName;
        private string m_email;
        private string m_password;
        private string m_passwordRetype;

        public string FirstName
        {
            get 
            { 
                return m_firstName;
            }
            set 
            { 
                m_firstName = value;
                this.OnPropertyChanged("FirstName");
            }
        }
        

        public string LastName
        {
            get 
            { 
                return m_lastName; 
            }
            set 
            { 
                m_lastName = value;
                this.OnPropertyChanged("LastName");
            }
        }

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
        
        public string Email
        {
            get 
            {
                return m_email;
            }
            set 
            { 
                m_email = value;
                this.OnPropertyChanged("Email");
            }
        }
        
        public string Password
        {
            get 
            { 
                return m_password;
            }
            set 
            { 
                m_password = value;
                this.OnPropertyChanged("Password");
            }
        }

        public string PasswordRetype
        {
            get
            {
                return m_passwordRetype;
            }
            set
            {
                m_passwordRetype = value;
                this.OnPropertyChanged("Password");
            }
        }

        public DelegateCommand<object[]> SubmitButtonCommand
        {
            get
            {
                return new DelegateCommand<object[]>(values =>
                {
                    if (!(values is object[]))
                    {
                        return;
                    }
                    var password = ((PasswordBox)values[0]).Password;
                    var passwordRetype = ((PasswordBox)values[1]).Password;

                    if (password != passwordRetype)
                    {
                        AppController.ChangeStatus("Passwords do not match.");
                        return;
                    }

                    if (AppController.AddNewUser(FirstName, LastName, LoginName, Email, password))
                    {
                        AppController.ChangeView(AppController.AppView.Login);
                    }
                });
            }
        }

        public DelegateCommand BackToLoginCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    AppController.ChangeView(AppController.AppView.Login);
                });
            }
        }
    }
}
