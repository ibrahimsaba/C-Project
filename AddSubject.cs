using System;
using System.Collections.Generic;
using System.IO;

namespace Project
{
    internal static class AddSubject
    {
        public static List<Subject> Subjects = new List<Subject>();
        private static string filePath = @"C:\Users\DELL\Desktop\C# Labs\Project\Project\bin\Debug\net10.0\subjects.txt";

        public static void AddSubjec(Subject subject)
        {
            Subjects.Add(subject);
            SaveSubjects();
        }

        public static void ShowSubject()
        {
            if (Subjects.Count == 0)
            {
                Console.WriteLine("No subjects available.");
                return;
            }

            for (int i = 0; i < Subjects.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {Subjects[i]}");
            }
        }

        public static Subject? GetSubject(string subjcode)
        {
            return Subjects.Find(a => a.SubjCode.Equals(subjcode, StringComparison.OrdinalIgnoreCase));
        }
        private static void SaveSubjects()
        {
            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                foreach (var subj in Subjects)
                {
                    sw.WriteLine($"{subj.SubjCode}|{subj.SubjName}|{subj.Desc}");
                }
            }
        }
        public static void LoadSubjects()
        {
            Subjects.Clear();
            if (!File.Exists(filePath))
                return;

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split('|');
                if (parts.Length == 3)
                {
                    Subjects.Add(new Subject(parts[1], parts[0], parts[2]));
                }
            }
        }
    }
}
