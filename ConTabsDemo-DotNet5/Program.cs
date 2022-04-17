using System;
using ConTabs;
using ConTabs.TestData;
using System.Text;

namespace ConTabsDemo_DotNetFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CONTABS .NET 5 DEMO");

            // Get some data (can be an IEnumerable of anything)
            var Data = DemoDataProvider.ListOfDemoData();

            // Create a table object
            var table = Table.Create(Data);

            /*
             * 
             *   Everything that follows is optional.
             *   You could just skip to a Console.Writeline here.
             *   
             */

            // Set the style (and enable Unicode for the console)
            table.TableStyle = Style.UnicodeArcs;
            Console.OutputEncoding = Encoding.Unicode;

            // Hide the diameter column
            table.Columns["Diameter"].Hide = true;

            // Add a computed column for the radius
            table.Columns.AddGeneratedColumn<int, int>(d => d / 2, "Radius", table.Columns["Diameter"]);

            // Move the orbital period column next to the name
            table.Columns.MoveColumn("OrbitalPeriod", 1);

            // Rename the orbital period column
            table.Columns["OrbitalPeriod"].ColumnName = "Year length";

            // Add a format string to orbital period
            table.Columns["Year length"].FormatString = "# days";

            // Right-align the distance from sun column (and format it nicely)
            table.Columns["DistanceFromSun"].Alignment = Alignment.Right;
            table.Columns["DistanceFromSun"].FormatString = "###,###,###0 km";

            // Handle the length of the fact
            table.Columns["Fact"].LongStringBehaviour = LongStringBehaviour.Wrap;
            table.Columns["Fact"].LongStringBehaviour.Width = 25;

            // Add some padding
            table.Padding = new Padding(1, 1);

            // Stretch long string columns to fit console
            table.CanvasWidth = Console.WindowWidth - 1;
            table.TableStretchStyles = TableStretchStyles.StretchOrSqueezeLongStrings;

            // Center the outputted table
            table.TableAlignment = Alignment.Center;

            // Finally, spit out the finished table
            Console.WriteLine(table);

            Console.WriteLine("Press return to exit...");
            Console.ReadLine();
        }
    }
}