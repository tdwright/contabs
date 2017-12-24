using ConTabs.TestData;
using ConTabs;
using NUnit.Framework;
using Shouldly;
using System;

namespace ConTabs.Tests
{
    [TestFixture]
    public class LongStringBehaviourTests
    {
        public readonly string LongString = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vehicula fringilla tortor.";

        [Test]
        public void DefaultBehaviourShouldNotChangeString()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            listOfTestClasses[0].StringColumn = LongString;
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns[0].LongStringBehaviour = LongStringBehaviour.Default;

            tableObj.Columns[1].Hide = true; // hide non-string fields 
            tableObj.Columns[2].Hide = true;
            tableObj.Columns[3].Hide = true;

            // Act
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+-------------------------------------------------------------------------------------------+" + Environment.NewLine;
            expected += "| StringColumn                                                                              |" + Environment.NewLine;
            expected += "+-------------------------------------------------------------------------------------------+" + Environment.NewLine;
            expected += "| Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vehicula fringilla tortor. |" + Environment.NewLine;
            expected += "+-------------------------------------------------------------------------------------------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void TruncateBehaviourShouldNotTruncateShortString()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns[0].LongStringBehaviour = LongStringBehaviour.Truncate;

            tableObj.Columns[1].Hide = true; // hide non-string fields 
            tableObj.Columns[2].Hide = true;
            tableObj.Columns[3].Hide = true;

            // Act
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+--------------+" + Environment.NewLine;
            expected += "| StringColumn |" + Environment.NewLine;
            expected += "+--------------+" + Environment.NewLine;
            expected += "| AAAA         |" + Environment.NewLine;
            expected += "+--------------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void TruncateBehaviourShouldTruncateLongString()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            listOfTestClasses[0].StringColumn = LongString;
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns[0].LongStringBehaviour = LongStringBehaviour.Truncate;

            tableObj.Columns[1].Hide = true; // hide non-string fields 
            tableObj.Columns[2].Hide = true;
            tableObj.Columns[3].Hide = true;

            // Act
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+-----------------+" + Environment.NewLine;
            expected += "| StringColumn    |" + Environment.NewLine;
            expected += "+-----------------+" + Environment.NewLine;
            expected += "| Lorem ipsum dol |" + Environment.NewLine;
            expected += "+-----------------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void TruncateWithEllipsisBehaviourShouldTruncateLongString()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            listOfTestClasses[0].StringColumn = LongString;
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns[0].LongStringBehaviour = LongStringBehaviour.TruncateWithEllipsis;

            tableObj.Columns[1].Hide = true; // hide non-string fields 
            tableObj.Columns[2].Hide = true;
            tableObj.Columns[3].Hide = true;

            // Act
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+-----------------+" + Environment.NewLine;
            expected += "| StringColumn    |" + Environment.NewLine;
            expected += "+-----------------+" + Environment.NewLine;
            expected += "| Lorem ipsum ... |" + Environment.NewLine;
            expected += "+-----------------+";
            tableString.ShouldBe(expected);
        }
    }
}
