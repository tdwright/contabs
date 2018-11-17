using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;

namespace ConTabs.Tests
{
    [TestFixture]
    public class ColumnTests
    {
        [Test]
        public void StringValForColWithNullDataShouldReturnEmptyString()
        {
            //Arrange
            var column = new Column(typeof(string), "StringColumn");

            //Act
            var result = column.StringValForCol(null);

            //Assert
            result.ShouldBe("");
        }

        [Test]
        public void MaxWidthWithNullValuesReturnsColumnNameLength()
        {
            //Arrange
            var column = new Column(typeof(string), "StringColumn");

            //Act
            var result = TableStretchStyles.DoNothing.CalculateOptimalWidth(column);

            //Assert
            result.ShouldBe(12);
        }

        [Test]
        public void MaxWidthWithZeroValuesReturnsColumnNameLength()
        {
            //Arrange
            var column = new Column(typeof(string), "StringColumn");
            column.Values = new List<object>();

            //Act
            var result = TableStretchStyles.DoNothing.CalculateOptimalWidth(column);

            //Assert
            result.ShouldBe(12);
        }

        [Test]
        public void MaxWidthWithLongStringBehaviourReturnsLongStringBehaviourWidth()
        {
            //Arrange
            var column = new Column(typeof(string), "StringColumn");
            column.Values = new List<object>() { null };
            column.LongStringBehaviour = LongStringBehaviour.Wrap;
            column.LongStringBehaviour.Width = 15;

            //Act
            var result = TableStretchStyles.DoNothing.CalculateOptimalWidth(column);

            //Assert
            result.ShouldBe(15);
        }

        [Test]
        public void MaxWidthWithValuesReturnsLengthOfLongestValue()
        {
            //Arrange
            var column = new Column(typeof(string), "data");
            column.Values = new List<object>() { "one", "two", "three", "four" };

            //Act
            var result = TableStretchStyles.DoNothing.CalculateOptimalWidth(column);

            //Assert
            result.ShouldBe(5);
        }
    }
}
