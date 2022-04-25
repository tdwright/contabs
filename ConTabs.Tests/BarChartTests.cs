using ConTabs.TestData;
using NUnit.Framework;
using Shouldly;
using System;

namespace ConTabs.Tests
{
    [TestFixture]
    public class BarChartTests
    {
        [Test]
        public void GenerateBarChart_ResultsInNewColumnAdded()
        {
            // Arrange
            var data = DataProvider.ListOfMinimalData();
            var table = Table.Create(data);

            // Act
            table.Columns.AddBarChart<int>("Chart", table.Columns["IntB"]);

            // Assert
            table.Columns.Count.ShouldBe(3);
        }

        [Test]
        public void GenerateBarChart_ResultsInStringColumnAdded()
        {
            // Arrange
            var data = DataProvider.ListOfMinimalData();
            var table = Table.Create(data);

            // Act
            table.Columns.AddBarChart<int>("Chart", table.Columns["IntB"]);

            // Assert
            table.Columns[2].SourceType.ShouldBe(typeof(string));
        }

        [Test]
        public void GenerateBarChart_WithDefaultParams_BarsAreExpectedSize()
        {
            // Arrange
            var data = DataProvider.ListOfMinimalData(3); // IntB = 3, 9, 27
            var table = Table.Create(data);

            // Act
            table.Columns.AddBarChart<int>("Chart", table.Columns["IntB"]); // Max = 27, default width = 25, unit scale = 25/27 = 0.92592...

            // Assert
            table.Columns[2].Values[0].ShouldBe("###");                         // 0.92592 *  3 =  2.77 = 3
            table.Columns[2].Values[1].ShouldBe("########");                    // 0.92592 *  9 =  8.33 = 8
            table.Columns[2].Values[2].ShouldBe("#########################");   // 0.92592 * 27 = 25.00 = 25
        }

        [Test]
        public void GenerateBarChart_MaxWidthCanBeSpecified()
        {
            // Arrange
            var data = DataProvider.ListOfMinimalData(1); // single row, IntB = 3
            var table = Table.Create(data);

            // Act
            table.Columns.AddBarChart<int>("Chart", table.Columns["IntB"], maxLength: 3);

            // Assert
            table.Columns[2].Values[0].ShouldBe("###");
        }

        [Test]
        public void GenerateBarChart_ForSingleValues_ResultsInFullWidthBar()
        {
            // Arrange
            var data = DataProvider.ListOfMinimalData(1); // single row, IntB = 3
            var table = Table.Create(data);

            // Act
            table.Columns.AddBarChart<int>("Chart", table.Columns["IntB"], maxLength: 10);

            // Assert
            table.Columns[2].Values[0].ShouldBe("##########"); // count = 10 = maxLength
        }

        [Test]
        public void GenerateBarChart_UnitCharCanBeChanged()
        {
            // Arrange
            var data = DataProvider.ListOfMinimalData(1); // single row, IntB = 3
            var table = Table.Create(data);

            // Act
            table.Columns.AddBarChart<int>("Chart", table.Columns["IntB"], maxLength:3, unitChar: '>');

            // Assert
            table.Columns[2].Values[0].ShouldBe(">>>");
        }

        [Test]
        public void GenerateBarChart_ScaleCanBeSpecified()
        {
            // Arrange
            var data = DataProvider.ListOfMinimalData(1); // single row, IntB = 3
            var table = Table.Create(data);

            // Act
            table.Columns.AddBarChart<int>("Chart", table.Columns["IntB"], unitSize: 3);

            // Assert
            table.Columns[2].Values[0].ShouldBe("#"); // one unit of size 3, for a value of 3
        }

        [Test]
        public void GenerateBarChart_WhenBothScaleAndMaxWidthAreSpecified_ScaleTakesPrecedence()
        {
            // Arrange
            var data = DataProvider.ListOfMinimalData(1); // single row, IntB = 3
            var table = Table.Create(data);

            // Act
            table.Columns.AddBarChart<int>("Chart", table.Columns["IntB"], unitSize: 3, maxLength: 9);

            // Assert
            table.Columns[2].Values[0].ShouldBe("#"); // one unit of size 3, for a value of 3, rather than 100% of maxLength
        }

        [Test]
        public void GenerateBarChart_WhenBothScaleAndMaxWidthAreSpecified_NeverExceedsMax()
        {
            // Arrange
            var data = DataProvider.ListOfMinimalData(3); // single row, IntB = 3
            var table = Table.Create(data);

            // Act
            table.Columns.AddBarChart<int>("Chart", table.Columns["IntB"], unitSize: 3, maxLength: 5);

            // Assert
            table.Columns[2].Values[0].ShouldBe("#");
            table.Columns[2].Values[1].ShouldBe("###");
            table.Columns[2].Values[2].ShouldBe("#####"); // capped at 5 instead of value 27 / scale 3 = 9 units
        }

        [Test]
        public void GenerateBarChart_NegativeValuesGetHandledGracefully()
        {
            // Arrange
            var data = DataProvider.ListOfMinimalData(1); // single row, IntB = 3
            data[0].IntB = -1;
            var table = Table.Create(data);

            // Act
            table.Columns.AddBarChart<int>("Chart", table.Columns["IntB"]);

            // Assert
            table.Columns[2].Values[0].ShouldBe("");
        }

        [Test]
        public void GenerateBarChart_ConformanceTest()
        {
            // Arrange
            var data = DataProvider.ListOfMinimalData(3); // IntB = 3, 9, 27
            var table = Table.Create(data);

            // Act
            table.Columns.AddBarChart<int>("Chart", table.Columns["IntB"], maxLength: 10, unitChar: '=');

            // Assert
            string expected = "";
            expected += "+------+------+------------+" + Environment.NewLine;
            expected += "| IntA | IntB | Chart      |" + Environment.NewLine;
            expected += "+------+------+------------+" + Environment.NewLine;
            expected += "| 1    | 3    | =          |" + Environment.NewLine;
            expected += "| 2    | 9    | ===        |" + Environment.NewLine;
            expected += "| 3    | 27   | ========== |" + Environment.NewLine;
            expected += "+------+------+------------+";

            table.ToString().ShouldBe(expected);
        }
    }
}