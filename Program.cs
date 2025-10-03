using System;
using System.Collections.Generic;
using System.IO;
using Project;

class Program
{
    static void Main()
    {
        AddSubject.LoadSubjects();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome! Please choose your position:");
            Console.WriteLine("1) Admin");
            Console.WriteLine("2) Student");
            Console.WriteLine("3) Exit");

            string choice = Console.ReadLine()?.Trim() ?? "";

            if (choice == "1" || choice.ToLower() == "admin")
            {
                Admin admin = new Admin();

                while (true)
                {
                    Console.WriteLine("\nAdmin Menu:");
                    Console.WriteLine("1) Add Subject");
                    Console.WriteLine("2) Add Question");
                    Console.WriteLine("3) Back");
                    string adminChoice = Console.ReadLine()?.Trim() ?? "";

                    if (adminChoice == "1")
                    {
                        Console.WriteLine("Enter Subject Name:");
                        string name = Console.ReadLine() ?? "Unknown";
                        Console.WriteLine("Enter Subject Code:");
                        string code = Console.ReadLine() ?? "000";
                        Console.WriteLine("Enter Subject Description:");
                        string desc = Console.ReadLine() ?? "No Description";

                        Subject subj = new Subject(name, code, desc);
                        AddSubject.AddSubjec(subj);
                        Console.WriteLine("Subject Added!");
                    }
                    else if (adminChoice == "2")
                    {
                        Console.WriteLine("Available Subjects:");
                        AddSubject.ShowSubject();

                        Console.WriteLine("Enter Subject Code to Add Question:");
                        string subjCode = Console.ReadLine() ?? "";
                        Subject subject = AddSubject.GetSubject(subjCode);
                        if (subject == null)
                        {
                            Console.WriteLine("Invalid Subject Code!");
                            continue;
                        }

                        Console.WriteLine("Enter Question Type (TrueOrFalse / CO / CM):");
                        string type = Console.ReadLine()?.Trim() ?? "";
                        Console.WriteLine("Enter Question Head:");
                        string head = Console.ReadLine() ?? "";
                        Console.WriteLine("Enter Question Body:");
                        string body = Console.ReadLine() ?? "";
                        Console.WriteLine("Enter Question Mark:");
                        int mark = int.TryParse(Console.ReadLine(), out int m) ? m : 0;

                        Questions q = null!;
                        if (type.ToLower() == "trueorfalse")
                        {
                            Console.WriteLine("Enter Correct Answer (True/False):");
                            bool correct = bool.TryParse(Console.ReadLine(), out bool c) && c;
                            q = new TrueOrFalse(head, body, mark, correct);
                        }
                        else if (type.ToLower() == "co")
                        {
                            Console.WriteLine("Enter Choices Separated by comma:");
                            List<string> choices = new List<string>((Console.ReadLine() ?? "").Split(','));
                            Console.WriteLine("Enter Correct Answer Number:");
                            int correct = int.TryParse(Console.ReadLine(), out int c) ? c : 1;
                            q = new ChooseOne(head, body, mark, choices, correct);
                        }
                        else if (type.ToLower() == "cm")
                        {
                            Console.WriteLine("Enter Choices Separated by comma:");
                            List<string> choices = new List<string>((Console.ReadLine() ?? "").Split(','));
                            Console.WriteLine("Enter Correct Answers Numbers Separated by comma:");
                            List<int> correct = new List<int>();
                            foreach (var ci in (Console.ReadLine() ?? "").Split(','))
                            {
                                if (int.TryParse(ci.Trim(), out int num)) correct.Add(num);
                            }
                            q = new ChooseMulti(head, body, mark, choices, correct);
                        }

                        if (q != null)
                        {
                            admin.AddQuestion(q, subject);
                            Console.WriteLine("Question Added Successfully!");
                        }
                    }
                    else if (adminChoice == "3")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice!");
                    }
                }
            }
            else if (choice == "2" || choice.ToLower() == "student")
            {
                Console.WriteLine("Enter Student Name:");
                string studentName = Console.ReadLine() ?? "Unknown";
                Console.WriteLine("Enter Student ID:");
                int studentID = int.TryParse(Console.ReadLine(), out int id) ? id : 0;

                Student student = new Student(studentName, studentID);

                Console.WriteLine("Available Subjects:");
                AddSubject.ShowSubject();

                Console.WriteLine("Enter Subject Code to Take Exam:");
                string subjCode = Console.ReadLine() ?? "";
                Subject subject = AddSubject.GetSubject(subjCode);
                if (subject == null)
                {
                    Console.WriteLine("Invalid Subject Code!");
                    continue;
                }

                string filePath = Path.Combine(
                    @"C:\Users\DELL\Desktop\C# Labs\Project\Project\bin\Debug\net10.0\",
                    $"{subject.SubjCode}_questions.txt"
                );

                if (!File.Exists(filePath))
                {
                    Console.WriteLine("No questions available for this subject!");
                    continue;
                }

                var questions = ImportQuestions.LoadFromFile(filePath);

                Console.WriteLine("\nChoose Exam Type:");
                Console.WriteLine("1) Practice Exam");
                Console.WriteLine("2) Final Exam");
                string examChoice = Console.ReadLine()?.Trim() ?? "";

                Exam exam;
                if (examChoice == "1")
                {
                    exam = new ParacticeExam { TimeLimitMinutes = 10, questions = questions };
                }
                else if (examChoice == "2")
                {
                    exam = new FinalExam { TimeLimitMinutes = 30, questions = questions };
                }
                else
                {
                    Console.WriteLine("Invalid choice!");
                    continue;
                }
                Dictionary<string, string> studentAnswers = new Dictionary<string, string>();

                foreach (var q in questions)
                {
                    q.DisplayQuestion();
                    int score = q.Answer();

                    string correctAns = q switch
                    {
                        TrueOrFalse tf => tf.CorrectAns.ToString(),
                        ChooseOne co => co.Choices[co.CorrectAns - 1],
                        ChooseMulti cm => string.Join(",", cm.CorrectAnswers),
                        _ => ""
                    };

                    studentAnswers[q.Body] = correctAns;
                }

                exam.StartExam();

                Console.WriteLine("Exam Finished! Press any key to continue...");
                Console.ReadKey();
            }
            else if (choice == "3" || choice.ToLower() == "exit")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice! Press Any key to try Again...");
                Console.ReadKey();
            }
        }
    }
}
