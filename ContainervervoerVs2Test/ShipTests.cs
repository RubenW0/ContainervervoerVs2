using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ContainervervoerVs2.Tests
{
    [TestClass]
    public class ShipTests
    {
        [TestMethod]
        public void Ship_Initialization_ShouldSetProperties()
        {
            // Arrange
            int length = 10;
            int width = 5;

            // Act
            Ship ship = new Ship(length, width);

            // Assert
            Assert.AreEqual(length, ship.Length);
            Assert.AreEqual(width, ship.Width);
            Assert.AreEqual(width, ship.Rows.Count);
        }

        [TestMethod]
        public void Ship_Initialization_ShouldThrowExceptionForInvalidLength()
        {
            // Arrange
            int length = 0;
            int width = 5;

            // Act & Assert
            var ex = Assert.ThrowsException<ArgumentException>(() => new Ship(length, width));
            Assert.AreEqual("Length must be greater than 0", ex.Message);
        }

        [TestMethod]
        public void Ship_Initialization_ShouldThrowExceptionForInvalidWidth()
        {
            // Arrange
            int length = 10;
            int width = 0;

            // Act & Assert
            var ex = Assert.ThrowsException<ArgumentException>(() => new Ship(length, width));
            Assert.AreEqual("Width must be greater than 0", ex.Message);
        }

        [TestMethod]
        public void GetTotalWeight_ShouldReturnCorrectTotalWeight()
        {
            // Arrange
            Ship ship = new Ship(10, 5);
            Container container1 = new Container(10, Container.Type.Normal);
            Container container2 = new Container(20, Container.Type.Valuable);
            ship.TryToAddContainer(container1);
            ship.TryToAddContainer(container2);

            // Act
            int totalWeight = ship.GetTotalWeight();

            // Assert
            Assert.AreEqual(30, totalWeight);
        }

        [TestMethod]
        public void TryToAddContainer_ShouldAddContainerToCorrectRow()
        {
            // Arrange
            Ship ship = new Ship(10, 5);
            Container container = new Container(10, Container.Type.Normal);

            // Act
            bool result = ship.TryToAddContainer(container);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(10, ship.GetTotalWeight());
        }

        [TestMethod]
        public void IsBalanced_ShouldReturnTrueWhenBalanced()
        {
            // Arrange
            Ship ship = new Ship(10, 4);
            ship.TryToAddContainer(new Container(10, Container.Type.Normal));
            ship.TryToAddContainer(new Container(10, Container.Type.Normal));
            ship.TryToAddContainer(new Container(10, Container.Type.Normal));
            ship.TryToAddContainer(new Container(10, Container.Type.Normal));

            // Act
            bool result = ship.IsBalanced();

            // Assert
            Assert.IsTrue(result);
        }

    }
}

