using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace String_Calculator
{
    public class Calculator
    {
        public virtual int Add(string numbers)
        {
            IEnumerable<string> delimiters = ParseDelimiters(numbers);
            IEnumerable<int> parsedNumbers = ParseInput(numbers, delimiters);

            ValidateForNegativeNumbers(parsedNumbers);

            return parsedNumbers.Where(num => num < 1000).Sum();
        }

        private void ValidateForNegativeNumbers(IEnumerable<int> numbers)
        {
            var negativeNumbers = numbers.Where(num => num < 0);

            if (negativeNumbers.Any())
            {
                StringBuilder messageBuilder = new("Negatives not allowed:");
                foreach (var item in negativeNumbers)
                {
                    messageBuilder.Append(' ').Append(item);
                }

                throw new FormatException(messageBuilder.ToString());
            }
        }

        private IEnumerable<string> ParseDelimiters(string input)
        {
            if (input.StartsWith("//") && input.Contains("\\n"))
            {
                int indexOfDelimiterEnd = input.IndexOf("\\n");
                string delimitersAsString = input[2..indexOfDelimiterEnd];
                IEnumerable<string> delimitersAsArray = ParseDelimiterString(delimitersAsString);

                indexOfDelimiterEnd += 2;

                return delimitersAsArray;
            }
            else
            {
                return Array.Empty<string>();
            }
        }
        private IEnumerable<string> ParseDelimiterString(string delimiters)
        {
            int lastBracketIndex = delimiters.Length - 1;
            delimiters = delimiters[1..lastBracketIndex];

            return delimiters.Split("][");
        }

        private IEnumerable<int> ParseInput(string input, IEnumerable<string> customDelimiters)
        {
            string[] delimeters = customDelimiters.Any() ? customDelimiters.ToArray() : new string[] { ",", "\n" };
            
            if (input.StartsWith("//") && input.Contains("\\n"))
            {
                int indexOfDelimiterEnd = input.IndexOf("\\n") + 2;
                input = input[indexOfDelimiterEnd..input.Length];
            }

            if (string.IsNullOrWhiteSpace(input))
            {
                return new int[] { 0 };
            }

            foreach (string delimiter in delimeters)
            {
                if (input.EndsWith(delimiter))
                {
                    throw new FormatException();
                }
            }

            return input.Split(delimeters, StringSplitOptions.None)
                        .Select(str => int.Parse(str))
                        .ToArray();
        }
    }
}
