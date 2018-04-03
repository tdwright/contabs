using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using ConTabs.Exceptions;
using ConTabs.TestData;

namespace ConTabs.Tests
{
    [TestFixture]
    public class ConTab_Table_BasicTests
    {
        [Test]
        public void TableObjectCanBeConstructedWithoutData()
        {
            // Act
            var table = Table<string>.Create();

            // Assert
            table.ToString().Length.ShouldBeGreaterThan(0);
        }

        [Test]
        public void TableObjectCanBeConstructedWithData()
        {
            // Arrange
            var listOfStrings = new List<string>();

            // Act
            var table = Table<string>.Create(listOfStrings);

            // Assert
            table.ToString().Length.ShouldBeGreaterThan(0);
        }

        [Test]
        public void TableObjectHasExpectedNumberOfColumns()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData();

            // Act
            var table = Table<TestDataType>.Create(listOfTestClasses);

            // Assert
            table.Columns.Count.ShouldBe(4);
        }

        [Test]
        public void Table_GivenClassWithoutPublicProperties_ThrowsPublicPropertiesNotFoundException()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfInvalidTestData();

            // Act
            TestDelegate testDelegate = () => Table<InvalidTestDataType>.Create(listOfTestClasses);

            // Assert
            Assert.Throws<PublicPropertiesNotFoundException>(testDelegate);
        }

        [Test]
        public void Table_GivenClassWithoutPublicProperties_ThrowsExceptionWithHelpfulMessage()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfInvalidTestData();

            // Act
            TestDelegate testDelegate = () => Table<InvalidTestDataType>.Create(listOfTestClasses);

            // Assert
            Assert.Throws<PublicPropertiesNotFoundException>(testDelegate).Message.ShouldContain("no valid properties");
        }

        [Test]
        public void Columns_GivenInvalidColumnName_ThrowsColumnNotFoundExceptionWithAppropriateMessage()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData();
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);

            // Act
            TestDelegate testDelegate = () => tableObj.Columns["nonexistant"].Hide = true;

            // Assert
            Assert.Throws<ColumnNotFoundException>(testDelegate).Message.ShouldContain("name");
        }

        [Test]
        public void Columns_GivenInvalidColumnIndex_ThrowsColumnNotFoundExceptionWithAppropriateMessage()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData();
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);

            // Act
            TestDelegate testDelegate = () => tableObj.Columns[999].Hide = true;

            // Assert
            Assert.Throws<ColumnNotFoundException>(testDelegate).Message.ShouldContain("index");
        }

        [Test]
        public void Table_AllColumnsHidden_ThrowsEmptyTableExceptionWithAppropriateMessage()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData();
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);

            foreach (var col in tableObj.Columns)
                col.Hide = true;

            // Act
            TestDelegate testDelegate = () => tableObj.ToString();

            // Assert
            Assert.Throws<EmptyTableException>(testDelegate).Message.ShouldContain("no visible columns");
        }
    }

    
}
