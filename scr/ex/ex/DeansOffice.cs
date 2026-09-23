using System.IO;

namespace Exam_Student
{
    public class DeansOffice
    {
        public Student[] Students { get; set; }
        public Student[] SortStudents { get; set; }
        public void Sort()
        {
            if (Students == null || Students.Length <= 1) return;
            SortStudents = (Student[])Students.Clone();
            for (int i = 0; i < SortStudents.Length; i++)
            {
                for (int j = 0; j < SortStudents.Length - 1; j++)
                {
                    if (SortStudents[j] == null || SortStudents[j + 1] == null) continue;
                    if (SortStudents[j].Surname == SortStudents[j + 1].Surname)
                    {
                        if (SortStudents[j].Name.CompareTo(SortStudents[j + 1].Name) > 0)
                        {
                            var temp = SortStudents[j];
                            SortStudents[j] = SortStudents[j + 1];
                            SortStudents[j + 1] = temp;
                        }
                    }
                    else if (SortStudents[j].Surname.CompareTo(SortStudents[j + 1].Surname) > 0)
                    {
                        var temp = SortStudents[j];
                        SortStudents[j] = SortStudents[j + 1];
                        SortStudents[j + 1] = temp;
                    }
                }
            }
        }
        public void SaveToFile(string filePath)
        {
            using StreamWriter sw = new StreamWriter(filePath);
            foreach (var student in SortStudents)
            {
                if (student == null) continue;
                sw.WriteLine($"{student.Surname}; {student.Name}; {student.NumberBook}");
            }
        }
    }
}
