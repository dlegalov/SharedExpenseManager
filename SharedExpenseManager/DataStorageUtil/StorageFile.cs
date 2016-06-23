using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Security;

using SharedExpenseManager.Users;
using SharedExpenseManager.Expenses;


namespace SharedExpenseManager.DataStorageUtil
{
    [DataContract]
    public class StorageFile // Used to store data between sessions
    {
        // Using non-interface classes due to limitations of serialization library
        // Should use a mutex or other lock to prevent simultaneous write and read, to have accurate calculation
        // With long term storage, actually keep calculated scores, for this will calculate on load to reduce complexity

        public delegate void StorageFileUpdateHandler();

        public event StorageFileUpdateHandler StorageFileUpdateEvent;

        [DataMember]
        private List<long> m_admins; // Users id's with admin priveleges
        [DataMember]
        private List<User> m_userList; // All users, including admins
        [DataMember]
        private List<Expense> m_expenses; // All expenses being tracked

        public StorageFile()
        {
            m_admins = new List<long>();
            m_userList = new List<User>();
            m_expenses = new List<Expense>();
        }

        // FOR TEST PURPOSES, adds some users
        public StorageFile(bool test) : this()
        {
            if (!test)
            {
                return;
            }
            m_admins.Add(1L);
            m_admins.Add(4L);

            m_userList.Add(new User(4L, "admin", "admin", "admin", @"admin@test.com", "admin"));
            m_userList.Add(new User(1L, "John", "Doe", "JohnDoe", @"JohnDoe@test.com", "test"));
            m_userList.Add(new User(2L, "Jane", "Doe", "JaneDoe", @"JaneDoe@test.com", "test"));
            m_userList.Add(new User(3L, "Bob", "Doe", "BobDoe", @"BobDoe@test.com", ""));
        }

        public List<long> Admins
        {
            get { return m_admins; }
        }

        public List<User> UserList
        {
            get { return m_userList; }
        }

        public List<Expense> Expenses
        {
            get { return m_expenses; }
        }

        public bool AddUser(string firstName, string lastName, string loginName, string email, string password)
        {
            if (CheckForUser(loginName)) // user exists
            {
                return false;
            }
            m_userList.Add(new User(GetNextId(), firstName, lastName, loginName, email, password));

            this.StorageFileChanged();
            return true;
        }

        public void AddAdmin(User user)
        {
            m_admins.Add(user.Id);

            this.StorageFileChanged();
        }

        public void AddExpense(Expense expense)
        {
            m_expenses.Add(expense);

            this.StorageFileChanged();
        }

        // Need removal or editing of expenses, only allow admins to do so
        public bool CheckForAdmin(User user)
        {
            return m_admins.Contains(user.Id);
        }

        public bool CheckForUser(string loginName)
        {
            if (m_userList.Select(x => x.LoginName).Contains(loginName))
            {
                return true;
            }
            return false;
        }

        public bool CheckUserPassword(string loginName, string password, out User user)
        {
            user = m_userList.Where(x => x.LoginName == loginName).First();
            return user.CheckPassword(password);
        }

        public long GetNextId()
        {
            return m_userList.Select(x => x.Id).Max() + 1L;
        }

        public void StorageFileChanged()
        {
            if(this.StorageFileUpdateEvent != null)
            {
                StorageFileUpdateEvent();
            }
        }
    }
}
