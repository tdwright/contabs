using NUnit.Framework;
using Shouldly;

namespace ConTabs.Tests
{
    [TestFixture]
    public class ConTab_Table_AnonymousTypes
    {
        [Test]
        public void TableCreatedFromBasicAnonymousType()
        {
            // Arrange
            var data = new[]
                {
                    new { A = 1, B = "ABC", C = 12.5 },
                    new { A = 2, B = "DEF", C = 42.0 },
                    new { A = 3, B = "XYZ", C = 0.0 },
                };

            // Act
            var table = Table.Create(data);

            // Assert
            table.ToString().Length.ShouldBeGreaterThan(0);
            table.Columns.Count.ShouldBe(3);
        }

        [Test]
        public void TableCreatedFromAnonymousTypeColumnsMatch()
        {
            // Arrange
            var data = new[]
            {
                new { Name = "Mercury", DistanceFromSun = 57909227L, OrbitalPeriod = 88F, Diameter = 4879, Fact = "Mercury is the smallest planet." },
                new { Name = "Venus", DistanceFromSun = 108209475L, OrbitalPeriod = 225F, Diameter = 12104, Fact = "Venus is the hottest planet." },
                new { Name = "Earth", DistanceFromSun = 149598262L, OrbitalPeriod = 365.24F, Diameter = 12742, Fact = "Earth is where we live" },
                new { Name = "Mars", DistanceFromSun = 227943824L, OrbitalPeriod = 693.5F, Diameter = 6779, Fact = "Mars is also often described as the “Red Planet” due to its reddish appearance." },
                new { Name = "Jupiter", DistanceFromSun = 778340821L, OrbitalPeriod = 4343.5F, Diameter = 139822, Fact = "Jupiter is the largest planet." },
                new { Name = "Saturn", DistanceFromSun = 1426666422L, OrbitalPeriod = 10767.5F, Diameter = 116464, Fact = "Saturn is best known for its fabulous ring system that was first observed in 1610 by the astronomer Galileo Galilei." },
                new { Name = "Uranus", DistanceFromSun = 2870658186L, OrbitalPeriod = 30660F, Diameter = 50724, Fact = "Uranus became the first planet discovered with the use of a telescope." },
                new { Name = "Neptune", DistanceFromSun = 4498396441L, OrbitalPeriod = 60152F, Diameter = 49244, Fact = "On average Neptune is the coldest planet" },
            };

            // Act
            var table = Table.Create(data);

            // Assert
            table.Columns["Name"].ShouldNotBeNull();
            table.Columns["DistanceFromSun"].ShouldNotBeNull();
            table.Columns["OrbitalPeriod"].ShouldNotBeNull();
            table.Columns["Diameter"].ShouldNotBeNull();
            table.Columns["Fact"].ShouldNotBeNull();

            table.Columns["Name"].SourceType.ShouldBe(typeof(string));
            table.Columns["DistanceFromSun"].SourceType.ShouldBe(typeof(long));
            table.Columns["OrbitalPeriod"].SourceType.ShouldBe(typeof(float));
            table.Columns["Diameter"].SourceType.ShouldBe(typeof(int));
            table.Columns["Fact"].SourceType.ShouldBe(typeof(string));

            table.Columns["Name"].Values.Count.ShouldBe(data.Length);
            table.Columns["DistanceFromSun"].Values.Count.ShouldBe(data.Length);
            table.Columns["OrbitalPeriod"].Values.Count.ShouldBe(data.Length);
            table.Columns["Diameter"].Values.Count.ShouldBe(data.Length);
            table.Columns["Fact"].Values.Count.ShouldBe(data.Length);
        }


        [Test]
        public void AsTableMatchesTableCreate()
        {
            // Arrange
            var data = new[]
                {
                    new { A = 1, B = "ABC", C = 12.5 },
                    new { A = 2, B = "DEF", C = 42.0 },
                    new { A = 3, B = "XYZ", C = 0.0 },
                };

            // Act
            var tableA = Table.Create(data);
            var tableB = data.AsTable();

            // Assert
            tableA.ToString().Length.ShouldBeGreaterThan(0);
            tableA.Columns.Count.ShouldBe(3);
            tableB.ToString().Length.ShouldBeGreaterThan(0);
            tableB.Columns.Count.ShouldBe(3);

            tableA.ToString().ShouldBe(tableB.ToString());
        }
    }
}
