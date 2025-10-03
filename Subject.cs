using System;
using System.Collections.Generic;

namespace Project
{
    internal class Subject : ICloneable, IComparable<Subject>
    {
        public string SubjName { get; set; }
        public string SubjCode { get; set; }
        public string Desc { get; set; }

        public Dictionary<string, string> Tags { get; set; } = new Dictionary<string, string>();
        public Subject(string subjName = "Unknown", string subjCode = "000", string desc = "No Description")
        {
            SubjName = subjName;
            SubjCode = subjCode;
            Desc = desc;
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public int CompareTo(Subject? other)
        {
            if (other == null) return 1;
            return SubjCode.CompareTo(other.SubjCode);
        }
        public override string ToString()
        {
            return $"{SubjCode}::{SubjName}::{Desc}";
        }
        public override bool Equals(object? obj)
        {
            return obj is Subject s && s.SubjCode.Equals(SubjCode, StringComparison.OrdinalIgnoreCase);
        }
        public override int GetHashCode()
        {
            return SubjCode.GetHashCode();
        }
    }
}
