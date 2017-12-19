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

            Console.WriteLine("Press return to continue...");
            Console.ReadLine();

            var animalData = DemoDataProvider.ListOfDemoAnimalsData();
            var table2 = TableBuilder<DemoAnimals>
                .Initialize(animalData)
                .Build();

            Console.WriteLine(table2);
            
            Console.WriteLine("Press return to continue...");
            Console.ReadLine();
            
            var table3 = TableBuilder<DemoAnimals>
                .Initialize(animalData)
                .HideColumn("Name")
                .Build();

            Console.WriteLine(table3);
            
            Console.WriteLine("Press return to continue...");
            Console.ReadLine();
            
            var table4 = TableBuilder<DemoAnimals>
                .Initialize(animalData)
                .HideColumn("Name")
                .HideColumn("Color")
                .Build();

            Console.WriteLine(table4);
            
            Console.WriteLine("Press return to continue...");
            Console.ReadLine();
        }
    }
}