using NUnit.Framework;
using Shouldly;

namespace ConTabs.Tests
{
    [TestFixture]
    public class ConformanceTests
    {
        [Test]
        public void BasicTableWithNoDataShouldLookLikeThis()
        {
            // Arrange
            var listOfTestClasses = TestData.ListOfTestData(0);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);

            // Act
            var tableString = tableObj.ToString();

            // Assert
            tableString.ShouldBe(@"+------------------------------------------------------------+
| StringColumn | IntColumn | CurrencyColumn | DateTimeColumn |
+------------------------------------------------------------+");
        }
    }

}
