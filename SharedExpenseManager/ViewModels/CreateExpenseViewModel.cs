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

        private string m_valueString;

        private DateTime m_date;

        private DateTime m_time;

        private DateTime m_dateTime;

        private User m_payingUser;

        private List<SelectedUser> m_userSelectionList;

        private List<SelectedUser> m_paySelectionList;

        private string m_description;

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
                return double.Parse(m_valueString);
            }
            set
            {
                m_value = value;
                OnPropertyChanged("Value");
            }
        }

        public string ValueString
        {
            get
            {
                return m_valueString;
            }
            set
            {
                m_valueString = value;
                OnPropertyChanged("ValueString");
            }
        }

        public string Description
        {
            get
            {
                return m_description;
            }
            set
            {
                m_description = value;
                OnPropertyChanged("Description");
            }
        }

        public DateTime Time
        {
            get
            {
                return m_time;
            }
            set
            {
                m_time = value;
                OnPropertyChanged("Time");
            }
        }

        public DateTime Date
        {
            get
            {
                return m_time;
            }
            set
            {
                m_time = value;
                OnPropertyChanged("Date");
            }
        }

        private DateTime DateTime
        {
            get
            {
                return new DateTime(Date.Year, Date.Month, Date.Day, Time.Hour, Time.Minute, Time.Second);
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
            // Logic to add a new user
        }

        private void OnClearCommand()
        {
            this.CreateSelectedUserList();
            this.OnUserSelectionCheckBoxChecked(false);
            Description = string.Empty;
            ValueString = string.Empty;
        }

        private void OnSubmitCommand()
        {
            AppController.AddExpense(CreateExpense());
            OnClearCommand();
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

        private Expense CreateExpense()
        {
            return new Expense(m_payingUser, m_paySelectionList.Select(x => x.User).ToList(), DateTime, Value, Description);
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