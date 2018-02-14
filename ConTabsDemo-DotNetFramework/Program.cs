using System;
using ConTabs;
using ConTabs.TestData;

namespace ConTabsDemo_DotNetFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CONTABS .NET FRAMEWORK DEMO");

            var Data = DemoDataProvider.ListOfDemoData();

            var table = Table<DemoDataType>.Create(Data);
            table.Padding = 3;
            table.HeaderAlignment = Alignment.Center;
            table.ColumnAlignment = Alignment.Right;
            Console.WriteLine(table.ToString());

            Console.WriteLine("Press return to exit...");
            Console.ReadLine();
        }
    }
}
