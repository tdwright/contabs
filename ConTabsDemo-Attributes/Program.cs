using ConTabs;
using System;

namespace ConTabsDemo_Attributes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CONTABS ATTRIBUTE DEMO");

            // Get some data (can be an IEnumerable of anything)
            var Data = DemoDataProvider.ListOfDemoData();

            // Write it to the console (in one line!)
            Console.WriteLine(Table.Create(Data));

            Console.WriteLine("Press return to exit...");
            Console.ReadLine();
        }
    }
}
