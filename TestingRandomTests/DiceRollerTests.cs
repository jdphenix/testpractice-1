using Moq;
using System;
using TestingRandom;
using Xunit;

namespace TestingRandomTests
{
    public class DiceRollerTests
    {
        private DiceRoller _sut;

        public DiceRollerTests()
        {
            _sut = new DiceRoller(new Randomizer());
        }

        [Theory]
        [InlineData("fd6")]
        [InlineData("2df")]
        public void Roll_Throws_Format_Exception_For_Malformed_Input_String(string input)
        {
            void Act() => _sut.Roll(input);

            Assert.Throws<FormatException>(Act);
        }

        // TODO: Fix this test
        [Theory]
        [InlineData("2d6", 2, 6)]
        [InlineData("1d6", 1, 6)]
        [InlineData("3d6", 3, 6)]
        public void Roll_Never_Out_Of_Range(string diceExpression, int dieCount, int sideCount)
        {
            var randomMock = new Mock<IRandomizer>();
            randomMock.Setup(r => r.Next(It.IsAny<int>())).Returns(() => dieCount);
            var diceRoller = new DiceRoller(randomMock.Object);

            var actual = diceRoller.Roll(diceExpression);

            randomMock.Verify(mock => mock.Next(6), Times.Exactly(dieCount));
        }

        // TODO: Figure out a way to test making sure the dice roller gets random numbers the right number of times
        // TODO: rolling "2d6" should call the randomizer twice, and pass in 6 to IRandomizer.Next(int)
    }
}