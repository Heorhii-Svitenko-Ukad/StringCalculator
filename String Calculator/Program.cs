using System;

namespace String_Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessUserInput();
        }

        static void ProcessUserInput()
        {
            Calculator calculator = new();

            while (true)
            {
                Console.WriteLine("Enter comma separated numbers (enter to exit)");
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    break;
                }

                try
                {
                    Console.WriteLine($"Result: {calculator.Add(input)}");
                }
                catch (FormatException e)
                {
                    Console.WriteLine($"Invalid format\nDetails: {e.Message}");
                }
            }
        }
    }
}
