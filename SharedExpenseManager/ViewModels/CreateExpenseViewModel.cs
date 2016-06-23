using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SharedExpenseManager.Expenses;
using SharedExpenseManager.Users;
using SharedExpenseManager.DataStorageUtil;
using SharedExpenseManager.Utility;

namespace SharedExpenseManager.ViewModels
{
    public class CreateExpenseViewModel : ViewModelBase
    {
        private object m_lockObject = new object();

        private double m_value;

        private DateTime m_dateTime;

        private User m_payingUser;

        private List<SelectedUser> m_userSelectionList;

        private List<SelectedUser> m_paySelectionList;

        private StorageFile.StorageFileUpdateHandler m_storageFileUpdateHandler;

        public CreateExpenseViewModel()
        {
            m_storageFileUpdateHandler = new StorageFile.StorageFileUpdateHandler(OnStorageFileUpdate);
            AppController.StorageFileUpdateEventAddHandler(m_storageFileUpdateHandler);
            this.CreateSelectedUserList();
            m_payingUser = new User();
        }

        ~CreateExpenseViewModel()
        {
            AppController.StorageFileUpdateEventRemoveHandler(m_storageFileUpdateHandler);
        }

        public User CurrentUser
        {
            get
            {
                return AppController.LoggedInUser;
            }
        }

        public List<SelectedUser> SelectedUserList
        {
            get
            {
                return m_userSelectionList;
            }
            set
            {
                m_userSelectionList = value;
                OnPropertyChanged("SelectedUserList");
            }
        }

        public List<SelectedUser> PaySelectionList
        {
            get
            {
                return m_paySelectionList;
            }
            set
            {
                m_paySelectionList = value;
                OnPropertyChanged("PaySelectionList");
            }
        }

        public double Value
        {
            get
            {
                return m_value;
            }
            set
            {
                m_value = value;
                OnPropertyChanged("Value");
            }
        }

        public DelegateCommand NewUserCommand
        {
            get { return new DelegateCommand(OnNewUserCommand); }
        }

        public DelegateCommand ClearCommand
        {
            get { return new DelegateCommand(OnClearCommand); }
        }

        public DelegateCommand SubmitCommand
        {
            get { return new DelegateCommand(OnSubmitCommand); }
        }

        public DelegateCommand<bool> UserSelectionCheckedCommand
        {
            get { return new DelegateCommand<bool>(OnUserSelectionCheckBoxChecked); }
        }

        public DelegateCommand<User> PayerSelectionCheckedCommand
        {
            get { return new DelegateCommand<User>(OnPayerSelectionCheckBoxChecked); }
        }

        private void OnNewUserCommand()
        {

        }

        private void OnClearCommand()
        {
            this.CreateSelectedUserList();
            Value = 0;
        }

        private void OnSubmitCommand()
        {

        }

        private void LoadUsers()
        {

        }

        private void OnStorageFileUpdate()
        {
            // Logic for when storage file has an update (new user/expense)
        }

        private void CreateSelectedUserList()
        {
            m_userSelectionList = new List<SelectedUser>();
            foreach (var user in AppController.StorageFile.UserList)
            {
                if (user.Id != AppController.LoggedInUser.Id)
                {
                    m_userSelectionList.Add(new SelectedUser(user));
                }
            }
            OnPropertyChanged("SelectedUserList");
        }

        private void OnUserSelectionCheckBoxChecked(bool isChecked)
        {
            m_paySelectionList = new List<SelectedUser>();
            m_paySelectionList.Add(new SelectedUser(AppController.LoggedInUser));
            foreach (var selectedUser in SelectedUserList)
            {
                if (selectedUser.Selected)
                {
                    m_paySelectionList.Add(new SelectedUser(selectedUser.User));
                }
            }
            OnPropertyChanged("PaySelectionList");
        }

        private void OnPayerSelectionCheckBoxChecked(User user)
        {
            m_payingUser = user;
            foreach (var selectedUser in PaySelectionList)
            {
                if (selectedUser.User.Id != m_payingUser.Id)
                {
                    selectedUser.Selected = false;
                }
            }
        }
    }

    public class SelectedUser : NotifyPropertyChangedBase
    {
        private User m_user;
        private bool m_selected;

        public SelectedUser(User user)
            : this(user, false)
        {
        }

        public SelectedUser(User user, bool selected)
        {
            m_user = user;
            m_selected = selected;
        }

        public User User
        {
            get
            {
                return m_user;
            }
            set
            {
                m_user = value;
                OnPropertyChanged("User");
            }
        }

        public string FirstName
        {
            get
            {
                return m_user.FirstName;
            }
        }

        public string LastName
        {
            get
            {
                return m_user.LastName;
            }
        }

        public string LoginName
        {
            get
            {
                return m_user.LoginName;
            }
        }

        public bool Selected
        {
            get
            {
                return m_selected;
            }
            set
            {
                m_selected = value;
                OnPropertyChanged("Selected");
            }
        }
    }
}