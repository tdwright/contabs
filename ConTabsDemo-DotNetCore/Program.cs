using System;
using ConTabs;
using System.Collections.Generic;

namespace ConTabsDemo_DotNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CONTABS .NET CORE DEMO");

            var Data = new List<TestClass>
            {
                new TestClass{StringCol="Tom", IntCol = 29},
                new TestClass{StringCol="Hannah", IntCol = 34}
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