using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    internal abstract class Exam
    {
        public int TimeLimitMinutes { get; set; }
        public List<Questions> questions { get; set; } = new List<Questions>();
        public Subject SubjExam { get; set; }

        public void AddQuestion(Questions que)
        {
            this.questions.Add(que);
        }

        public abstract void StartExam();
    }
}
