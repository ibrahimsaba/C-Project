using System;

namespace Project
{
    internal class Student : ICloneable, IComparable<Student>
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public event EventHandler<string> ExamStarted;

        public Student(string name = "Unknown", int id = 0)
        {
            Name = name;
            ID = id;
        }
        public void StartExam(string subjectName)
        {
            ExamStarted?.Invoke(this, subjectName);
        }
        public object Clone()
        {
            return new Student(Name, ID);
        }
        public int CompareTo(Student other)
        {
            if (other == null) return 1;
            return ID.CompareTo(other.ID);
        }
        public override string ToString()
        {
            return $"Student: {Name}, ID: {ID}";
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            Student other = (Student)obj;
            return ID == other.ID && Name.Equals(other.Name, StringComparison.OrdinalIgnoreCase);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Name.ToLower(), ID);
        }
    }
}
