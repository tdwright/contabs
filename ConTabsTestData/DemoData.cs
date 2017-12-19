using System.Collections.Generic;

namespace ConTabs.TestData
{
    public static class DemoDataProvider
    {
        public static List<DemoDataType> ListOfDemoData()
        {
            return new List<DemoDataType>
            {
                new DemoDataType{StringCol="Cats", IntCol = 2},
                new DemoDataType{StringCol="Dogs", IntCol = 1},
                new DemoDataType{StringCol="Chickens", IntCol = 3}
            };
        }
        
        public static List<DemoAnimals> ListOfDemoAnimalsData()
        {
            return new List<DemoAnimals>
            {
                new DemoAnimals{Species="Cats", Color = "Gray", Name = "Joe", IntCol = 2},
                new DemoAnimals{Species="Dogs", Color = "Red", Name = "Billy", IntCol = 1},
                new DemoAnimals{Species="Chickens", Color = "Green", Name = "Jill", IntCol = 3}
            };
        }
    }


    public class DemoDataType
    {
        public string StringCol { get; set; }
        public int IntCol { get; set; }
    }
    public class DemoAnimals
    {
        public string Species { get; set; }
        public string Color { get; set; }
        public string Name { get; set; }
        public int IntCol { get; set; }
    }
}
