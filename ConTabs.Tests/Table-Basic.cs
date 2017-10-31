using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;

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
            var listOfTestClasses = new List<TestDataType>();

            // Act
            var table = Table<TestDataType>.Create(listOfTestClasses);

            // Assert
            table.Columns.Count.ShouldBe(4);
        }

    }

    internal class TestDataType
    {
        public string StringColumn { get; set; }
        public int IntColumn { get; set; }
        public decimal CurrencyColumn { get; set; }
        public DateTime DateTimeColumn { get; set; }
        private string HiddenProp { get; set; }
    }
}
