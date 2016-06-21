using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using SharedExpenseManager.Users;

namespace SharedExpenseManager.Expenses
{
    [DataContract]
    public class Expense : IExpense // Will be used to calculate debts
    {
        [DataMember]
        private readonly User m_userPayer;
        [DataMember]
        private readonly List<User> m_usersInTransaction;
        [DataMember]
        private readonly DateTime m_timestamp;
        [DataMember]
        private readonly double m_amount;
        [DataMember]
        private string m_description;
        [DataMember]
        private bool m_processedFlag;    // For reduction in calculation time, use flag to indicate which expenses have been added to user totals, and only run through those that have yet to be added

        public Expense(User userPayer, List<User> usersInTransaction, DateTime timestamp, double amount, string description)
        {
            m_processedFlag = false;

            m_userPayer = userPayer;
            m_usersInTransaction = usersInTransaction;
            m_timestamp = timestamp;
            m_amount = amount;
            m_description = description;
        }

        public User UserPayer
        {
            get { return m_userPayer; }
        }

        public List<User> UsersInTransaction
        {
            get { return m_usersInTransaction; }
        }

        public DateTime Timestamp
        {
            get { return m_timestamp; }
        }

        public double Amount
        {
            get { return m_amount; }
        }

        public string Description
        {
            get { return m_description; }
        }

        public bool Processed
        {
            get { return m_processedFlag; }
        }
    }
}
