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
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns[0].LongStringBehaviour = LongStringBehaviour.Default;

            // Act
            var processedString = tableObj.Columns[0].StringValForCol(LongString);

            // Assert
            processedString.ShouldBe(LongString);
        }

        [Test]
        public void TruncateBehaviourShouldNotTruncateShortString()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns[0].LongStringBehaviour = LongStringBehaviour.Truncate;

            // Act
            var processedString = tableObj.Columns[0].StringValForCol("AAAA");

            // Assert
            processedString.ShouldBe("AAAA");
        }

        [Test]
        public void TruncateBehaviourShouldTruncateLongString()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns[0].LongStringBehaviour = LongStringBehaviour.Truncate;

            // Act
            var processedString = tableObj.Columns[0].StringValForCol(LongString);

            // Assert
            processedString.ShouldBe("Lorem ipsum dol");
        }

        [Test]
        public void TruncateWithEllipsisBehaviourShouldTruncateLongString()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns[0].LongStringBehaviour = LongStringBehaviour.TruncateWithEllipsis;

            // Act
            var processedString = tableObj.Columns[0].StringValForCol(LongString);

            // Assert
            processedString.ShouldBe("Lorem ipsum ...");
        }

        [Test]
        public void WordWrapBehaviourShouldWrapLongString()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns[0].LongStringBehaviour = LongStringBehaviour.Wrap;

            // Act
            var processedString = tableObj.Columns[0].StringValForCol(LongString);

            // Assert
            processedString.ShouldBe("" +
                "Lorem ipsum dolor sit" + Environment.NewLine +
                "amet, consectetur" + Environment.NewLine +
                "adipiscing elit. Donec" + Environment.NewLine +
                "vehicula fringilla" + Environment.NewLine +
                "tortor.");
        }
    }
}
