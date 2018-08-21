using NUnit.Framework;
using Shouldly;
using ConTabs.TestData;
using System;
using System.Linq;
using System.Collections.Generic;

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
            table.Columns[2].Values[0].ShouldBe(365);
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
	}
}
