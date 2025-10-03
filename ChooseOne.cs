using System;
using System.Collections.Generic;

namespace Project
{
    internal class ChooseOne : Questions
    {
        public List<string> Choices { get; set; }
        public int CorrectAns { get; set; }

        public ChooseOne(string _header, string _body, int _mark, List<string> _choices, int _correctAnsw)
            : base(_header, _body, _mark)
        {
            Choices = _choices;
            CorrectAns = _correctAnsw;
        }

        public override void DisplayQuestion()
        {
            Console.WriteLine($"{Head} - Mark: {Mark}");
            Console.WriteLine(Body);
            for (int i = 0; i < Choices.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {Choices[i]}");
            }
        }
        public override int Answer()
        {
            int studentAnswer = GetStudentAnswer();
            return (studentAnswer == CorrectAns) ? Mark : 0;
        }
        public int AnswerWithCorrection()
        {
            int result = Answer();
            if (result == Mark)
                Console.WriteLine("Correct!");
            else
                Console.WriteLine($"Wrong Correct Answer: {CorrectAns}");
            return result;
        }
        private int GetStudentAnswer()
        {
            string answer = Console.ReadLine().Trim().ToLower();
            if (int.TryParse(answer, out int ans))
                return ans;
            int index = Choices.FindIndex(c => c.ToLower() == answer);
            return (index != -1) ? (index + 1) : -1;
        }
    }
}
