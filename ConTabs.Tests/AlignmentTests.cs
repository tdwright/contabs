using ConTabs.TestData;
using NUnit.Framework;
using Shouldly;

namespace ConTabs.Tests
{
    [TestFixture]
    public class AlignmentTests
    {
        [Test]
        public void DefaultColumnAlignmentShouldBeLeft()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);

            // Act
            // Assert
            var leftAlignedColumns = tableObj.Columns.FindAll(c => c.Alignment == Alignment.Left);
            leftAlignedColumns.Count.ShouldBe(tableObj.Columns.Count);
        }

        [Test]
        public void DefaultHeaderAlignmentShouldBeLeft()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);

            // Act
            // Assert
            tableObj.HeaderAlignment.ShouldBe(Alignment.Left);
        }

        [Test]
        public void SettingColumnAlignmentShouldSetAllColumnAlignments()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);

            // Act
            tableObj.ColumnAlignment = Alignment.Right;

            // Assert
            var rightAlignedColumns = tableObj.Columns.FindAll(c => c.Alignment == Alignment.Right);
            rightAlignedColumns.Count.ShouldBe(tableObj.Columns.Count);
        }

        [Test]
        public void SettingIndividualColumnAlignmentShouldOverwriteTableColumnAlignment()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);

            // Act
            tableObj.ColumnAlignment = Alignment.Right;
            tableObj.Columns[1].Alignment = Alignment.Center;

            // Assert
            tableObj.Columns[0].Alignment.ShouldBe(Alignment.Right);
            tableObj.Columns[1].Alignment.ShouldBe(Alignment.Center);
            tableObj.Columns[2].Alignment.ShouldBe(Alignment.Right);
            tableObj.Columns[3].Alignment.ShouldBe(Alignment.Right);
        }

        [Test]
        public void SettingIndividualColumnAlignmentShouldNotChangeTableColumnAlignment()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);

            // Act
            tableObj.ColumnAlignment = Alignment.Right;
            tableObj.Columns[1].Alignment = Alignment.Center;

            // Assert
            tableObj.ColumnAlignment.ShouldBe(Alignment.Right);
        }

        [Test]
        public void LeftAlignmentShouldLookLikeThis()
        {
            // Arrange
            var input = "My input";
            var colMaxWidth = 12;
            var alignment = Alignment.Left;

            // Act
            var result = alignment.ProcessString(input, colMaxWidth);

            // Assert
            result.ShouldBe("My input    ");
        }

        [Test]
        public void RightAlignmentShouldLookLikeThis()
        {
            // Arrange
            var input = "My input";
            var colMaxWidth = 12;
            var alignment = Alignment.Right;

            // Act
            var result = alignment.ProcessString(input, colMaxWidth);

            // Assert
            result.ShouldBe("    My input");
        }

        [Test]
        public void CenterAlignmentWithEvenColumnMaxWidthShouldLookLikeThis()
        {
            // Arrange
            var input = "My input";
            var colMaxWidth = 12;
            var alignment = Alignment.Center;

            // Act
            var result = alignment.ProcessString(input, colMaxWidth);

            // Assert
            result.ShouldBe("  My input  ");
        }

        [Test]
        public void CenterAlignmentWithOddColumnMaxWidthShouldLookLikeThis()
        {
            // Arrange
            var input = "My input";
            var colMaxWidth = 13;
            var alignment = Alignment.Center;

            // Act
            var result = alignment.ProcessString(input, colMaxWidth);

            // Assert
            result.ShouldBe("  My input   ");
        }
    }
}
