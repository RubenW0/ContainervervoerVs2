using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ContainervervoerVs2.Tests
{
    [TestClass]
    public class ContainerTests
    {
        [TestMethod]
        public void Container_Initialization_ShouldSetProperties()
        {
            // Arrange
            int weight = 10;
            Container.Type containerType = Container.Type.Valuable;

            // Act
            Container container = new Container(weight, containerType);

            // Assert
            Assert.AreEqual(weight, container.Weight);
            Assert.AreEqual(containerType, container.ContainerType);
            Assert.IsTrue(container.IsValuable);
            Assert.IsFalse(container.NeedsCooling);
        }

        [TestMethod]
        public void Container_Initialization_ShouldThrowExceptionForInvalidWeightBelowMinimum()
        {
            // Arrange
            int weight = 3;
            Container.Type containerType = Container.Type.Normal;

            // Act & Assert
            var ex = Assert.ThrowsException<Exception>(() => new Container(weight, containerType));
            Assert.AreEqual("Gewicht kan niet minder zijn dan 4 ton", ex.Message);
        }

        [TestMethod]
        public void Container_Initialization_ShouldThrowExceptionForInvalidWeightAboveMaximum()
        {
            // Arrange
            int weight = 31;
            Container.Type containerType = Container.Type.Normal;

            // Act & Assert
            var ex = Assert.ThrowsException<Exception>(() => new Container(weight, containerType));
            Assert.AreEqual("Gewicht kan niet meer zijn dan 30 ton", ex.Message);
        }

        [TestMethod]
        public void Container_Initialization_ShouldSetPropertiesForCoolableContainer()
        {
            // Arrange
            int weight = 15;
            Container.Type containerType = Container.Type.Coolable;

            // Act
            Container container = new Container(weight, containerType);

            // Assert
            Assert.AreEqual(weight, container.Weight);
            Assert.AreEqual(containerType, container.ContainerType);
            Assert.IsFalse(container.IsValuable);
            Assert.IsTrue(container.NeedsCooling);
        }

        [TestMethod]
        public void Container_Initialization_ShouldSetPropertiesForCoolableValuableContainer()
        {
            // Arrange
            int weight = 20;
            Container.Type containerType = Container.Type.CoolableValuable;

            // Act
            Container container = new Container(weight, containerType);

            // Assert
            Assert.AreEqual(weight, container.Weight);
            Assert.AreEqual(containerType, container.ContainerType);
            Assert.IsTrue(container.IsValuable);
            Assert.IsTrue(container.NeedsCooling);
        }
    }
}
