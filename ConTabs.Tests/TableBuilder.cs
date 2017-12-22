using System;
using ConTabs.Exceptions;
using ConTabs.TestData;
using NUnit.Framework;

namespace ConTabs.Tests
{
    [TestFixture]
    public class TableBuilder
    {
        [Test]
        public void HideColumn_GivenInvalidColumnName_ThrowsColumnNotFoundException()
        {
            var animalData = DemoDataProvider.ListOfDemoAnimalsData();
            var tableBuilder = TableBuilder<DemoAnimals>
                .Initialize(animalData);

            Assert.Throws<ColumnNotFoundException>(() => tableBuilder.HideColumn("BadColumName"));
        }
    }
}