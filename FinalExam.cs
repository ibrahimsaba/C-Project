using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    internal class FinalExam : Exam
    {

        public override void StartExam()
        {
            int score = 0;
            int studentScore = 0;
            int totalScore = 0;
            DateTime timeStart = DateTime.Now;
            DateTime timeEnd = timeStart.AddMinutes(TimeLimitMinutes);
            Console.WriteLine("Final Exam");
            foreach (var q in questions)
            {
                if (DateTime.Now >= timeEnd)
                {
                    Console.WriteLine("الوقت خلص");
                    break;
                }
                q.DisplayQuestion();
                score = q.Answer();
                studentScore += score;
                totalScore += q.Mark;

            }
            Console.WriteLine($"Your Final Score: {studentScore}/{totalScore}");
        }
    }
}
