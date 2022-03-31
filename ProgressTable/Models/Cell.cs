using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProgressTable.Models
{
    public class Cell : INotifyPropertyChanged
    {
        string name;
        string color = "Red";
        string grade;
        public event PropertyChangedEventHandler PropertyChanged;

        public Cell(string _name, string _grade = "0")
        {
            Name = _name;
            Grade = _grade;
        }

        private void PropertyChangeNotification([CallerMemberName] String property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        private void SetColor()
        {
            try
            {
                if (Convert.ToDouble(grade) == 0)
                    Color = "Red";
                else if (Convert.ToDouble(grade) == 1)
                    Color = "Yellow";
                else if (Convert.ToDouble(grade) == 2)
                    Color = "LightGreen";
                else
                {
                    Color = "White";
                    grade = "#ERROR";
                }
            }
            catch
            {
                Color = "White";
                grade = "#ERROR";
            }
        }


        public string Name
        {
            get => name;
            set
            {
                name = value;
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

        public string Grade
        {
            get => grade;
            set
            {
                grade = value;
                if (grade != "" && grade != "#ERROR")
                {
                    SetColor();
                }
                PropertyChangeNotification();
            }
        }
    }
}
