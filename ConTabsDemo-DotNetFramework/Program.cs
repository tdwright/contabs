using System;
using System.Collections.Generic;
using ConTabs;

namespace ConTabsDemo_DotNetFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CONTABS .NET FRAMEWORK DEMO");

            var Data = new List<TestClass>
            {
                new TestClass{StringCol="Cats", IntCol = 2},
                new TestClass{StringCol="Dogs", IntCol = 1},
                new TestClass{StringCol="Chickens", IntCol = 3}
            };

            var table = Table<TestClass>.Create(Data);
            Console.WriteLine(table.ToString());

            Console.WriteLine("Press return to exit...");
            Console.ReadLine();
        }

        private class TestClass
        {
            public string StringCol { get; set; }
            public int IntCol { get; set; }
        }
    }
}
