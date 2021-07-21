using System;

namespace String_Calculator
{
    public class UserInputProcessor
    {
        private ConsoleWrapper consoleWrapper;
        private Calculator calculator;

        public UserInputProcessor(ConsoleWrapper consoleWrapper, Calculator calculator)
        {
            this.consoleWrapper = consoleWrapper;
            this.calculator = calculator;
        }

        public void ProcessUserInputAndPerfomCalculations()
        {
            while (true)
            {
                consoleWrapper.WriteLine("Enter comma separated numbers (enter to exit)");

                string input = consoleWrapper.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    break;
                }

                try
                {
                    consoleWrapper.WriteLine($"Result: {calculator.Add(input)}");
                }
                catch (FormatException e)
                {
                    consoleWrapper.WriteLine($"Invalid format\nDetails: {e.Message}");
                }
            }
        }        
    }
}