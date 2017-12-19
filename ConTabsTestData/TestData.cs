using System;
using System.Collections.Generic;
using System.Linq;

namespace ConTabs.TestData
{
    public static class DataProvider
    {
        public static List<TestDataType> ListOfTestData(int? limit = null)
        {
            var list = new List<TestDataType>
            {
                new TestDataType{StringColumn="AAAA", IntColumn=999, CurrencyColumn=19.95M, DateTimeColumn=new DateTime(2017,01,01)},
                new TestDataType{StringColumn="BB", IntColumn=1234567899, CurrencyColumn=-2000M, DateTimeColumn=new DateTime(2017,01,13)},
                new TestDataType{StringColumn="CCCCCCC", IntColumn=-12, CurrencyColumn=19.95M, DateTimeColumn=new DateTime(2017,02,20)}
            };
            if (!limit.HasValue || limit < 0) limit = list.Count;
            return list.Take(limit.Value).ToList();
        }

        public static List<MinimalDataType> ListOfMinimalData(int? limit = null)
        {
            var list = new List<MinimalDataType>
            {
                new MinimalDataType{IntA = 1, IntB = 3},
                new MinimalDataType{IntA = 2, IntB = 9},
                new MinimalDataType{IntA = 3, IntB = 27},
                new MinimalDataType{IntA = 4, IntB = 81},
                new MinimalDataType{IntA = 4, IntB = 243}
            };
            if (!limit.HasValue || limit < 0) limit = list.Count;
            return list.Take(limit.Value).ToList();
        }
        
        public static List<InvalidTestDataType> ListOfInvalidTestData()
        {
            var list = new List<InvalidTestDataType>
            {
                new InvalidTestDataType(),
                new InvalidTestDataType(),
                new InvalidTestDataType(),
                new InvalidTestDataType(),
            };
            return list;
        }
    }
    
    

    public class TestDataType
    {
        public string StringColumn { get; set; }
        public int IntColumn { get; set; }
        public decimal CurrencyColumn { get; set; }
        public DateTime DateTimeColumn { get; set; }
        private string HiddenProp { get; set; }
    }

    public class MinimalDataType
    {
        public int IntA { get; set; }
        public int IntB { get; set; }
    }
    
    public class InvalidTestDataType
    {
        private string StringColumn { get; set; }
        private int IntColumn { get; set; }
        private decimal CurrencyColumn { get; set; }
        private DateTime DateTimeColumn { get; set; }
        private string HiddenProp { get; set; }
    }
}
