using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SharedExpenseManager
{
    public class DelegateCommand : DelegateCommand<object>
    {
        public DelegateCommand(Action action) : base(action)
        {
        }

        public DelegateCommand(Action<object> action) : base(action)
        {
        }
    }

    public class DelegateCommand<T> : ICommand
    {
        protected readonly Action m_action;

        protected readonly Action<T> m_actionParam;

        protected readonly bool m_parameterFlag;

        public DelegateCommand(Action action)
        {
            m_parameterFlag = false;
            m_action = action;
        }

        public DelegateCommand(Action<T> action)
        {
            m_parameterFlag = true;
            m_actionParam = action;
        }

        public void Execute(object parameter)
        {
            if (m_parameterFlag)
            {
                m_actionParam((T)parameter);
                return;
            }
            m_action();
        }

        public bool CanExecute(object parameter)
        {
            if (m_parameterFlag)
            {
                return m_actionParam != null ? true : false;
            }
            return m_action != null ? true : false;
        }

        public event EventHandler CanExecuteChanged;
    }
}
