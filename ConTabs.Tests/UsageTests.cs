using ConTabs.TestData;
using NUnit.Framework;
using Shouldly;
using System;

namespace ConTabs.Tests
{
    [TestFixture]
    class UsageTests
    {
        [Test]
        public void OnceFormatSet_FormatCanBeUpdated()
        {
            // Arrange
            var tableObj = Table<TestDataType>.Create();
            tableObj.TableStyle = Style.Default;

            // Act
            tableObj.TableStyle = Style.Heavy;
            var result = tableObj.ToString();

            // Assert
            result[0].ShouldBe('#');
        }

        [Test]
        public void OnceFormatSet_CornerCanBeUpdated()
        {
            // Arrange
            var tableObj = Table<TestDataType>.Create();
            tableObj.TableStyle = Style.Default;

            // Act
            tableObj.TableStyle.Corners.CornerTopLeft = '$';
            var result = tableObj.ToString();

            // Assert
            result[0].ShouldBe('$');
        }

    }
}
