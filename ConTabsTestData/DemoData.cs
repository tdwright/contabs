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
    }


    public class DemoDataType
    {
        public string StringCol { get; set; }
        public int IntCol { get; set; }
    }
}
