using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ReactiveUI;
using System.Reactive;
using ProgressTable.Models;

namespace ProgressTable.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        List<string> items;
        List<Student> students;
        List<SubjectAverageCell> itemsAverage = new List<SubjectAverageCell>() { new SubjectAverageCell(), new SubjectAverageCell(), new SubjectAverageCell() };

        ObservableCollection<Student> studentList;
        ObservableCollection<SubjectAverageCell> itemsAverageList;

        public MainWindowViewModel()
        {
            items = new List<string>() { "Математика", "ТРПО", "Визуальное программирование" };
            students = new List<Student>() { };
            studentList = new ObservableCollection<Student>(students);
            itemsAverageList = new ObservableCollection<SubjectAverageCell>(itemsAverage);
            Add = ReactiveCommand.Create(() => add());
            RemoveSelected = ReactiveCommand.Create(() => Remove());
        }

        public void add()
        {
            students.Add(new Student(items));
            StudentList = new ObservableCollection<Student>(students);
            RefreshAverageList();
        }

        public void Remove()
        {
            List<Student> removeList = new List<Student>();
            foreach (var item in StudentList)
            {
                if (item.IsSelected)
                    removeList.Add(item);
            }
            foreach (var item in removeList)
            {
                students.Remove(item);
            }
            StudentList = new ObservableCollection<Student>(students);
            RefreshAverageList();
        }

        public void RefreshAverageList()
        {
            try
            {
                foreach (SubjectAverageCell average in itemsAverage)
                {
                    average.Grade = "0";
                }

                foreach (Student student in studentList)
                {
                    for (int i = 0; i < student.ItemList.Count(); i++)
                    {
                        itemsAverageList[i].Grade = Convert.ToString(Convert.ToDouble(itemsAverageList[i].Grade)
                            + Convert.ToDouble(student.ItemList[i].Grade) / (double)studentList.Count());
                    }
                }
            }
            catch
            {

            }
        }

        public void RefreshStudentAverage()
        {
            foreach (var student in studentList)
            {
                student.RefreshAverageGrade();
            }
        }

        public ReactiveCommand<Unit, Unit> Add { get; }
        public ReactiveCommand<Unit, Unit> RemoveSelected { get; }
        public ObservableCollection<Student> StudentList
        {
            get => studentList;
            set
            {
                this.RaiseAndSetIfChanged(ref studentList, value);
            }
        }

        public ObservableCollection<SubjectAverageCell> ItemsAverageList
        {
            get => itemsAverageList;
        }

        public List<Student> Students
        {
            get => students;
        }

        public void SaveFile(string path)
        {
            ProcessingFile.WriteFile(path, students);
        }

        public void LoadFile(string path)
        {
            students = ProcessingFile.ReadFile(path, items.Count());
            StudentList = new ObservableCollection<Student>(students);
            RefreshAverageList();
            RefreshStudentAverage();
        }
    }
}
