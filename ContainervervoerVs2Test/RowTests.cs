using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using ContainervervoerVs2;

namespace ContainervervoerVs2.Tests
{
    [TestClass]
    public class RowTests
    {
        [TestMethod]
        public void Row_Initialization_ShouldCreateStacks()
        {
            // Arrange
            int length = 6;

            // Act
            Row row = new Row(length);

            // Assert
            Assert.AreEqual(length, row.Stacks.Count);
        }

        [TestMethod]
        public void CalculateTotalWeight_ShouldReturnCorrectTotalWeight()
        {
            // Arrange
            Row row = new Row(6);
            row.Stacks[0].TryToAddContainer(new Container(10, false, false));
            row.Stacks[1].TryToAddContainer(new Container(15, false, false));

            // Act
            int totalWeight = row.CalculateTotalWeight();

            // Assert
            Assert.AreEqual(25, totalWeight);
        }

        [TestMethod]
        public void TryToAddContainer_ShouldAddContainerToRow()
        {
            // Arrange
            Row row = new Row(6);
            Container container = new Container(10, false, false);

            // Act
            bool result = row.TryToAddContainer(container);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsStackReachable_ShouldReturnTrueForReachableStack()
        {
            // Arrange
            Row row = new Row(6);

            // Act
            bool result = row.IsStackReachable(0);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsPreviousAndNextReachable_ShouldReturnTrueForReachableStacks()
        {
            // Arrange
            Row row = new Row(6);

            // Act
            bool result = row.IsPreviousAndNextReachable(1);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
