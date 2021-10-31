using System;
using Validation.Classes;

namespace Validation
{
    class Program
    {
        static void Main(string[] args)
        {
            Person validPerson = new Person()
            {
                Name = "Alex Fergusson",
                Height = 190
            };

            Person lengthyNamePerson = new Person()
            {
                Name = "Alex Fergusson The Holder of the longest name in existence",
                Height = 190
            };

            Person lengthyPerson = new Person()
            {
                Name = "Yet Another Alex Fergusson",
                Height = 290
            };

            Validator validator = new Validator();

            Console.WriteLine($"validPerson: {validator.Validate(validPerson)}");
            Console.WriteLine($"lengthyNamePerson: {validator.Validate(lengthyNamePerson)}");
            Console.WriteLine($"lengthyPerson: {validator.Validate(lengthyPerson)}");


            Console.ReadLine();
        }
    }
}
