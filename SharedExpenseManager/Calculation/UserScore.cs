using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharedExpenseManager.Users;
using SharedExpenseManager.Expenses;
using SharedExpenseManager.DataStorageUtil;

namespace SharedExpenseManager.Calculation
{
    // Each user will have a tabulated user score, do it on login for specific user
    public class UserScore // Used to store calculated scores for a user in relationship to the users they have a score with. This is like the table in the project overview
    {
        private long m_userId; // The user requesting their score
        private Dictionary<long, double> m_scoreDict; // User Id, value of those who have a score with user

        public UserScore()
        {
            m_scoreDict = new Dictionary<long, double>();
        }

        public long User
        {
            get { return m_userId; }
        }

        public Dictionary<long, double> ScoreDict
        {
            get { return m_scoreDict; }
        }

        public void AddScore(User user, double amount) // Add to tally, amount can be +/-
        {
            if (m_userId == user.Id) // User cannot have amount with self, ignore
            {
                return; // Later add system to return different values if the operation was a success
            }

            if (!m_scoreDict.ContainsKey(user.Id)) // No entry for score, add a new one
            {
                m_scoreDict.Add(user.Id, amount);
                return;
            }

            m_scoreDict[user.Id] += amount;
        }

        public void AddScore(List<User> users, double amount)
        {
            foreach (var user in users)
            {
                AddScore(user, amount);
            }
        }

        // Every time a new expense is added, this needs to be used
    }
}
