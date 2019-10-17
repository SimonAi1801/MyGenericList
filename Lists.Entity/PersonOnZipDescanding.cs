using System;
using System.Collections;
namespace Lists.Entity
{
    public class PersonOnZipDescanding : IComparer
    {
        public int Compare(object x, object y)
        {
            Person person1 = x as Person;
            Person person2 = y as Person;

            if (person1 == null)
                throw new ArgumentException("Invalid typ!");
            if (person2 == null)
                throw new AggregateException("Invalid typ!");
            int result = 0;

            if (Convert.ToInt32(person1.Zip) > Convert.ToInt32(person2.Zip))
            {
                result = 1;
            }
            else if (Convert.ToInt32(person1.Zip) < Convert.ToInt32(person2.Zip))
            {
                result = -1;
            }
            return result;
        }
    }
}