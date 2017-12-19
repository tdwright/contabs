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

            // Act - using try-catch due to static builder pattern, which precludes use of Assert.Throws.
            var message = string.Empty;
            try
            {
                var table = Table<InvalidTestDataType>.Create(listOfTestClasses);
            }
            catch (PublicPropertiesNotFoundException e)
            {
                message = e.Message;
            }
            
            // Assert
            Assert.AreEqual(message, "On Table<T> creation, no valid properties were identified. Check access modifiers.");
        }
    }

    
}
