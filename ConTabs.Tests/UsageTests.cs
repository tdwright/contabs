using ConTabs.TestData;
using NUnit.Framework;
using Shouldly;
using System;

namespace ConTabs.Tests
{
    [TestFixture]
    class UsageTests
    {
        [Test]
        public void OnceFormatSet_FormatCanBeUpdated()
        {
            // Arrange
            var tableObj = Table<TestDataType>.Create();
            tableObj.TableStyle = Style.Default;

            // Act
            tableObj.TableStyle = Style.Heavy;
            var result = tableObj.ToString();

            // Assert
            result[0].ShouldBe('#');
        }

        [Test]
        public void CustomStyle_OnceSet_AffectsAllElements()
        {
            // Arrange
            var tableObj = Table<TestDataType>.Create();

            // Act
            tableObj.TableStyle = new Style('W','F','C');

            // Assert
            tableObj.TableStyle.Corners.CornerBottomLeft.ShouldBe('C');
            tableObj.TableStyle.Corners.CornerBottomRight.ShouldBe('C');
            tableObj.TableStyle.Corners.CornerTopLeft.ShouldBe('C');
            tableObj.TableStyle.Corners.CornerTopRight.ShouldBe('C');
            tableObj.TableStyle.Corners.Intersection.ShouldBe('C');
            tableObj.TableStyle.Corners.TeeNoDown.ShouldBe('C');
            tableObj.TableStyle.Corners.TeeNoLeft.ShouldBe('C');
            tableObj.TableStyle.Corners.TeeNoRight.ShouldBe('C');
            tableObj.TableStyle.Corners.TeeNoUp.ShouldBe('C');
        }

        [Test]
        public void OnceFormatSet_CornerCanBeUpdated()
        {
            // Arrange
            var tableObj = Table<TestDataType>.Create();
            tableObj.TableStyle = Style.Default;

            // Act
            tableObj.TableStyle.Corners.CornerTopLeft = '$';
            var result = tableObj.ToString();

            // Assert
            result[0].ShouldBe('$');
        }

        [Test]
        public void AddressingColByPropName_ReturnsCorrectCol()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);

            // Act
            tableObj.Columns["IntColumn"].Hide = true;
            tableObj.Columns["CurrencyColumn"].Hide = true;
            tableObj.Columns["DateTimeColumn"].Hide = true;

            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+--------------+" + Environment.NewLine;
            expected += "| StringColumn |" + Environment.NewLine;
            expected += "+--------------+" + Environment.NewLine;
            expected += "| AAAA         |" + Environment.NewLine;
            expected += "+--------------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void RenamingCol_NewNameIsShownInHeader()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns["IntColumn"].Hide = true;
            tableObj.Columns["CurrencyColumn"].Hide = true;
            tableObj.Columns["DateTimeColumn"].Hide = true;

            // Act
            tableObj.Columns["StringColumn"].ColumnName = "NewName";

            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+---------+" + Environment.NewLine;
            expected += "| NewName |" + Environment.NewLine;
            expected += "+---------+" + Environment.NewLine;
            expected += "| AAAA    |" + Environment.NewLine;
            expected += "+---------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void AddressingColByColName_ReturnsCorrectCol()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns["CurrencyColumn"].Hide = true;
            tableObj.Columns["DateTimeColumn"].Hide = true;

            // Act
            tableObj.Columns["IntColumn"].ColumnName = "NewName";
            tableObj.Columns["NewName"].Hide = true;

            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+--------------+" + Environment.NewLine;
            expected += "| StringColumn |" + Environment.NewLine;
            expected += "+--------------+" + Environment.NewLine;
            expected += "| AAAA         |" + Environment.NewLine;
            expected += "+--------------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void ReorderingColByColIndex_ColPrintedInNewOrder()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns["CurrencyColumn"].Hide = true;
            tableObj.Columns["DateTimeColumn"].Hide = true;

            // Act
            tableObj.Columns.MoveColumn(1, 0);

            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+-----------+--------------+" + Environment.NewLine;
            expected += "| IntColumn | StringColumn |" + Environment.NewLine;
            expected += "+-----------+--------------+" + Environment.NewLine;
            expected += "| 999       | AAAA         |" + Environment.NewLine;
            expected += "+-----------+--------------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void ReorderingColByColName_ColPrintedInNewOrder()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Columns["CurrencyColumn"].Hide = true;
            tableObj.Columns["DateTimeColumn"].Hide = true;

            // Act
            tableObj.Columns.MoveColumn("IntColumn", 0);

            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+-----------+--------------+" + Environment.NewLine;
            expected += "| IntColumn | StringColumn |" + Environment.NewLine;
            expected += "+-----------+--------------+" + Environment.NewLine;
            expected += "| 999       | AAAA         |" + Environment.NewLine;
            expected += "+-----------+--------------+";
            tableString.ShouldBe(expected);
        }

    }
}
