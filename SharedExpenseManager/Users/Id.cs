using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedExpenseManager.Users
{
    public struct Id
    {
        private long m_value;

        public static implicit operator Id(long value)
        {
            return new Id { m_value = value };
        }

        public static implicit operator long(Id value)
        {
            return value.m_value;
        }
    }
}
