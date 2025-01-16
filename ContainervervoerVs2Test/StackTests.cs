using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContainervervoerVs2; // Ensure this namespace is included to use the correct Container class

namespace ContainervervoerVs2.Tests
{
    [TestClass]
    public class StackTests
    {
        [TestMethod]
        public void Constructor_ShouldSetIsCooled()
        {
            // Arrange & Act
            var stack = new Stack(isCooled: true);

            // Assert
            Assert.IsTrue(stack.IsCooled);
        }

    }
}
