using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using SharedExpenseManager.Views;

namespace SharedExpenseManager.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ContentControl m_windowContent;

        private string m_title;
        private string m_status;

        public MainWindowViewModel()
        {
            m_windowContent = new LoginScreenView();
            //m_windowContent = new NewUserSetupView();
            m_title = "Shared Expense Manager";
            m_status = "Ready";
            AppController.AppViewChangeEvent += this.AppViewChangedEvent;
            AppController.StatusChangeEvent += this.OnStatusChangeEvent;
        }

        public ContentControl WindowContent // Window content is just a bound user control that is loaded with the right data before presentation. This way the window can be kept the same without closing
        {
            get
            {
                return m_windowContent;
            }
            set 
            {
                m_windowContent = value;
                OnPropertyChanged("WindowContent");
            }
        }

        public string Title
        {
            get
            {
                return m_title;
            }
            set
            {
                m_title = value;
                OnPropertyChanged("Title");
            }
        }

        public string Status
        {
            get
            {
                return m_status;
            }
            set
            {
                m_status = value;
                OnPropertyChanged("Status");
            }
        }

        public void AppViewChangedEvent(AppController.AppView view)
        {
            switch (view)
            {
                case AppController.AppView.Login:
                    WindowContent = new LoginScreenView();
                    break;
                case AppController.AppView.NewUserSetup:
                    WindowContent = new NewUserSetupView();
                    break;
                case AppController.AppView.UserScreen:
                    WindowContent = new UserScreenView();
                    break;
                default:
                    break;
            }
        }

        private void OnStatusChangeEvent(string status)
        {
            Status = status;
        }
    }
}
