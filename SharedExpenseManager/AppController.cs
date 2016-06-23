using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharedExpenseManager.Users;
using SharedExpenseManager.DataStorageUtil;
using SharedExpenseManager.Expenses;

namespace SharedExpenseManager
{
    public static class AppController // Loads data, logs in user, and initiates calculations. Used to check state of program
    {
        public enum AppView
        {
            Login,
            NewUserSetup,
            UserScreen,
        }

        public enum LoginState
        {
            LogIn = 0,
            LogOut = 1,
        }

        private static bool m_loggedInFlag;
        private static bool m_adminUserFlag;
        private static User m_loggedInUser;
        private static StorageFile m_storageFile;

        private static AppView m_currentAppView;

        public delegate void LoginHandler(LoginState state);

        public delegate void AppViewHandler(AppView view);

        public delegate void StatusChangeHandler(string status);

        public static event LoginHandler LoginEvent; // Fire event if a login happens

        public static event AppViewHandler AppViewChangeEvent; // Fire event if app screen changes

        public static event StatusChangeHandler StatusChangeEvent; // Fire event when want to change status display

        static AppController()
        {
            m_currentAppView = AppView.Login;
            m_loggedInFlag = false;
            m_adminUserFlag = false;
            m_loggedInUser = new User();
            m_storageFile = new StorageFile();
        }

        public static bool LoggedInFlag
        {
            get
            {
                return m_loggedInFlag;
            }
        }

        public static bool AdminUserFlag
        {
            get
            {
                return m_adminUserFlag;
            }
        }

        public static User LoggedInUser
        {
            get
            {
                return m_loggedInUser;
            }
        }

        public static void Load()
        {
            //var m_storageFile = DataStorage.GetInstance.LoadStorageFile();
            // FOR TEST
            m_storageFile = new StorageFile(true);

        }

        public static StorageFile StorageFile
        {
            get
            {
                return m_storageFile;
            }
        }

        public static void Save()
        {
            DataStorage.GetInstance.SaveStorageFile(new StorageFile(true)); // Currently uses a test-specific constructor
        }

        public static bool Login(string username, string password)
        {
            // Check if user exists
            if (!m_storageFile.CheckForUser(username))
            {
                ChangeStatus(string.Format(@"No user ""{0}"" found.", username));
                return false;
            }

            User user;
            if ( !m_storageFile.CheckUserPassword(username, password, out user))
            {
                ChangeStatus("Incorrect password.");
                return false;
            }

            m_loggedInUser = user;
            if (m_storageFile.CheckForAdmin(user))
            {
                m_adminUserFlag = true;
            }

            RaiseLoginEvent(AppController.LoginState.LogIn);
            ChangeView(AppController.AppView.UserScreen);

            if (m_adminUserFlag)
            {
                ChangeStatus(string.Format(@"Logged in as administrator ""{0}"".", username));
            }
            else
            {
                ChangeStatus(string.Format(@"Logged in as user ""{0}"".", username));
            }
            return true;
        }

        public static void Logout()
        {

        }

        public static void ChangeView(AppView view)
        {
            m_currentAppView = view;
            if (AppViewChangeEvent != null)
            {
                AppViewChangeEvent(m_currentAppView); // Raise event
            }
        }

        public static void RaiseLoginEvent(AppController.LoginState state)
        {
            if (LoginEvent != null)
            {
                LoginEvent(state);
            }
        }

        public static void ChangeStatus(string status)
        {
            if (StatusChangeEvent != null)
            {
                StatusChangeEvent(status);
            }
        }

        public static bool AddNewUser(string firstName, string lastName, string loginName, string email, string password)
        {
            if (m_storageFile.AddUser(firstName, lastName, loginName, email, password))
            {
                ChangeStatus(string.Format(@"User ""{0}"" added.", loginName));
                return true;
            }
            ChangeStatus(string.Format(@"User ""{0}"" already exists.", loginName));
            return false;
        }

        public static bool AddExpense(Expense expense)
        {
            m_storageFile.AddExpense(expense);
            return true;
        }

        public static void StorageFileUpdateEventAddHandler(StorageFile.StorageFileUpdateHandler handler)
        {
            // Should have more reliable subscription system
            m_storageFile.StorageFileUpdateEvent += handler;
        }

        public static void StorageFileUpdateEventRemoveHandler(StorageFile.StorageFileUpdateHandler handler)
        {
            // Should have more reliable subscription system
            m_storageFile.StorageFileUpdateEvent -= handler;
        }
    }
}
