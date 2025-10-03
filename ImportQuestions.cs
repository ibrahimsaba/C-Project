using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    internal static class ImportQuestions
    {
        public static List<Questions> LoadFromFile(string filePath)
        {
            var questions = new List<Questions>();
            foreach (var l in File.ReadAllLines(filePath))
            {
                var take = l.Split('|');
                string qType = take[0];
                string qHead = take[1];
                string qBody = take[2];
                int mark = int.Parse(take[3]);
                if (qType == "TrueOrFalse")
                {
                    bool correctAns = bool.Parse(take[4]);
                    questions.Add(new TrueOrFalse(qHead, qBody, mark, correctAns));
                }
                else if (qType == "CO")
                {
                    var choices = new List<string>();
                    foreach (var c in take[4].Split(','))
                    {
                        choices.Add(c.Trim());
                    }

                    int correctAns = int.Parse(take[5]) - 1;

                    questions.Add(new ChooseOne(qHead, qBody, mark, choices, correctAns));
                }
                else if (qType == "CM")
                {
                    var choices = new List<string>(take[4].Split(","));
                    var correctAnswers = new List<int>();
                    foreach (var answer in take[5].Split(","))
                    {
                        correctAnswers.Add(int.Parse(answer));
                    }
                    questions.Add(new ChooseMulti(qHead, qBody, mark, choices, correctAnswers));
                }
            }
            return questions;

        }

    }
}
