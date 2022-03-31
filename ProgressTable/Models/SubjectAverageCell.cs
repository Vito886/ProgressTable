using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProgressTable.Models
{
    public class SubjectAverageCell : INotifyPropertyChanged
    {
        string grade;
        string color;
        public event PropertyChangedEventHandler PropertyChanged;

        public SubjectAverageCell()
        {
            grade = "0";
            color = "Red";
        }

        private void PropertyChangeNotification([CallerMemberName] String property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public string Grade
        {
            get => grade;
            set
            {
                grade = value;
                try
                {
                    if (Convert.ToDouble(grade) < 1)
                        Color = "Red";
                    else if (Convert.ToDouble(grade) < 1.5)
                        Color = "Yellow";
                    else
                        Color = "LightGreen";
                    PropertyChangeNotification();
                }
                catch
                {

                }
            }
        }

        public string Color
        {
            get => color;
            set
            {
                color = value;
                PropertyChangeNotification();
            }
        }
    }
}
