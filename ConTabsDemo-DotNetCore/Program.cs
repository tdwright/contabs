using System;
using ConTabs;
using System.Collections.Generic;
using ConTabs.TestData;

namespace ConTabsDemo_DotNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CONTABS .NET CORE DEMO");

            var Data = DemoDataProvider.ListOfDemoData();

            var table = Table<DemoDataType>.Create(Data);
            Console.WriteLine(table.ToString());

            Console.WriteLine("Press return to exit...");
            Console.ReadLine();
        }
    }
}