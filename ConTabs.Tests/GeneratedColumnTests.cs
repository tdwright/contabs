using NUnit.Framework;
using Shouldly;
using ConTabs.TestData;
using System;

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
		}

		[Test]
		public void ColumnGeneratedUsingLocalFunction()
		{
			// Arrange
			var data = DemoDataProvider.ListOfDemoData();
			var table = Table<Planet>.Create(data);

			double CalculateCircumference(int diameter)
			{
				return (diameter * 3.14159 / 2);
			}

			// Act
			table.Columns.AddGeneratedColumn<int, double>(
				CalculateCircumference,
				"Circumference",
				table.Columns[2]);

			// Assert
			table.Columns.Count.ShouldBe(6);
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
		}
	}
}
