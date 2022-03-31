using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace ProgressTable.Models
{
    public class ProcessingFile
    {
        public static List<Student> ReadFile(string path, int num)
        {
            List<Student> students = new List<Student>();

            StreamReader file = new StreamReader(path);
            try
            {
                while (!file.EndOfStream)
                {
                    List<Cell> studentCells = new List<Cell>();
                    string studentName = file.ReadLine();

                    for (int i = 0; i < num; i++)
                    {
                        string itemName = file.ReadLine();
                        string itemGrade = file.ReadLine();
                        studentCells.Add(new Cell(itemName, itemGrade));
                    }

                    string studentAverage = file.ReadLine();

                    students.Add(new Student(studentCells, studentName, Convert.ToDouble(studentAverage)));
                }
                file.Close();
                return students;
            }
            catch
            {
                file.Close();
                return new List<Student>();
            }
        }

        public static void WriteFile(string path, List<Student> content)
        {
            File.WriteAllText(path, "");
            List<string> data = new List<string>();
            foreach (Student student in content)
            {
                data.Add(student.Fio);
                foreach (Cell cell in student.ItemList)
                {
                    data.Add(cell.Name);
                    data.Add(cell.Grade);
                }
                data.Add(Convert.ToString(student.Average));
            }
            File.WriteAllLines(path, data);
        }
    }
}
