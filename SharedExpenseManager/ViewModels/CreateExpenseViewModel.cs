using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedExpenseManager.Expenses;
using SharedExpenseManager.Users;
using SharedExpenseManager.DataStorageUtil;

namespace SharedExpenseManager.ViewModels
{
    public class CreateExpenseViewModel : ViewModelBase
    {
        private List<User> m_availableUserList;
        private List<User> m_selectedUserList;

        private double m_value;
        private DateTime m_dateTime;

        private User m_availableUserSelection;
        private User m_selectedUserSelection;

        private Dictionary<User, bool> m_selectedUserDict;

        private StorageFile.StorageFileUpdateHandler m_storageFileUpdateHandler;

        public CreateExpenseViewModel()
        {
            m_storageFileUpdateHandler = new StorageFile.StorageFileUpdateHandler(OnStorageFileUpdate);
            AppController.StorageFileUpdateEventAddHandler(m_storageFileUpdateHandler);
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

        public DelegateCommand SelectUserCommand
        {
            get { return new DelegateCommand(OnSelectUserCommand); }
        }

        public DelegateCommand RemoveUserCommand
        {
            get { return new DelegateCommand(OnRemoveUserCommand); }
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

        private void OnSelectUserCommand()
        {

        }

        private void OnRemoveUserCommand()
        {

        }

        private void OnNewUserCommand()
        {

        }

        private void OnClearCommand()
        {

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

        private void CreateSelectedUserDict()
        {
            m_selectedUserDict = new Dictionary<User, bool>();
            foreach (var user in AppController.StorageFile.UserList)
            {
                m_selectedUserDict.Add(user, false);
            }
        }
    }
}