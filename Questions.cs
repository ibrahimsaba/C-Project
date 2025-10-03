using System;

namespace Project
{
    internal abstract class Questions : ICloneable, IComparable<Questions>
    {
        public string Head { get; set; }
        public string Body { get; set; }
        public int Mark { get; set; }

        public Questions(string head = "No Head", string body = "No Body", int mark = 0)
        {
            Head = head;
            Body = body;
            Mark = mark;
        }

        public abstract void DisplayQuestion();
        public abstract int Answer();
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public int CompareTo(Questions other)
        {
            if (other == null) return 1;
            return this.Mark.CompareTo(other.Mark);
        }
        public override string ToString()
        {
            return $"{Head} ({Mark} pts)";
        }

        public override bool Equals(object obj)
        {
            if (obj is Questions q)
            {
                return this.Head == q.Head && this.Body == q.Body && this.Mark == q.Mark;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Head, Body, Mark);
        }
    }
}
