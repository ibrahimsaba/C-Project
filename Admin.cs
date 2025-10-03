using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Project
{
    internal class Admin
    {
        string basePath = @"C:\Users\DELL\Desktop\C# Labs\Project\Project\bin\Debug\net10.0\";

        public void AddQuestion(Questions q, Subject subject)
        {
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }
            string path = Path.Combine(basePath, $"{subject.SubjCode}_questions.txt");

            using (StreamWriter sw = new StreamWriter(path, true))
            {
                if (q is TrueOrFalse t)
                {
                    sw.WriteLine($"TrueOrFalse|{t.Head}|{t.Body}|{t.Mark}|{t.CorrectAns}");
                }
                else if (q is ChooseOne co)
                {
                    string choices = string.Join(",", co.Choices);
                    sw.WriteLine($"CO|{co.Head}|{co.Body}|{co.Mark}|{choices}|{co.CorrectAns}");
                }
                else if (q is ChooseMulti cm)
                {
                    string choices = string.Join(",", cm.Choices);
                    string correctAnswer = string.Join(",", cm.CorrectAnswers);
                    sw.WriteLine($"CM|{cm.Head}|{cm.Body}|{cm.Mark}|{choices}|{correctAnswer}");
                }
            }

            Console.WriteLine($"Question added to {subject.SubjCode}_questions.txt");
        }
    }
}
