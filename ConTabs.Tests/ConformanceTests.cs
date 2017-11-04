using NUnit.Framework;using Shouldly;
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
            string expected = "";
            expected += "+------------------------------------------------------------+" + Environment.NewLine;
            expected += "| StringColumn | IntColumn | CurrencyColumn | DateTimeColumn |" + Environment.NewLine;
            expected += "+------------------------------------------------------------+" + Environment.NewLine;
            expected += "|                          no data                           |" + Environment.NewLine;
            expected += "+------------------------------------------------------------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void BasicTableWithOneLineOfDataShouldLookLikeThis()
        {
            // Arrange
            var listOfTestClasses = TestData.ListOfTestData(1);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);

            // Act
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+-----------------------------------------------------------------+" + Environment.NewLine;
            expected += "| StringColumn | IntColumn | CurrencyColumn | DateTimeColumn      |" + Environment.NewLine;
            expected += "+-----------------------------------------------------------------+" + Environment.NewLine;
            expected += "| AAAA         | 999       | 19.95          | 01/01/2017 00:00:00 |" + Environment.NewLine;
            expected += "+-----------------------------------------------------------------+";
            tableString.ShouldBe(expected);
        }
    }

}
