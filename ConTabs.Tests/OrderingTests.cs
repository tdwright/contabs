using NUnit.Framework;
using Shouldly;
using ConTabs.Exceptions;
using ConTabs.TestData;

namespace ConTabs.Tests
{
    [TestFixture]
    public class OrderingTests
    {
        [Test]
        public void ColumnCanBeMovedUsingIndex()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData(0);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);

            // Act
            tableObj.Columns.MoveColumn(2, 1);

            // Assert
            tableObj.Columns[1].ColumnName.ShouldBe("CurrencyColumn");
        }

        [Test]
        public void ColumnCanBeMovedUsingName()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData(0);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);

            // Act
            tableObj.Columns.MoveColumn("CurrencyColumn", 1);

            // Assert
            tableObj.Columns[1].ColumnName.ShouldBe("CurrencyColumn");
        }

        [Test]
        public void WhenColumnMoved_LowerColsMoveAlong()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData(0);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);

            // Act
            tableObj.Columns.MoveColumn(2, 1);

            // Assert
            tableObj.Columns[2].ColumnName.ShouldBe("IntColumn");
        }

        [Test]
        public void WhenColumnMoved_UpperColsStayPut()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData(0);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);

            // Act
            tableObj.Columns.MoveColumn(2, 1);

            // Assert
            tableObj.Columns[0].ColumnName.ShouldBe("StringColumn");
        }

        [Test]
        public void AttemptToMoveColToInvalidIndex_ThrowsException()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData(0);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);

            // Act
            TestDelegate testDelegate = () => tableObj.Columns.MoveColumn(99, 1);

            // Assert
            Assert.Throws<ColumnNotFoundException>(testDelegate);
        }
    }
}
