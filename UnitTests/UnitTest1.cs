using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestReadHtmlPage()
        {
            VictorianRoadsTT.MainPage.readHtmlPage("http://traffic.vicroads.vic.gov.au/getRecords.asp");
        }
    }
}
