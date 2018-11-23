using AutoFixture.NUnit3;
using ConTabs.Attributes;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using System.Linq;

namespace ConTabs.Tests
{
    [TestFixture]
    public class ColumnAttributeTests
    {
        [Test, AutoData]
        public void ConTabsColumnHideAttributeShouldResultInColumnBeingHidden(List<ClassWithOneHiddenColumn> data)
        {
            // act
            var table = Table.Create(data);

            // assert
            table.Columns.Count(c => c.Hide).ShouldBe(1);
            table.Columns.Count(c => !c.Hide).ShouldBe(1);
        }

        [Test, AutoData]
        public void ConTabsColumnPositionAttributeShouldResultInColumnBeingMoved(List<ClassWithReorderedColumns> data)
        {
            // act
            var table = Table.Create(data);

            // assert
            table.Columns[0].ColumnName.ShouldBe("ColumnB");
            table.Columns[1].ColumnName.ShouldBe("ColumnA");
        }

        [Test, AutoData]
        public void ConTabsColumnNameAttributeShouldResultInColumnBeingRenamed(List<ClassWithRenamedColumns> data)
        {
            // act
            var table = Table.Create(data);

            // assert
            table.Columns[1].ColumnName.ShouldBe("ColumnX");
        }
    }


    public class ClassWithOneHiddenColumn
    {
        public int ColumnA { get; set; }

        [ConTabsColumnHide]
        public int ColumnB { get; set; }
    }

    public class ClassWithReorderedColumns
    {
        public int ColumnA { get; set; }

        [ConTabsColumnPosition(0)]
        public int ColumnB { get; set; }
    }

    public class ClassWithRenamedColumns
    {
        public int ColumnA { get; set; }

        [ConTabsColumnName("ColumnX")]
        public int ColumnB { get; set; }
    }
}
