using NUnit.Framework;
using Shouldly;
using System;

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
            string expected = "+------------------------------------------------------------+" + Environment.NewLine;
            expected       += "| StringColumn | IntColumn | CurrencyColumn | DateTimeColumn |" + Environment.NewLine;
            expected       += "+------------------------------------------------------------+";
            tableString.ShouldBe(expected);
        }
    }

}
