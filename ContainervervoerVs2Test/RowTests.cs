using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ContainervervoerVs2.Tests
{
    [TestClass]
    public class RowTests
    {
        [TestMethod]
        public void Row_Initialization_ShouldCreateCorrectNumberOfStacks()
        {
            // Arrange
            int length = 5;

            // Act
            Row row = new Row(length);

            // Assert
            Assert.AreEqual(length, row.Stacks.Count);
        }

        [TestMethod]
        public void Row_Initialization_FirstStackShouldBeCooled()
        {
            // Arrange
            int length = 5;

            // Act
            Row row = new Row(length);

            // Assert
            Assert.IsTrue(row.Stacks[0].IsCooled);
        }

        [TestMethod]
        public void Row_Initialization_OtherStacksShouldNotBeCooled()
        {
            // Arrange
            int length = 5;

            // Act
            Row row = new Row(length);

            // Assert
            for (int i = 1; i < length; i++)
            {
                Assert.IsFalse(row.Stacks[i].IsCooled);
            }
        }

        [TestMethod]
        public void GetTotalWeight_ShouldReturnCorrectTotalWeight()
        {
            // Arrange
            Row row = new Row(5);
            Container container1 = new Container(10, Container.Type.Normal);
            Container container2 = new Container(20, Container.Type.Valuable);
            row.Stacks[0].TryToAddContainer(container1);
            row.Stacks[1].TryToAddContainer(container2);

            // Act
            int totalWeight = row.GetTotalWeight();

            // Assert
            Assert.AreEqual(30, totalWeight);
        }

        [TestMethod]
        public void TryToAddContainer_ShouldAddContainerToCorrectStack()
        {
            // Arrange
            Row row = new Row(5);
            Container container = new Container(10, Container.Type.Normal);

            // Act
            bool result = row.TryToAddContainer(container);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(10, row.GetTotalWeight());
        }

        [TestMethod]
        public void IsStackAccessible_ShouldReturnTrueForFirstAndLastStack()
        {
            // Arrange
            Row row = new Row(5);

            // Act & Assert
            Assert.IsTrue(row.IsStackAccessible(0));
            Assert.IsTrue(row.IsStackAccessible(4));
        }

        [TestMethod]
        public void IsStackAccessible_ShouldReturnTrueForEmptyStack()
        {
            // Arrange
            Row row = new Row(5);

            // Act & Assert
            Assert.IsTrue(row.IsStackAccessible(2));
        }

        [TestMethod]
        public void IsStackAccessible_ShouldReturnTrueForNonValuableStack()
        {
            // Arrange
            Row row = new Row(5);
            Container container = new Container(10, Container.Type.Normal);
            row.Stacks[2].TryToAddContainer(container);

            // Act & Assert
            Assert.IsTrue(row.IsStackAccessible(2));
        }

        [TestMethod]
        public void IsStackAccessible_ShouldReturnFalseForValuableStackWithHigherNeighbors()
        {
            // Arrange
            Row row = new Row(5);
            Container container1 = new Container(10, Container.Type.Valuable);
            Container container2 = new Container(20, Container.Type.Normal);
            row.Stacks[1].TryToAddContainer(container2);
            row.Stacks[2].TryToAddContainer(container1);
            row.Stacks[3].TryToAddContainer(container2);

            // Act & Assert
            Assert.IsFalse(row.IsStackAccessible(2));
        }

        [TestMethod]
        public void AreFrontAndBackStacksAccessible_ShouldReturnTrueWhenBothAccessible()
        {
            // Arrange
            Row row = new Row(5);
            Container container = new Container(10, Container.Type.Normal);
            row.Stacks[1].TryToAddContainer(container);
            row.Stacks[3].TryToAddContainer(container);

            // Act & Assert
            Assert.IsTrue(row.AreFrontAndBackStacksAccessible(2));
        }


    }
}

