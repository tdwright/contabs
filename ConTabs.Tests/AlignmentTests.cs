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
	}
}
