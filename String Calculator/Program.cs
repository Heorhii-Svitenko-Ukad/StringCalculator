using System;

namespace String_Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            UserInputProcessor processor = new(new ConsoleWrapper(), new Calculator());

            processor.ProcessUserInputAndPerfomCalculations();
        }        
    }
}
