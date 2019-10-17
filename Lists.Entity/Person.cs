using System;
using System.Collections;

namespace Lists.Entity
{
    public class Person : IComparable
    {
        public Person(string firstName, string lastName, string svNr, string zip)
        {
            FirstName = firstName;
            LastName = lastName;
            SvNr = svNr;
            Zip = zip;
        }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Zip { get; set; }
        public string SvNr { get; set; }

        public int CompareTo(object obj)
        {
            var person = obj as Person;
            if (person == null)
            {
                throw new ArgumentException("Illigaler Typ");
            }
            return LastName.CompareTo(person.LastName);
        }

        public static IComparer ComparePersonOnZipDescanding()
        {
            return new PersonOnZipDescanding();
        }

        public static IComparer ComparePersonOnSvNrDescanding()
        {
            return new PersonOnSvNrDescanding();
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName}";
        }
    }
}
