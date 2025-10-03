using System;

namespace Project
{
    internal class TrueOrFalse : Questions
    {
        public bool CorrectAns { get; set; }

        public TrueOrFalse(string _header, string _body, int _mark, bool _correctAns)
            : base(_header, _body, _mark)
        {
            CorrectAns = _correctAns;
        }
        public override void DisplayQuestion()
        {
            Console.WriteLine($"{Head} - Mark: {Mark}");
            Console.WriteLine(Body);
            Console.WriteLine("1) True");
            Console.WriteLine("2) False");
        }
        public override int Answer()
        {
            string answer = Console.ReadLine().Trim().ToLower();
            bool studentAnswer;

            if (answer == "1" || answer == "true")
                studentAnswer = true;
            else if (answer == "2" || answer == "false")
                studentAnswer = false;
            else
            {
                Console.WriteLine("Please Enter a Valid Value (1/2 or true/false)");
                return 0;
            }

            return studentAnswer == CorrectAns ? Mark : 0;
        }
        public int AnswerWithCorrection()
        {
            int result = Answer();
            if (result == Mark)
            {
                Console.WriteLine("Correct!");
            }
            else
            {
                Console.WriteLine($"Wrong! Correct Answer is: {CorrectAns}");
            }
            return result;
        }
    }
}
