using ConTabs.Attributes;
using System.Collections.Generic;

namespace ConTabsDemo_Attributes
{
    public static class DemoDataProvider
    {
        public static List<Planet> ListOfDemoData()
        {
            return new List<Planet>
            {
                new Planet{Name="Mercury", DistanceFromSun=57909227, OrbitalPeriod=88F, Diameter=4879, Fact="Mercury is the smallest planet."},
                new Planet{Name="Venus", DistanceFromSun=108209475, OrbitalPeriod=225F, Diameter=12104, Fact="Venus is the hottest planet."},
                new Planet{Name="Earth", DistanceFromSun=149598262, OrbitalPeriod=365.24F, Diameter=12742, Fact="Earth is where we live"},
                new Planet{Name="Mars", DistanceFromSun=227943824, OrbitalPeriod=693.5F, Diameter=6779, Fact="Mars is also often described as the “Red Planet” due to its reddish appearance."},
                new Planet{Name="Jupiter", DistanceFromSun=778340821, OrbitalPeriod=4343.5F, Diameter=139822, Fact="Jupiter is the largest planet."},
                new Planet{Name="Saturn", DistanceFromSun=1426666422, OrbitalPeriod=10767.5F, Diameter=116464, Fact="Saturn is best known for its fabulous ring system that was first observed in 1610 by the astronomer Galileo Galilei."},
                new Planet{Name="Uranus", DistanceFromSun=2870658186, OrbitalPeriod=30660F, Diameter=50724, Fact="Uranus became the first planet discovered with the use of a telescope."},
                new Planet{Name="Neptune", DistanceFromSun=4498396441, OrbitalPeriod=60152F, Diameter=49244, Fact="On average Neptune is the coldest planet"}
            };
        }
    }

    public class Planet
    {
        public string Name { get; set; }

        [ConTabsColumnPosition(3)]
        public long DistanceFromSun { get; set; }

        public int Diameter { get; set; }

        [ConTabsColumnName("Year length")]
        public float OrbitalPeriod { get; set; }

        [ConTabsColumnHide()]
        public string Fact { get; set; }
    }
}
