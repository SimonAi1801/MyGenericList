using System.Collections;
using System;
namespace Lists.Entity
{
    public class PersonOnFirstNameAscanding : IComparer
    {
        public int Compare(object x, object y)
        {
            var left = x as Person;
            var right = y as Person;
            if (left == null)
            {
                throw new ArgumentException("Illigaler Typ!");
            }
            if (right == null)
            {
                throw new ArgumentException("Illigaler Typ!");
            }
            return left.CompareTo(right);
        }
    }
}