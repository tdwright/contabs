using NUnit.Framework;
using Shouldly;
using System;
using ConTabs.TestData;

namespace ConTabs.Tests
{
    [TestFixture]
    public class ConformanceTests
    {
        [Test]
        public void BasicTableWithNoDataShouldLookLikeThis()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData(0);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);

            // Act
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+--------------+-----------+----------------+----------------+" + Environment.NewLine;
            expected += "| StringColumn | IntColumn | CurrencyColumn | DateTimeColumn |" + Environment.NewLine;
            expected += "+--------------+-----------+----------------+----------------+" + Environment.NewLine;
            expected += "|                          no data                           |" + Environment.NewLine;
            expected += "+--------------+-----------+----------------+----------------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void BasicTableWithOneLineOfDataShouldLookLikeThis()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns[3].Hide = true; // hide date field 

            // Act
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+--------------+-----------+----------------+" + Environment.NewLine;
            expected += "| StringColumn | IntColumn | CurrencyColumn |" + Environment.NewLine;
            expected += "+--------------+-----------+----------------+" + Environment.NewLine;
            expected += "| AAAA         | 999       | 19.95          |" + Environment.NewLine;
            expected += "+--------------+-----------+----------------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void TableStyledAsUnicodePipesShouldLookLikeThis()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfMinimalData(1);
            var tableObj = Table<MinimalDataType>.Create(listOfTestClasses);
            tableObj.TableStyle = Style.UnicodePipes;

            // Act
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "╔══════╦══════╗" + Environment.NewLine;
            expected += "║ IntA ║ IntB ║" + Environment.NewLine;
            expected += "╠══════╬══════╣" + Environment.NewLine;
            expected += "║ 1    ║ 3    ║" + Environment.NewLine;
            expected += "╚══════╩══════╝";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void TableStyledAsHeavyShouldLookLikeThis()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfMinimalData(1);
            var tableObj = Table<MinimalDataType>.Create(listOfTestClasses);
            tableObj.TableStyle = Style.Heavy;

            // Act
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "#======#======#" + Environment.NewLine;
            expected += "# IntA # IntB #" + Environment.NewLine;
            expected += "#======#======#" + Environment.NewLine;
            expected += "# 1    # 3    #" + Environment.NewLine;
            expected += "#======#======#";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void TableWithCustomStyleShouldLookLikeThis()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfMinimalData(1);
            var tableObj = Table<MinimalDataType>.Create(listOfTestClasses);
            tableObj.TableStyle = new Style('W', 'F', 'C');

            // Act
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "CFFFFFFCFFFFFFC" + Environment.NewLine;
            expected += "W IntA W IntB W" + Environment.NewLine;
            expected += "CFFFFFFCFFFFFFC" + Environment.NewLine;
            expected += "W 1    W 3    W" + Environment.NewLine;
            expected += "CFFFFFFCFFFFFFC";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void TableStyledAsUnicodeLinesShouldLookLikeThis()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfMinimalData(1);
            var tableObj = Table<MinimalDataType>.Create(listOfTestClasses);
            tableObj.TableStyle = Style.UnicodeLines;

            // Act
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "┌──────┬──────┐" + Environment.NewLine;
            expected += "│ IntA │ IntB │" + Environment.NewLine;
            expected += "├──────┼──────┤" + Environment.NewLine;
            expected += "│ 1    │ 3    │" + Environment.NewLine;
            expected += "└──────┴──────┘";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void TableStyledAsUnicodeArcsShouldLookLikeThis()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfMinimalData(1);
            var tableObj = Table<MinimalDataType>.Create(listOfTestClasses);
            tableObj.TableStyle = Style.UnicodeArcs;

            // Act
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "╭──────┬──────╮" + Environment.NewLine;
            expected += "│ IntA │ IntB │" + Environment.NewLine;
            expected += "├──────┼──────┤" + Environment.NewLine;
            expected += "│ 1    │ 3    │" + Environment.NewLine;
            expected += "╰──────┴──────╯";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void BasicTableWithWrappedStringShouldLookLikeThis()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            listOfTestClasses[0].StringColumn = "This string will need to be wrapped";
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns[3].Hide = true; // hide date field 

            // Act
            tableObj.Columns[0].LongStringBehaviour = LongStringBehaviour.Wrap;
            tableObj.Columns[0].LongStringBehaviour.Width = 12;
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+--------------+-----------+----------------+" + Environment.NewLine;
            expected += "| StringColumn | IntColumn | CurrencyColumn |" + Environment.NewLine;
            expected += "+--------------+-----------+----------------+" + Environment.NewLine;
            expected += "| This string  | 999       | 19.95          |" + Environment.NewLine;
            expected += "| will need to |           |                |" + Environment.NewLine;
            expected += "| be wrapped   |           |                |" + Environment.NewLine;
            expected += "+--------------+-----------+----------------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void BasicTableWithTruncatedStringShouldLookLikeThis()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            listOfTestClasses[0].StringColumn = "This string will need to be wrapped";
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns[3].Hide = true; // hide date field 

            // Act
            tableObj.Columns[0].LongStringBehaviour = LongStringBehaviour.TruncateWithEllipsis;
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+-----------------+-----------+----------------+" + Environment.NewLine;
            expected += "| StringColumn    | IntColumn | CurrencyColumn |" + Environment.NewLine;
            expected += "+-----------------+-----------+----------------+" + Environment.NewLine;
            expected += "| This string ... | 999       | 19.95          |" + Environment.NewLine;
            expected += "+-----------------+-----------+----------------+";
            tableString.ShouldBe(expected);
        }
    }

}
