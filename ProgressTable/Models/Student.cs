using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Media;

namespace ProgressTable.Models
{
    public class Student : INotifyPropertyChanged
    {
        double average = 0;
        string averageColor = "Red";
        List<string> subjects = new List<string>() { "Math", "Trpo", "VisualProg" };
        List<Cell> items = new List<Cell>();
        ObservableCollection<Cell> itemList;

        public Student(List<string> _subjects, string fio = "Студент")
        {
            Fio = fio;
            if (_subjects.Count() != 0)
            {
                subjects = _subjects;
            }
            foreach (string itemName in subjects)
            {
                items.Add(new Cell(itemName));
            }
            itemList = new ObservableCollection<Cell>(items);
        }

        public Student(List<Cell> _items, string fio = "Студент", double _average = 0)
        {
            Fio = fio;
            Average = _average;
            items = _items;
            itemList = new ObservableCollection<Cell>(_items);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public string Fio { get; set; }
        public bool IsSelected { get; set; }
      
        public ObservableCollection<Cell> ItemList
        {
            get => itemList;
            set
            {
                itemList = value;
            }
        }

        public string AverageColor
        {
            get => averageColor;
            set
            {
                averageColor = value;
                NotifyPropertyChanged();
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] String property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public void RefreshAverageGrade()
        {
            try
            {
                average = 0;
                foreach (Cell item in items)
                {
                    Average += Convert.ToDouble(item.Grade) / (double)items.Count();
                }
            }
            catch
            {
                Average = 0;
            }
        }

        public double Average
        {
            get => average;
            set
            {
                average = value;
                if (average < 1)
                    AverageColor = "Red";
                else if (average < 1.5)
                    AverageColor = "Yellow";
                else
                    AverageColor = "LightGreen";
                NotifyPropertyChanged();
            }
        }
    }
}
