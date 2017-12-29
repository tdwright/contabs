using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using ConTabs.Exceptions;
using ConTabs.TestData;
using NUnit.Framework.Constraints;

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
    }

    
}
