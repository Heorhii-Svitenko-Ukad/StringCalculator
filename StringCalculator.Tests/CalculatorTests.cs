using String_Calculator;
using System;
using Xunit;

namespace StringCalculator.Tests
{
    public class CalculatorTests
    {
        private Calculator calculator;
        public CalculatorTests()
        {
            calculator = new();
        }

        //Task 1
        [Fact]
        public void Add_EmptyString_ReturnsZero()
        {
            //Act
            var sum = calculator.Add("");

            //Assert
            Assert.Equal(0, sum);
        }

        [Fact]
        public void Add_OneNumber_ReturnsThisNumber()
        {
            //Act
            var sum = calculator.Add("2");

            //Assert
            Assert.Equal(2, sum);
        }

        [Fact]
        public void Add_TwoNumbers_ReturnsSumOfThisNumbers()
        {
            //Act
            var sum = calculator.Add("2,2");

            //Assert
            Assert.Equal(4, sum);
        }

        [Theory]
        [InlineData("10,")]
        [InlineData("10,,10")]
        [InlineData("10,\n")]
        public void Add_NumbersWithExtraCommas_ThrowsFormatException(string value)
        {
            //Act
            void action() => calculator.Add(value);

            //Assert 
            Assert.Throws<FormatException>(action);
        }        

        //Task 2
        [Fact]
        public void Add_FiveNumbers_ReturnsSumOfThisNumbers()
        {
            //Act
            var sum = calculator.Add("2,2,2,2,2");

            //Assert
            Assert.Equal(10, sum);
        }

        //Task 3
        [Fact]
        public void Add_ThreeNumbersWithDifferentDelimiters_ReturnsNumbers()
        {
            //Act
            var sum = calculator.Add("10\n10,10");

            //Assert
            Assert.Equal(30, sum);
        }

        //Task 4
        [Fact]
        public void Add_ThreeNumbersWithCustomSingleCharDelimiter_ReturnsNumbers()
        {
            //Act 
            var sum = calculator.Add("//[;]\\n10;10;10");

            //Assert
            Assert.Equal(30, sum);
        }

        //Task 5
        [Theory]
        [InlineData("-10,10", "Negatives not allowed: -10")]
        [InlineData("-10,-10", "Negatives not allowed: -10 -10")]
        public void Add_OneNegativeNumber_ThrowsFormatExceptionWithMessageAboutNegativeNumbers(string input, string message)
        {
            //Act            
            void action() => calculator.Add(input);
            FormatException exception = Assert.Throws<FormatException>(action);

            //Assert
            Assert.Equal(message, exception.Message);
        }
        
        //Task 6
        [Fact]
        public void Add_TwoNumbersButOneNumberIsBiggerThanThousand_ReturnsNumberThatLessThanThousand()
        {
            //Act
            var result = calculator.Add("1000,10");

            //Assert
            Assert.Equal(10, result);
        }

        //Task 7
        [Fact]
        public void Add_ThreeNumbersWithCustomMultiCharDelimiter_ReturnsNumbers()
        {
            //Act 
            var sum = calculator.Add("//[;*]\\n10;*10;*10");

            //Assert
            Assert.Equal(30, sum);
        }

        //Task 8
        [Fact]
        public void Add_ThreeNumbersWithTwoCustomSingleCharDelimiter_ReturnsNumbers()
        {
            //Act 
            var sum = calculator.Add("//[;][*]\\n10;10*10");

            //Assert
            Assert.Equal(30, sum);
        }

        //Task 9
        [Fact]
        public void Add_ThreeNumbersWithTwoCustomMultiCharDelimiter_ReturnsNumbers()
        {
            //Act 
            var sum = calculator.Add("//[;*][??]\\n10;*10??10");

            //Assert
            Assert.Equal(30, sum);
        }
    }
}
