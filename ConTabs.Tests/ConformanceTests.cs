﻿using NUnit.Framework;
using Shouldly;
using System;
using System.Threading;
using System.Globalization;
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
        public void BasicTableWithNoDataAndExplicitPaddingShouldLookLikeThis()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData(0);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Padding = new Padding(0);

            // Act
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+------------+---------+--------------+--------------+" + Environment.NewLine;
            expected += "|StringColumn|IntColumn|CurrencyColumn|DateTimeColumn|" + Environment.NewLine;
            expected += "+------------+---------+--------------+--------------+" + Environment.NewLine;
            expected += "|                      no data                       |" + Environment.NewLine;
            expected += "+------------+---------+--------------+--------------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void BasicTableWithOneLineOfDataShouldLookLikeThis()
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
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
        public void BasicTableWithOneLineOfDataAndExplicitPaddingShouldLookLikeThis()
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns[3].Hide = true; // hide date field 
            tableObj.Padding = new Padding(2);

            // Act
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+----------------+-------------+------------------+" + Environment.NewLine;           
            expected += "|  StringColumn  |  IntColumn  |  CurrencyColumn  |" + Environment.NewLine;
            expected += "+----------------+-------------+------------------+" + Environment.NewLine;
            expected += "|  AAAA          |  999        |  19.95           |" + Environment.NewLine;
            expected += "+----------------+-------------+------------------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void BasicTableWithOneLineOfDataAndEmptyStringValueShouldLookLikeThis()
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            listOfTestClasses[0].StringColumn = string.Empty;
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns[3].Hide = true; // hide date field 

            // Act
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+--------------+-----------+----------------+" + Environment.NewLine;
            expected += "| StringColumn | IntColumn | CurrencyColumn |" + Environment.NewLine;
            expected += "+--------------+-----------+----------------+" + Environment.NewLine;
            expected += "|              | 999       | 19.95          |" + Environment.NewLine;
            expected += "+--------------+-----------+----------------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void BasicTableWithOneLineOfDataAndEmptyStringValueWithExplicitPaddingShouldLookLikeThis()
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            listOfTestClasses[0].StringColumn = string.Empty;
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns[3].Hide = true; // hide date field 
            tableObj.Padding = new Padding(0, 4);

            // Act
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+--------------------+-----------------+----------------------+" + Environment.NewLine;
            expected += "|    StringColumn    |    IntColumn    |    CurrencyColumn    |" + Environment.NewLine;
            expected += "+--------------------+-----------------+----------------------+" + Environment.NewLine;
            expected += "|                    |    999          |    19.95             |" + Environment.NewLine;
            expected += "+--------------------+-----------------+----------------------+";
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
        public void TableStyledAsUnicodePipesWithExplicitPaddingShouldLookLikeThis()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfMinimalData(1);
            var tableObj = Table<MinimalDataType>.Create(listOfTestClasses);
            tableObj.TableStyle = Style.UnicodePipes;
            tableObj.Padding = new Padding(0, 0);

            // Act
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "╔════╦════╗" + Environment.NewLine;
            expected += "║IntA║IntB║" + Environment.NewLine;
            expected += "╠════╬════╣" + Environment.NewLine;
            expected += "║1   ║3   ║" + Environment.NewLine;
            expected += "╚════╩════╝";
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
        public void TableStyledAsHashesShouldLookLikeThis()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfMinimalData(1);
            var tableObj = Table<MinimalDataType>.Create(listOfTestClasses);
            tableObj.TableStyle = Style.Hash;

            // Act
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "###############" + Environment.NewLine;
            expected += "# IntA # IntB #" + Environment.NewLine;
            expected += "###############" + Environment.NewLine;
            expected += "# 1    # 3    #" + Environment.NewLine;
            expected += "###############";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void TableStyledAsPlussesShouldLookLikeThis()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfMinimalData(1);
            var tableObj = Table<MinimalDataType>.Create(listOfTestClasses);
            tableObj.TableStyle = Style.Plus;

            // Act
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+++++++++++++++" + Environment.NewLine;
            expected += "+ IntA + IntB +" + Environment.NewLine;
            expected += "+++++++++++++++" + Environment.NewLine;
            expected += "+ 1    + 3    +" + Environment.NewLine;
            expected += "+++++++++++++++";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void TableStyledAsWhitespaceShouldLookLikeThis()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfMinimalData(1);
            var tableObj = Table<MinimalDataType>.Create(listOfTestClasses);
            tableObj.TableStyle = Style.Whitespace;

            // Act
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "               " + Environment.NewLine;
            expected += "  IntA   IntB  " + Environment.NewLine;
            expected += "               " + Environment.NewLine;
            expected += "  1      3     " + Environment.NewLine;
            expected += "               ";
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
        public void TableStyledAsUnicodeArcsWithExplicitPaddingShouldLookLikeThis()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfMinimalData(1);
            var tableObj = Table<MinimalDataType>.Create(listOfTestClasses);
            tableObj.TableStyle = Style.UnicodeArcs;
            tableObj.Padding = new Padding(1, 2);

            // Act
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "╭────────┬────────╮" + Environment.NewLine;
            expected += "│        │        │" + Environment.NewLine;
            expected += "│  IntA  │  IntB  │" + Environment.NewLine;
            expected += "│        │        │" + Environment.NewLine;
            expected += "├────────┼────────┤" + Environment.NewLine;
            expected += "│        │        │" + Environment.NewLine;
            expected += "│  1     │  3     │" + Environment.NewLine;
            expected += "│        │        │" + Environment.NewLine;
            expected += "╰────────┴────────╯";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void TableStyledAsDotsShouldLookLikeThis()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfMinimalData(1);
            var tableObj = Table<MinimalDataType>.Create(listOfTestClasses);
            tableObj.TableStyle = Style.Dots;

            // Act
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "..............." + Environment.NewLine;
            expected += ": IntA : IntB :" + Environment.NewLine;
            expected += ":......:......:" + Environment.NewLine;
            expected += ": 1    : 3    :" + Environment.NewLine;
            expected += ":......:......:";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void TableStyledAsUnicodeDoubleWalledShouldLookLikeThis()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfMinimalData(1);
            var tableObj = Table<MinimalDataType>.Create(listOfTestClasses);
            tableObj.TableStyle = Style.UnicodeDoubleWalled;

            // Act
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "╓──────╥──────╖" + Environment.NewLine;
            expected += "║ IntA ║ IntB ║" + Environment.NewLine;
            expected += "╟──────╫──────╢" + Environment.NewLine;
            expected += "║ 1    ║ 3    ║" + Environment.NewLine;
            expected += "╙──────╨──────╜";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void TableStyledAsUnicodeDoubleWalledWithExplicitPaddingShouldLookLikeThis()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfMinimalData(1);
            var tableObj = Table<MinimalDataType>.Create(listOfTestClasses);
            tableObj.TableStyle = Style.UnicodeDoubleWalled;
            tableObj.Padding = new Padding(1, 2);

            // Act
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "╓────────╥────────╖" + Environment.NewLine;
            expected += "║        ║        ║" + Environment.NewLine;
            expected += "║  IntA  ║  IntB  ║" + Environment.NewLine;
            expected += "║        ║        ║" + Environment.NewLine;
            expected += "╟────────╫────────╢" + Environment.NewLine;
            expected += "║        ║        ║" + Environment.NewLine;
            expected += "║  1     ║  3     ║" + Environment.NewLine;
            expected += "║        ║        ║" + Environment.NewLine;
            expected += "╙────────╨────────╜";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void TableStyledAsUnicodeDoubleFlooredShouldLookLikeThis()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfMinimalData(1);
            var tableObj = Table<MinimalDataType>.Create(listOfTestClasses);
            tableObj.TableStyle = Style.UnicodeDoubleFloored;

            // Act
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "╒══════╤══════╕" + Environment.NewLine;
            expected += "│ IntA │ IntB │" + Environment.NewLine;
            expected += "╞══════╪══════╡" + Environment.NewLine;
            expected += "│ 1    │ 3    │" + Environment.NewLine;
            expected += "╘══════╧══════╛";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void TableStyledAsUnicodeDoubleFlooredWithExplicitPaddingShouldLookLikeThis()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfMinimalData(1);
            var tableObj = Table<MinimalDataType>.Create(listOfTestClasses);
            tableObj.TableStyle = Style.UnicodeDoubleFloored;
            tableObj.Padding = new Padding(1, 2);

            // Act
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "╒════════╤════════╕" + Environment.NewLine;
            expected += "│        │        │" + Environment.NewLine;
            expected += "│  IntA  │  IntB  │" + Environment.NewLine;
            expected += "│        │        │" + Environment.NewLine;
            expected += "╞════════╪════════╡" + Environment.NewLine;
            expected += "│        │        │" + Environment.NewLine;
            expected += "│  1     │  3     │" + Environment.NewLine;
            expected += "│        │        │" + Environment.NewLine;
            expected += "╘════════╧════════╛";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void BasicTableWithWrappedStringShouldLookLikeThis()
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
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
        public void BasicTableWithWrappedStringWithExplicitPaddingShouldLookLikeThis()
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            listOfTestClasses[0].StringColumn = "This string will need to be wrapped";
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns[3].Hide = true; // hide date field 
            tableObj.Padding = new Padding()
            {
                Left = 2,
                Right = 1,
                Top = 1,
                Bottom = 2
            };

            // Act
            tableObj.Columns[0].LongStringBehaviour = LongStringBehaviour.Wrap;
            tableObj.Columns[0].LongStringBehaviour.Width = 12;
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+---------------+------------+-----------------+" + Environment.NewLine;
            expected += "|               |            |                 |" + Environment.NewLine;
            expected += "|  StringColumn |  IntColumn |  CurrencyColumn |" + Environment.NewLine;
            expected += "|               |            |                 |" + Environment.NewLine;
            expected += "|               |            |                 |" + Environment.NewLine;
            expected += "+---------------+------------+-----------------+" + Environment.NewLine;            
            expected += "|               |            |                 |" + Environment.NewLine;
            expected += "|  This string  |  999       |  19.95          |" + Environment.NewLine;
            expected += "|  will need to |            |                 |" + Environment.NewLine;
            expected += "|  be wrapped   |            |                 |" + Environment.NewLine;
            expected += "|               |            |                 |" + Environment.NewLine;
            expected += "|               |            |                 |" + Environment.NewLine;
            expected += "+---------------+------------+-----------------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void BasicTableWithWrappedStringAndRightAlignmentShouldLookLikeThis()
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            listOfTestClasses[0].StringColumn = "This string will need to be wrapped";
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns[3].Hide = true; // hide date field 

            // Act
            tableObj.Columns[0].LongStringBehaviour = LongStringBehaviour.Wrap;
            tableObj.Columns[0].LongStringBehaviour.Width = 12;
            tableObj.Columns[0].Alignment = Alignment.Right;
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+--------------+-----------+----------------+" + Environment.NewLine;
            expected += "| StringColumn | IntColumn | CurrencyColumn |" + Environment.NewLine;
            expected += "+--------------+-----------+----------------+" + Environment.NewLine;
            expected += "|  This string | 999       | 19.95          |" + Environment.NewLine;
            expected += "| will need to |           |                |" + Environment.NewLine;
            expected += "|   be wrapped |           |                |" + Environment.NewLine;
            expected += "+--------------+-----------+----------------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void BasicTableWithWrappedStringAndRightAlignmentWithExplicitPaddingShouldLookLikeThis()
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            listOfTestClasses[0].StringColumn = "This string will need to be wrapped";
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns[3].Hide = true; // hide date field 
            tableObj.Padding = new Padding(0, 1);

            // Act
            tableObj.Columns[0].LongStringBehaviour = LongStringBehaviour.Wrap;
            tableObj.Columns[0].LongStringBehaviour.Width = 12;
            tableObj.Columns[0].Alignment = Alignment.Right;
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+--------------+-----------+----------------+" + Environment.NewLine;
            expected += "| StringColumn | IntColumn | CurrencyColumn |" + Environment.NewLine;
            expected += "+--------------+-----------+----------------+" + Environment.NewLine;
            expected += "|  This string | 999       | 19.95          |" + Environment.NewLine;
            expected += "| will need to |           |                |" + Environment.NewLine;
            expected += "|   be wrapped |           |                |" + Environment.NewLine;
            expected += "+--------------+-----------+----------------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void BasicTableWithWrappedStringAndCenterAlignmentShouldLookLikeThis()
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            listOfTestClasses[0].StringColumn = "This string will need to be wrapped";
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns[3].Hide = true; // hide date field 

            // Act
            tableObj.Columns[0].LongStringBehaviour = LongStringBehaviour.Wrap;
            tableObj.Columns[0].LongStringBehaviour.Width = 12;
            tableObj.Columns[0].Alignment = Alignment.Center;
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+--------------+-----------+----------------+" + Environment.NewLine;
            expected += "| StringColumn | IntColumn | CurrencyColumn |" + Environment.NewLine;
            expected += "+--------------+-----------+----------------+" + Environment.NewLine;
            expected += "| This string  | 999       | 19.95          |" + Environment.NewLine;
            expected += "| will need to |           |                |" + Environment.NewLine;
            expected += "|  be wrapped  |           |                |" + Environment.NewLine;
            expected += "+--------------+-----------+----------------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void BasicTableWithWrappedStringAndCenterAlignmentWithExplicitPaddingShouldLookLikeThis()
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            listOfTestClasses[0].StringColumn = "This string will need to be wrapped";
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns[3].Hide = true; // hide date field 
            tableObj.Padding = new Padding(2);

            // Act
            tableObj.Columns[0].LongStringBehaviour = LongStringBehaviour.Wrap;
            tableObj.Columns[0].LongStringBehaviour.Width = 12;
            tableObj.Columns[0].Alignment = Alignment.Center;
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+----------------+-------------+------------------+" + Environment.NewLine;       
            expected += "|  StringColumn  |  IntColumn  |  CurrencyColumn  |" + Environment.NewLine;
            expected += "+----------------+-------------+------------------+" + Environment.NewLine;
            expected += "|  This string   |  999        |  19.95           |" + Environment.NewLine;
            expected += "|  will need to  |             |                  |" + Environment.NewLine;
            expected += "|   be wrapped   |             |                  |" + Environment.NewLine;
            expected += "+----------------+-------------+------------------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void BasicTableWithTruncatedStringShouldLookLikeThis()
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
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

        [Test]
        public void BasicTableWithTruncatedStringWithExplicitPaddingShouldLookLikeThis()
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            listOfTestClasses[0].StringColumn = "This string will need to be wrapped";
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns[3].Hide = true; // hide date field 
            tableObj.Padding = new Padding(0, 3);

            // Act
            tableObj.Columns[0].LongStringBehaviour = LongStringBehaviour.TruncateWithEllipsis;
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+---------------------+---------------+--------------------+" + Environment.NewLine;
            expected += "|   StringColumn      |   IntColumn   |   CurrencyColumn   |" + Environment.NewLine;
            expected += "+---------------------+---------------+--------------------+" + Environment.NewLine;
            expected += "|   This string ...   |   999         |   19.95            |" + Environment.NewLine;
            expected += "+---------------------+---------------+--------------------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void BasicTableWithRightColumnAlignmentShouldLookLikeThis()
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns[3].Hide = true; // hide date field 

            // Act
            tableObj.ColumnAlignment = Alignment.Right;
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+--------------+-----------+----------------+" + Environment.NewLine;
            expected += "| StringColumn | IntColumn | CurrencyColumn |" + Environment.NewLine;
            expected += "+--------------+-----------+----------------+" + Environment.NewLine;
            expected += "|         AAAA |       999 |          19.95 |" + Environment.NewLine;
            expected += "+--------------+-----------+----------------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void BasicTableWithCenterColumnAlignmentShouldLookLikeThis()
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns[3].Hide = true; // hide date field 

            // Act
            tableObj.ColumnAlignment = Alignment.Center;
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+--------------+-----------+----------------+" + Environment.NewLine;
            expected += "| StringColumn | IntColumn | CurrencyColumn |" + Environment.NewLine;
            expected += "+--------------+-----------+----------------+" + Environment.NewLine;
            expected += "|     AAAA     |    999    |     19.95      |" + Environment.NewLine;
            expected += "+--------------+-----------+----------------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void BasicTableWithRightHeaderAlignmentShouldLookLikeThis()
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            listOfTestClasses[0].StringColumn = "Longer than header string";
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns[3].Hide = true; // hide date field 

            // Act
            tableObj.HeaderAlignment = Alignment.Right;
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+---------------------------+-----------+----------------+" + Environment.NewLine;
            expected += "|              StringColumn | IntColumn | CurrencyColumn |" + Environment.NewLine;
            expected += "+---------------------------+-----------+----------------+" + Environment.NewLine;
            expected += "| Longer than header string | 999       | 19.95          |" + Environment.NewLine;
            expected += "+---------------------------+-----------+----------------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void BasicTableWithRightHeaderAlignmentWithExplicitPaddingShouldLookLikeThis()
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            listOfTestClasses[0].StringColumn = "Longer than header string";
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns[3].Hide = true; // hide date field 
            tableObj.Padding = new Padding(0);

            // Act
            tableObj.HeaderAlignment = Alignment.Right;
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+-------------------------+---------+--------------+" + Environment.NewLine;
            expected += "|             StringColumn|IntColumn|CurrencyColumn|" + Environment.NewLine;
            expected += "+-------------------------+---------+--------------+" + Environment.NewLine;
            expected += "|Longer than header string|999      |19.95         |" + Environment.NewLine;
            expected += "+-------------------------+---------+--------------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void BasicTableWithCenterHeaderAlignmentShouldLookLikeThis()
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            listOfTestClasses[0].StringColumn = "Longer than header string";
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns[3].Hide = true; // hide date field 

            // Act
            tableObj.HeaderAlignment = Alignment.Center;
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+---------------------------+-----------+----------------+" + Environment.NewLine;
            expected += "|       StringColumn        | IntColumn | CurrencyColumn |" + Environment.NewLine;
            expected += "+---------------------------+-----------+----------------+" + Environment.NewLine;
            expected += "| Longer than header string | 999       | 19.95          |" + Environment.NewLine;
            expected += "+---------------------------+-----------+----------------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void BasicTableShouldByDefaultSuppressOverlappingColumn()
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);

            // Act
            tableObj.CanvasWidth = 50;
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
        public void BasicTableShouldByDefaultSuppressOverlappingColumnButNotLeaveItAsSuppressed()
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);

            // Act
            tableObj.CanvasWidth = 50;
            var tableString = tableObj.ToString();

            // Assert
            tableObj.Columns[3].Suppressed.ShouldBe(false);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void BasicTableWithoutStretchShouldNotBeStretched(int stylesWithoutStretch)
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns[3].Hide = true; // hide date field 

            // Act
            tableObj.CanvasWidth = 60;
            switch (stylesWithoutStretch)
            {
                case 0: tableObj.TableStretchStyles = TableStretchStyles.DoNothing; break;
                case 1: tableObj.TableStretchStyles = TableStretchStyles.SqueezeAllColumnsEvenly; break;
                case 2:
                    {
                        tableObj.TableStretchStyles = TableStretchStyles.SqueezeLongStrings;
                        tableObj.Columns[0].LongStringBehaviour = LongStringBehaviour.Truncate;
                        tableObj.Columns[0].LongStringBehaviour.Width = 0; // autodetect width
                        break;
                    }
                default: break;
            }
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

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void BasicTableWithoutCanvasWidthShouldIgnoreTableStretchStyles(int tableStretchStyles)
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns[3].Hide = true; // hide date field 

            // Act
            tableObj.CanvasWidth = 0;
            switch (tableStretchStyles)
            {
                case 0: tableObj.TableStretchStyles = TableStretchStyles.DoNothing; break;
                case 1: tableObj.TableStretchStyles = TableStretchStyles.EvenColumnWidth; break;
                case 2: tableObj.TableStretchStyles = TableStretchStyles.StretchOrSqueezeAllColumnsEvenly; break;
                case 3: tableObj.TableStretchStyles = TableStretchStyles.SqueezeAllColumnsEvenly; break;
                case 4: tableObj.TableStretchStyles = TableStretchStyles.StretchOrSqueezeLongStrings; break;
                case 5: tableObj.TableStretchStyles = TableStretchStyles.SqueezeLongStrings; break;
                default: break;
            }
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
        public void BasicTableWithEvenColumnWidthShouldLookLikeThis()
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns[3].Hide = true; // hide date field 

            // Act
            tableObj.CanvasWidth = 40;
            tableObj.TableStretchStyles = TableStretchStyles.EvenColumnWidth;
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+------------+------------+------------+" + Environment.NewLine;
            expected += "| StringColu | IntColumn  | CurrencyCo |" + Environment.NewLine;
            expected += "+------------+------------+------------+" + Environment.NewLine;
            expected += "| AAAA       | 999        | 19.95      |" + Environment.NewLine;
            expected += "+------------+------------+------------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void BasicTableWithEvenStretchShouldLookLikeThis()
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns[3].Hide = true; // hide date field 

            // Act
            tableObj.CanvasWidth = 60;
            tableObj.TableStretchStyles = TableStretchStyles.StretchOrSqueezeAllColumnsEvenly;
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+-------------------+----------------+---------------------+" + Environment.NewLine;
            expected += "| StringColumn      | IntColumn      | CurrencyColumn      |" + Environment.NewLine;
            expected += "+-------------------+----------------+---------------------+" + Environment.NewLine;
            expected += "| AAAA              | 999            | 19.95               |" + Environment.NewLine;
            expected += "+-------------------+----------------+---------------------+";
            tableString.ShouldBe(expected);
        }

        [TestCase(0)]
        [TestCase(1)]
        public void BasicTableWithEvenSqueezeShouldLookLikeThis(int evenSqueezeStretchStyles)
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns[3].Hide = true; // hide date field 

            // Act
            tableObj.CanvasWidth = 40;
            switch (evenSqueezeStretchStyles)
            {
                case 0: tableObj.TableStretchStyles = TableStretchStyles.SqueezeAllColumnsEvenly; break;
                case 1: tableObj.TableStretchStyles = TableStretchStyles.StretchOrSqueezeAllColumnsEvenly; break;
                default: break;
            }
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+------------+---------+---------------+" + Environment.NewLine;
            expected += "| StringColu | IntColu | CurrencyColum |" + Environment.NewLine;
            expected += "+------------+---------+---------------+" + Environment.NewLine;
            expected += "| AAAA       | 999     | 19.95         |" + Environment.NewLine;
            expected += "+------------+---------+---------------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void BasicTableWithLongStringStretchShouldLookLikeThis()
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns[3].Hide = true; // hide date field 

            // Act
            tableObj.CanvasWidth = 60;
            tableObj.TableStretchStyles = TableStretchStyles.StretchOrSqueezeLongStrings;
            tableObj.Columns[0].LongStringBehaviour = LongStringBehaviour.Truncate; // to test long string stretches
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+-----------------------------+-----------+----------------+" + Environment.NewLine;
            expected += "| StringColumn                | IntColumn | CurrencyColumn |" + Environment.NewLine;
            expected += "+-----------------------------+-----------+----------------+" + Environment.NewLine;
            expected += "| AAAA                        | 999       | 19.95          |" + Environment.NewLine;
            expected += "+-----------------------------+-----------+----------------+";
            tableString.ShouldBe(expected);
        }

        [TestCase(0)]
        [TestCase(1)]
        public void BasicTableWithLongStringSqueezeShouldLookLikeThis(int evenSqueezeStretchStyles)
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns[3].Hide = true; // hide date field 

            // Act
            tableObj.CanvasWidth = 40;
            switch (evenSqueezeStretchStyles)
            {
                case 0: tableObj.TableStretchStyles = TableStretchStyles.SqueezeLongStrings; break;
                case 1: tableObj.TableStretchStyles = TableStretchStyles.StretchOrSqueezeLongStrings; break;
                default: break;
            }
            tableObj.Columns[0].LongStringBehaviour = LongStringBehaviour.Truncate; // to test long string stretches
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+---------+-----------+----------------+" + Environment.NewLine;
            expected += "| StringC | IntColumn | CurrencyColumn |" + Environment.NewLine;
            expected += "+---------+-----------+----------------+" + Environment.NewLine;
            expected += "| AAAA    | 999       | 19.95          |" + Environment.NewLine;
            expected += "+---------+-----------+----------------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void BasicTableWithCentralAlignmentShouldLookLikeThis()
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);

            // Act
            tableObj.CanvasWidth = 50;
            tableObj.TableAlignment = Alignment.Center;
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "  +--------------+-----------+----------------+" + Environment.NewLine;
            expected += "  | StringColumn | IntColumn | CurrencyColumn |" + Environment.NewLine;
            expected += "  +--------------+-----------+----------------+" + Environment.NewLine;
            expected += "  | AAAA         | 999       | 19.95          |" + Environment.NewLine;
            expected += "  +--------------+-----------+----------------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void BasicTableWithRightAlignmentShouldLookLikeThis()
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);

            // Act
            tableObj.CanvasWidth = 50;
            tableObj.TableAlignment = Alignment.Right;
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "     +--------------+-----------+----------------+" + Environment.NewLine;
            expected += "     | StringColumn | IntColumn | CurrencyColumn |" + Environment.NewLine;
            expected += "     +--------------+-----------+----------------+" + Environment.NewLine;
            expected += "     | AAAA         | 999       | 19.95          |" + Environment.NewLine;
            expected += "     +--------------+-----------+----------------+";
            tableString.ShouldBe(expected);
        }
    }
}
