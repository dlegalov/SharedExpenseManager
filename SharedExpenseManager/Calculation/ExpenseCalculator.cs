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
    public class ExpenseCalculator
    {
        // User id, UserScore
        private Dictionary<long, UserScore> m_userScoreDict; // Dictionary of all user scores with those they have a score with. This is strange/complicated with .NET serialization, could try to store as list of keypairs and load into dict on startup

        public ExpenseCalculator()
        {
            m_userScoreDict = new Dictionary<long, UserScore>();
        }

        public void CalcUserScore(User user, StorageFile storageFile) // Count up score for a given user
        {
            if (!m_userScoreDict.ContainsKey(user.Id)) // Check if the user has a score dict
            {
                m_userScoreDict.Add(user.Id, new UserScore()); // Add user with empty score table
            }

            foreach (var expense in storageFile.Expenses) // Add check if expense has already been processed to prevent constant growth in loading times
            {
                if (user.Id == expense.UserPayer.Id) // The user paid, this will be added as positive
                {
                    m_userScoreDict[user.Id].AddScore(expense.UsersInTransaction, expense.Amount);
                }
                else if (expense.UsersInTransaction.Select(x => x.Id).Contains(user.Id)) // The user was paid for, this is subtracted from their score with payer
                {
                    m_userScoreDict[user.Id].AddScore(expense.UserPayer, -(expense.Amount));
                }
            }
        }

        public UserScore GetUserScore(User user) // Get the score for a given user
        {
            if (!m_userScoreDict.ContainsKey(user.Id)) // Check that user has a score
            {
                m_userScoreDict.Add(user.Id, new UserScore()); // Add user with empty score table
            }
            return m_userScoreDict[user.Id];
        }
    }
}
