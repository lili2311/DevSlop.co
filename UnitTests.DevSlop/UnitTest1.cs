using Microsoft.VisualStudio.TestTools.UnitTesting;
using DevSlop.Models;

namespace UnitTests.DevSlop
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var mock = new ErrorViewModel();
            Assert.AreEqual(mock.ShowRequestId, false);

            mock.RequestId = "Hi Morgan"; 
            Assert.AreEqual(mock.ShowRequestId, true);

            mock.RequestId = "";
            Assert.AreEqual(mock.ShowRequestId, false);

        }
    }
}
