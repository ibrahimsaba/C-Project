using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    internal class ParacticeExam : Exam
    {
        public override void StartExam()
        {
            int score = 0;
            int totalScore = 0;
            int studentScore = 0;
            DateTime timeStart = DateTime.Now;
            DateTime timeEnd = timeStart.AddMinutes(TimeLimitMinutes);
            Console.WriteLine("Paractice Exam");
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
                Console.WriteLine(score > 0 ? "Correct" : "Wrong");
                //Console.WriteLine(score);
                Console.WriteLine($"Your Score = {totalScore}");
            }


        }
    }
}
