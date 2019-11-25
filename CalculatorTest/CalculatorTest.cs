using IdeagenCalculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IdeagenCalculatorTest
{
    [TestClass]
    public class CalculatorTest
    {
        [TestMethod]
        public void TestRecursionCalculator()
        {
            Calculator calculator = new RecursionCalculator();

            //for (var i = 0; i < 100000; i++)
            //{
                Assert.AreEqual(200.55, calculator.Calculate("200.55"));
                Assert.AreEqual(2, calculator.Calculate("1 + 1"));
                Assert.AreEqual(4, calculator.Calculate("2 * 2"));
                Assert.AreEqual(6, calculator.Calculate("1 + 2 + 3"));
                Assert.AreEqual(3, calculator.Calculate("6 / 2"));
                Assert.AreEqual(34, calculator.Calculate("11 + 23"));
                Assert.AreEqual(34.1, calculator.Calculate("11.1 + 23"));
                Assert.AreEqual(37, calculator.Calculate("( 11.5 + 15.4 ) + 10.1"));
                Assert.AreEqual(6.2, calculator.Calculate("23 - ( 29.3 - 12.5 )"));
                Assert.AreEqual(4020, calculator.Calculate("( 1 + 1 ) * 100.5 * 20"));
                Assert.AreEqual(5, calculator.Calculate("2 * 2 + 4 / 4"));
                Assert.AreEqual(2526, calculator.Calculate("150 * 23 - ( ( 29.3 - 12.5 ) * ( 22 + 33 ) )"));
                Assert.AreEqual(2546.2, calculator.Calculate("( 100 / 500 + 20 ) + 150 * 23 - ( ( ( ( 29.3 - 12.5 ) * ( 22 + 33 ) ) ) )"));
            //}
        }

        [TestMethod]
        public void TestStackCalculator()
        {
            Calculator calculator = new StackCalculator();

            //for (var i = 0; i < 100000; i++)
            //{
                Assert.AreEqual(200.55, calculator.Calculate("200.55"));
                Assert.AreEqual(2, calculator.Calculate("1 + 1"));
                Assert.AreEqual(4, calculator.Calculate("2 * 2"));
                Assert.AreEqual(6, calculator.Calculate("1 + 2 + 3"));
                Assert.AreEqual(3, calculator.Calculate("6 / 2"));
                Assert.AreEqual(34, calculator.Calculate("11 + 23"));
                Assert.AreEqual(34.1, calculator.Calculate("11.1 + 23"));
                Assert.AreEqual(37, calculator.Calculate("( 11.5 + 15.4 ) + 10.1"));
                Assert.AreEqual(6.2, calculator.Calculate("23 - ( 29.3 - 12.5 )"));
                Assert.AreEqual(4020, calculator.Calculate("( 1 + 1 ) * 100.5 * 20"));
                Assert.AreEqual(5, calculator.Calculate("2 * 2 + 4 / 4"));
                Assert.AreEqual(2526, calculator.Calculate("150 * 23 - ( ( 29.3 - 12.5 ) * ( 22 + 33 ) )"));
                Assert.AreEqual(2546.2, calculator.Calculate("( 100 / 500 + 20 ) + 150 * 23 - ( ( ( ( 29.3 - 12.5 ) * ( 22 + 33 ) ) ) )"));
            //}
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidExpressionException))]
        public void TestRecursionCalculatorException()
        {
            Calculator calculator = new RecursionCalculator();
            Assert.AreEqual(6.2, calculator.Calculate("23 -29.3 - 12.5"));
            Assert.AreEqual(6.2, calculator.Calculate("23 -( 29.3 - * 12.5 )"));
            Assert.AreEqual(6.2, calculator.Calculate("1 +s)"));
            Assert.AreEqual(2546.2, calculator.Calculate("( 100 / 500 + 20 ) + 150 * 23 - ( ( ( ( 29.3 - 12.5 ) * ( 22 + 33 ) ) ) ) ) )"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidExpressionException))]
        public void TestStackCalculatorException()
        {
            Calculator calculator = new StackCalculator();
            Assert.AreEqual(6.2, calculator.Calculate("23 -29.3 - 12.5"));
            Assert.AreEqual(6.2, calculator.Calculate("23 -( 29.3 - * 12.5 )"));
            Assert.AreEqual(6.2, calculator.Calculate("1 +s)"));
            Assert.AreEqual(2546.2, calculator.Calculate("( 100 / 500 + 20 ) + 150 * 23 - ( ( ( ( 29.3 - 12.5 ) * ( 22 + 33 ) ) ) ) ) )"));
        }
    }
}
