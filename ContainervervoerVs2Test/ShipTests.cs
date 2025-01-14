using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContainervervoerVs2;
using System.Collections.Generic;

namespace ContainervervoerVs2.Tests
{
    [TestClass]
    public class ShipTests
    {

        [TestMethod]
        public void IsProperlyLoaded_ShouldReturnFalse_WhenShipIsNotProperlyLoaded()
        {
            // Arrange
            var ship = new Ship(10, 2);

            // Voeg containers en rijen toe zodat het schip niet goed geladen is

            // Act
            bool isProperlyLoaded = ship.IsProperlyLoaded();

            // Assert
            Assert.IsFalse(isProperlyLoaded);
        }

        // Test voor IsBalanced
        [TestMethod]
        public void IsBalanced_ShouldReturnTrue_WhenShipIsBalanced()
        {
            // Arrange
            var ship = new Ship(10, 2);

            // Voeg containers toe zodat het schip gebalanceerd is

            // Act
            bool isBalanced = ship.IsBalanced();

            // Assert
            Assert.IsTrue(isBalanced);
        }
    }
}
