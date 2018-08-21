using NUnit.Framework;
using Shouldly;
using ConTabs.TestData;
using System;
using System.Linq;
using System.Collections.Generic;
using ConTabs.Exceptions;

namespace ConTabs.Tests
{
	[TestFixture]
	public class GeneratedColumnTests
	{
		[Test]
		public void ColumnGeneratedUsingInlineFunction()
		{
			// Arrange
			var data = DemoDataProvider.ListOfDemoData();
			var table = Table<Planet>.Create(data);

			// Act
			table.Columns.AddGeneratedColumn<int, double>(
				(diameter) => diameter / 2,
				"Radius",
				table.Columns["Diameter"]);

			// Assert
			table.Columns.Count.ShouldBe(6);
            var radOfSat = (double)table.Columns["Radius"].Values[5];
            radOfSat.ShouldBe(58232);
        }

		[Test]
		public void ColumnGeneratedUsingLocalFunction()
		{
			// Arrange
			var data = DemoDataProvider.ListOfDemoData();
			var table = Table<Planet>.Create(data);

			double CalculateCircumference(int diameter)
			{
				return (diameter * Math.PI / 2);
			}

			// Act
			table.Columns.AddGeneratedColumn<int, double>(
				CalculateCircumference,
				"Circumference",
				table.Columns[2]);

			// Assert
			table.Columns.Count.ShouldBe(6);
            var circOfMerc = Math.Round((double)table.Columns["Circumference"].Values[0],0);
            circOfMerc.ShouldBe(7664);
        }

        [Test]
        public void ColumnGeneratedUsingComplexTypes()
        {
            // Arrange
            var data = new[]
                {
                    new { Start = new DateTime(2017, 01, 01), End = new DateTime(2018, 01, 01) },
                    new { Start = new DateTime(1996, 10, 15), End = DateTime.Now.Date },
                    new { Start = new DateTime(1970, 01, 01), End = new DateTime(2038, 01, 19) },
                };

            var table = Table.Create(data);

            // Act
            table.Columns.AddGeneratedColumn<DateTime, double>(
                (start, end) => (end - start).TotalDays,
                "Total Days",
                table.Columns[0], table.Columns["End"]);

            // Assert
            table.Columns.Count.ShouldBe(3);
            table.Columns["Total Days"].Values[0].ShouldBe(365);
        }

        [Test]
        public void ColumnGeneratedUsingComplexDifferentTypes()
        {
            // Arrange
            var data = new[]
                {
                    new { Start = new DateTime(2017, 01, 01), Duration = 3 },
                    new { Start = new DateTime(1996, 10, 15), Duration = 100 },
                    new { Start = new DateTime(1970, 01, 01), Duration = 50 },
                };

            var table = Table.Create(data);

            // Act
            table.Columns.AddGeneratedColumn<DateTime, int, DateTime>(
                (start, days) => start.AddDays(days),
                "End",
                table.Columns[0], table.Columns["Duration"]);

            // Assert
            table.Columns.Count.ShouldBe(3);
            table.Columns["End"].Values[0].ShouldBe(new DateTime(2017,1,4));
        }

        [Test]
		public void ColumnGeneratedUsingRangeEntireTable()
		{
			// Arrange
			var data = new[]
			{
				new { A = 3, B = 2, C = 6 },
				new { A = 4, B = 3, C = 7 },
				new { A = 5, B = 4, C = 8 },
				new { A = 6, B = 5, C = 9 },
			};

			var table = Table.Create(data);

			// Act
			table.Columns.AddGeneratedColumnFromRange<int>(
				(numbers) => numbers.Select(o=>(int)o).Sum(),
				"Sum",
				table.Columns);

			// Assert
			table.Columns.Count.ShouldBe(4);
            table.Columns["Sum"].Values[0].ShouldBe(11);
		}

		[Test]
		public void ColumnGeneratedUsingRangeSpecificColumns()
		{
			// Arrange
			var data = new[]
			{
				new { A = 3, B = 2, C = 6, D = 9 },
				new { A = 4, B = 3, C = 7, D = 10 },
				new { A = 5, B = 4, C = 8, D = 11 },
				new { A = 6, B = 5, C = 9, D = 12 },
			};

			var table = Table.Create(data);

			int ComputeValues(List<object> numbers)
			{
                var casted = numbers.Select(o => (int)o);

				int sum = 0;

				foreach (int num in numbers)
					sum += (num * 3) / 2;

				return sum;
			}

			// Act
			table.Columns.AddGeneratedColumnFromRange<int>(
				ComputeValues,
				"Sum",
				new List<Column>() { table.Columns[0], table.Columns[1], table.Columns[2]});

			// Assert
			table.Columns.Count.ShouldBe(5);
            table.Columns["Sum"].Values[0].ShouldBe(16);
        }

        [Test]
        public void SingleInputColumn_WrongType_ShouldThrow()
        {
            // Arrange
            var data = DemoDataProvider.ListOfDemoData();
            var table = Table<Planet>.Create(data);

            // Act
            TestDelegate testDelegate = () => table.Columns.AddGeneratedColumn<string, double>(
                (x) => 12d,
                "Twelve",
                table.Columns["Diameter"]);

            // Assert
            Assert.Throws<TypeMismatchException>(testDelegate).Message.ShouldBe("Computed column expected type 'String', but column 'Diameter' is of type 'Int32'.");
        }

        [Test]
        public void TwoInputColumns_FirstIsWrongType_ShouldThrow()
        {
            // Arrange
            var data = new[]
                {
                    new { Start = new DateTime(2017, 01, 01), Duration = 3 },
                    new { Start = new DateTime(1996, 10, 15), Duration = 100 },
                    new { Start = new DateTime(1970, 01, 01), Duration = 50 },
                };

            var table = Table.Create(data);

            // Act
            TestDelegate testDelegate = () => table.Columns.AddGeneratedColumn<string, int, DateTime>(
                (start, days) => DateTime.Parse(start).AddDays(days),
                "End",
                table.Columns[0], table.Columns["Duration"]);

            // Assert
            Assert.Throws<TypeMismatchException>(testDelegate).Message.ShouldBe("Computed column expected type 'String', but column 'Start' is of type 'DateTime'.");
        }

        [Test]
        public void TwoInputColumns_SecondIsWrongType_ShouldThrow()
        {
            // Arrange
            var data = new[]
                {
                    new { Start = new DateTime(2017, 01, 01), Duration = 3 },
                    new { Start = new DateTime(1996, 10, 15), Duration = 100 },
                    new { Start = new DateTime(1970, 01, 01), Duration = 50 },
                };

            var table = Table.Create(data);

            // Act
            TestDelegate testDelegate = () => table.Columns.AddGeneratedColumn<DateTime, string, DateTime>(
                (start, days) => start.AddDays(int.Parse(days)),
                "End",
                table.Columns[0], table.Columns["Duration"]);

            // Assert
            Assert.Throws<TypeMismatchException>(testDelegate).Message.ShouldBe("Computed column expected type 'String', but column 'Duration' is of type 'Int32'.");
        }
    }
}
