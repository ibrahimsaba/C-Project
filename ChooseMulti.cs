using System;
using System.Collections.Generic;

namespace Project
{
    internal class ChooseMulti : Questions
    {
        public List<string> Choices { get; set; }
        public List<int> CorrectAnswers { get; set; }

        public ChooseMulti(string _header, string _body, int _mark, List<string> _choices, List<int> _correctAns)
            : base(_header, _body, _mark)
        {
            Choices = _choices;
            CorrectAnswers = _correctAns;
        }

        public override void DisplayQuestion()
        {
            Console.WriteLine($"{Head} - Mark: {Mark}");
            Console.WriteLine(Body);
            for (int i = 0; i < Choices.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {Choices[i]}");
            }
            Console.WriteLine("You can choose multiple answers separated by comma.");
        }
        public override int Answer()
        {
            List<int> studentAnswer = GetStudentAnswer();
            if (studentAnswer.Count != CorrectAnswers.Count)
                return 0;

            studentAnswer.Sort();
            CorrectAnswers.Sort();

            for (int i = 0; i < studentAnswer.Count; i++)
            {
                if (studentAnswer[i] != CorrectAnswers[i])
                    return 0;
            }
            return Mark;
        }
        public int AnswerWithCorrection()
        {
            int result = Answer();
            if (result == Mark)
                Console.WriteLine("Correct");
            else
                Console.WriteLine($"Wrong Correct Answers : {string.Join(",", CorrectAnswers)}");

            return result;
        }

        private List<int> GetStudentAnswer()
        {
            string answer = Console.ReadLine().Trim().ToLower();
            string[] answers = answer.Split(',');
            List<int> studentAnswer = new List<int>();
            foreach (var item in answers)
            {
                if (int.TryParse(item, out int ans))
                {
                    studentAnswer.Add(ans);
                }
                else
                {
                    int index = Choices.FindIndex(c => c.ToLower() == item);
                    if (index != -1)
                        studentAnswer.Add(index + 1);
                }
            }
            return studentAnswer;
        }
    }
}
