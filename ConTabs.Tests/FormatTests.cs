using ConTabs.TestData;
using NUnit.Framework;
using Shouldly;
using System;
using System.Threading;
using System.Globalization;

namespace ConTabs.Tests
{
    class FormatTests
    {
        [TestCase("da-DK")]
        [TestCase("en-GB")]
        [TestCase("en-US")]
        [TestCase("sk-SK")]
        [TestCase("zh-CN")]
        public void DateTimeFieldCanBeFormattedRegardlessOfCulture(string cultureName)
        {
            // Arrange
            var refDate = new DateTime(2018, 01, 31);
            var culture = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentCulture = culture;

            // Act
            var tableObj = Table<TestDataType>.Create();
            tableObj.Columns[3].FormatString = "d-M-yyyy";
            var val = tableObj.Columns[3].StringValForCol(refDate);

            // Assert
            val.ShouldBe("31-1-2018");
        }

        [TestCase("da-DK")]
        [TestCase("en-GB")]
        [TestCase("en-US")]
        [TestCase("sk-SK")]
        [TestCase("zh-CN")]
        public void DateTimeFieldCanBeFormattedUsingCulture(string cultureName)
        {
            // Arrange
            var refDate = new DateTime(2018, 01, 31);
            var formatString = "dddd";
            var culture = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentCulture = culture;
            var dateString = refDate.ToString(formatString, culture);

            // Act
            var tableObj = Table<TestDataType>.Create();
            tableObj.Columns[3].FormatString = formatString;
            var val = tableObj.Columns[3].StringValForCol(refDate);

            // Assert
            val.ShouldBe(dateString);
        }

        [TestCase("en-GB", "£1.91")]
        [TestCase("sk-SK", "£1,91")]
        public void CurrencyFieldCanBeFormatted(string culture, string expected)
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
            var tableObj = Table<TestDataType>.Create();

            // Act
            tableObj.Columns[2].FormatString = "£0.00";
            var val = tableObj.Columns[2].StringValForCol(1.911M);

            // Assert
            val.ShouldBe(expected);
        }

        [Test]
        public void StringFieldNotAffectedByFormatting()
        {
            // Arrange
            var tableObj = Table<TestDataType>.Create();

            // Act
            tableObj.Columns[0].FormatString = "£0.00";
            var val = tableObj.Columns[0].StringValForCol("A");

            // Assert
            val.ShouldBe("A");
        }

        [Test]
        public void DateTimeFieldCanBeFormattedInTable()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);

            tableObj.Columns[0].Hide = true; // only show date field
            tableObj.Columns[1].Hide = true;
            tableObj.Columns[2].Hide = true;

            // Act
            tableObj.Columns[3].FormatString = "yy-MM-dd";
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+----------------+" + Environment.NewLine;
            expected += "| DateTimeColumn |" + Environment.NewLine;
            expected += "+----------------+" + Environment.NewLine;
            expected += "| 17-01-01       |" + Environment.NewLine;
            expected += "+----------------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void DateTimeFieldCanBeFormattedInTableWithExplicitPadding()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData(1);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Padding = new Padding(2);

            tableObj.Columns[0].Hide = true; // only show date field
            tableObj.Columns[1].Hide = true;
            tableObj.Columns[2].Hide = true;

            // Act
            tableObj.Columns[3].FormatString = "yy-MM-dd";
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+------------------+" + Environment.NewLine;
            expected += "|  DateTimeColumn  |" + Environment.NewLine;
            expected += "+------------------+" + Environment.NewLine;
            expected += "|  17-01-01        |" + Environment.NewLine;
            expected += "+------------------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void CurrencyFieldCanBeFormattedInTable()
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            var listOfTestClasses = DataProvider.ListOfTestData(2);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);

            tableObj.Columns[0].Hide = true; // only show currency field
            tableObj.Columns[1].Hide = true;
            tableObj.Columns[3].Hide = true;

            // Act
            tableObj.Columns[2].FormatString = "£0.00";
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+----------------+" + Environment.NewLine;
            expected += "| CurrencyColumn |" + Environment.NewLine;
            expected += "+----------------+" + Environment.NewLine;
            expected += "| £19.95         |" + Environment.NewLine;
            expected += "| -£2000.00      |" + Environment.NewLine;
            expected += "+----------------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void CurrencyFieldCanBeFormattedInTableWithExplicitPadding()
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            var listOfTestClasses = DataProvider.ListOfTestData(2);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);
            tableObj.Padding = new Padding(0);

            tableObj.Columns[0].Hide = true; // only show currency field
            tableObj.Columns[1].Hide = true;
            tableObj.Columns[3].Hide = true;

            // Act
            tableObj.Columns[2].FormatString = "£0.00";
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+--------------+" + Environment.NewLine;
            expected += "|CurrencyColumn|" + Environment.NewLine;
            expected += "+--------------+" + Environment.NewLine;
            expected += "|£19.95        |" + Environment.NewLine;
            expected += "|-£2000.00     |" + Environment.NewLine;
            expected += "+--------------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void StringFieldNotFormattedInTable()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfTestData(2);
            var tableObj = Table<TestDataType>.Create(listOfTestClasses);

            tableObj.Columns[1].Hide = true; // only show string field
            tableObj.Columns[2].Hide = true;
            tableObj.Columns[3].Hide = true;

            // Act
            tableObj.Columns[0].FormatString = "£0.00";
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+--------------+" + Environment.NewLine;
            expected += "| StringColumn |" + Environment.NewLine;
            expected += "+--------------+" + Environment.NewLine;
            expected += "| AAAA         |" + Environment.NewLine;
            expected += "| BB           |" + Environment.NewLine;
            expected += "+--------------+";
            tableString.ShouldBe(expected);
        }

        [Test]
        public void PropWithCustomToStringNotFormattedInTable()
        {
            // Arrange
            var listOfTestClasses = DataProvider.ListOfExtendedTestData(2);
            var tableObj = Table<ExtendedTestDataType>.Create(listOfTestClasses);

            tableObj.Columns[1].Hide = true; // only show custom ToString type field
            tableObj.Columns[2].Hide = true;
            tableObj.Columns[3].Hide = true;
            tableObj.Columns[4].Hide = true;

            // Act
            tableObj.Columns[0].FormatString = "£0.00";
            var tableString = tableObj.ToString();

            // Assert
            string expected = "";
            expected += "+----------------+" + Environment.NewLine;
            expected += "| CustomToString |" + Environment.NewLine;
            expected += "+----------------+" + Environment.NewLine;
            expected += "| A              |" + Environment.NewLine;
            expected += "| B              |" + Environment.NewLine;
            expected += "+----------------+";
            tableString.ShouldBe(expected);
        }
    }
}
