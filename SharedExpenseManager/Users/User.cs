using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Runtime.Serialization;

using SharedExpenseManager.DataStorageUtil;

namespace SharedExpenseManager.Users
{
    [DataContract]
    public class User
    {
        [DataMember]
        private readonly long m_id; // Should be greater than 0 for any user, 0 is null user
        [DataMember]
        private string m_firstName;
        [DataMember]
        private string m_lastName;
        [DataMember]
        private string m_loginName;
        [DataMember]
        private string m_email;
        [DataMember]
        private string m_passHash;

        public User() // Null user
        {
            m_id = 0;
            m_firstName = string.Empty;
            m_lastName = string.Empty;
            m_loginName = string.Empty;
            m_email = string.Empty;
            m_passHash = string.Empty;
        }

        public User(long id, string firstName, string lastName, string loginName, string email, string password) : this()
        {
            m_id = id;
            m_firstName = firstName;
            m_lastName = lastName;
            m_loginName = loginName;
            m_email = email;
            m_passHash = DataStorageUtil.PassHash.CreatePassHash(password);
        }

        public long Id
        {
            get { return m_id; }
        }

        public string FirstName
        {
            get { return m_firstName; }
        }

        public string LastName
        {
            get { return m_lastName; }
        }

        public string LoginName
        {
            get { return m_loginName; }
        }

        public string Email
        {
            get { return m_email; }
        }

        public string PassHash
        {
            get { return m_passHash; }
        }

        public bool CheckPassword(string passwordInput)
        {
            return DataStorageUtil.PassHash.CheckPassword(passwordInput, m_passHash);
        }

        public bool ChangePassword(string passwordInput, string passwordNew)
        {
            if (!DataStorageUtil.PassHash.CheckPassword(passwordInput, m_passHash))
            {
                return false; // User did not give correct original password
            }

            m_passHash = DataStorageUtil.PassHash.CreatePassHash(passwordNew);
            return true;
        }

        private bool CheckNullUser()
        {
            if (m_id > 0)
            {
                return true;
            }
            return false;
        }
    }
}
