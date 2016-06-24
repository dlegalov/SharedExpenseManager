using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace SharedExpenseManager.Controls
{
    /// <summary>
    /// Interaction logic for TimeSelector.xaml
    /// </summary>
    public partial class TimeSelector : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty DateTimeProperty = DependencyProperty.Register("DateTime", typeof(DateTime), typeof(TimeSelector), null);

        public enum Period
        {
            AM,
            PM,
        }
        private DateTime m_dateTime;

        private int m_hour;
        private int m_minute;
        private int m_second;
        private Period m_period;

        public int Hour
        {
            get
            {
                return m_hour;
            }
            set
            {
                m_hour = value;
                this.OnPropertyChanged("Hour");
            }
        }

        public int Minute
        {
            get
            {
                return m_minute;
            }
            set
            {
                m_minute = value;
                this.OnPropertyChanged("Minute");
            }
        }

        public int Second
        {
            get
            {
                return m_second;
            }
            set
            {
                m_second = value;
                this.OnPropertyChanged("Second");
            }
        }

        public Period SelectedPeriod
        {
            get
            {
                return m_period;
            }
            set
            {
                m_period = value;
                this.OnPropertyChanged("Period");
                this.OnPropertyChanged("AMSelected");
                this.OnPropertyChanged("PMSelected");
            }
        }

        public bool AMSelected
        {
            get
            {
                return (m_period == TimeSelector.Period.AM);
            }
            set
            {
                if (value)
                {
                    SelectedPeriod = Period.AM;
                }
            }
        }

        public bool PMSelected
        {
            get
            {
                return (m_period == TimeSelector.Period.PM);
            }
            set
            {
                if (value)
                {
                    SelectedPeriod = Period.PM;
                }
            }
        }

        public DateTime DateTime
        {
            get
            {
                DateTime = new DateTime(0, 0, 0, SelectedPeriod == Period.AM ? Hour : Hour + 12, Minute, Second);
                return (DateTime)GetValue(DateTimeProperty);
            }
            set
            {
                SetValue(DateTimeProperty, value);
            }
        }

        public TimeSelector()
        {
            InitializeComponent();
            this.OnReset();
            this.OnSetCurrent();
        }

        public DelegateCommand ResetCommand
        {
            get { return new DelegateCommand(OnReset); }
        }

        public DelegateCommand SetCurrentCommand
        {
            get { return new DelegateCommand(OnSetCurrent); }
        }

        private void OnReset()
        {
            SelectedPeriod = Period.AM;
            Hour = Minute = Second = 0;
        }

        private void OnSetCurrent()
        {
            m_dateTime = DateTime.Now;
            SelectedPeriod = m_dateTime.Hour > 11 ? Period.PM : Period.AM;
            Hour = m_dateTime.Hour > 12 ? m_dateTime.Hour - 12 : m_dateTime.Hour;
            Minute = m_dateTime.Minute;
            Second = m_dateTime.Second;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        #endregion
    }
}
