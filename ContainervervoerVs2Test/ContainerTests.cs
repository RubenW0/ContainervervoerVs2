using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContainervervoerVs2;

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
            bool isValuable = true;
            bool needsCooling = false;

            // Act
            Container container = new Container(weight, isValuable, needsCooling);

            // Assert
            Assert.AreEqual(weight, container.Weight);
            Assert.IsTrue(container.IsValuable);
            Assert.IsFalse(container.NeedsCooling);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Gewicht kan niet minder zijn dan 4 ton")]
        public void Container_Initialization_ShouldThrowExceptionForInvalidWeightBelowMinimum()
        {
            // Arrange
            int weight = 3;
            bool isValuable = false;
            bool needsCooling = false;

            // Act
            Container container = new Container(weight, isValuable, needsCooling);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Gewicht kan niet meer zijn dan 30 ton")]
        public void Container_Initialization_ShouldThrowExceptionForInvalidWeightAboveMaximum()
        {
            // Arrange
            int weight = 31;
            bool isValuable = false;
            bool needsCooling = false;

            // Act
            Container container = new Container(weight, isValuable, needsCooling);
        }
    }
}
