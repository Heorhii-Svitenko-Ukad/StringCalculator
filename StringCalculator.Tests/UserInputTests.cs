using Moq;
using String_Calculator;
using System;
using Xunit;

namespace StringCalculator.Tests
{
    public class UserInputTests
    {
        private Mock<ConsoleWrapper> consoleMock;
        private Mock<Calculator> calculatorMock;
        private UserInputProcessor inputProcessor;

        public UserInputTests()
        {
            consoleMock = new(); 
            calculatorMock = new();
            inputProcessor = new(consoleMock.Object, calculatorMock.Object);
        }

        [Fact]
        public void ProcessUserInputAndPerfomCalculations_EmptyUserInput_ReturnsSum()
        {
            //Arrange
            consoleMock.SetupSequence(x => x.ReadLine()).Returns("");

            //Act
            inputProcessor.ProcessUserInputAndPerfomCalculations();

            //Assert
            consoleMock.Verify(x => x.WriteLine("Enter comma separated numbers (enter to exit)"), Times.Once);
        }

        [Fact]
        public void ProcessUserInputAndPerfomCalculations_TwoCorrectUserInput_ReturnsSum()
        {
            //Arrange
            consoleMock.SetupSequence(x => x.ReadLine())
                        .Returns("1,2")
                        .Returns("4,5")
                        .Returns("");

            calculatorMock.SetupSequence(x => x.Add(It.IsAny<string>()))
                          .Returns(3)
                          .Returns(9);

            //Act
            inputProcessor.ProcessUserInputAndPerfomCalculations();

            //Assert
            consoleMock.Verify(x => x.WriteLine("Enter comma separated numbers (enter to exit)"), Times.Exactly(3));
            consoleMock.Verify(x => x.WriteLine("Result: 3"), Times.Once);
            consoleMock.Verify(x => x.WriteLine("Result: 9"), Times.Once);
        }

        [Fact]
        public void ProcessUserInputAndPerfomCalculation_IncorrectUserInput_ReturnsException()
        {
            //Arrange
            consoleMock.SetupSequence(x => x.ReadLine())
                       .Returns("-10,-10")
                       .Returns("1,2")
                       .Returns("");

            calculatorMock.SetupSequence(x => x.Add(It.IsAny<string>()))
                          .Throws(new FormatException("Negatives not allowed: -10 -10"))
                          .Returns(3);

            //Act
            inputProcessor.ProcessUserInputAndPerfomCalculations();
            
            //Assert
            consoleMock.Verify(x => x.WriteLine("Invalid format\nDetails: Negatives not allowed: -10 -10"), Times.Once);
            consoleMock.Verify(x => x.WriteLine("Result: 3"), Times.Once);
        }        
    }
}
