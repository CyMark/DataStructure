using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructure;

namespace UnitTestDataStrucruture
{
    [TestClass]
    public class UnitTestPage
    {
        [TestMethod]
        public void PageBasics()
        {
            Page<string> page = new Page<string>(1);
            Assert.AreEqual(0, page.Count);
            Assert.AreEqual(1, page.PageID);

            string string1 = "Item #0";
            page.Add(string1);

            Assert.AreEqual(1, page.Count);
            Assert.AreEqual(string1, page[0]);

            page.Add("Item #1");
            page.Update("Item #00", 0);
            Assert.AreEqual(2, page.Count);
            Assert.AreEqual("Item #00", page[0]);

            page.Add("Item #2");
            page.Delete("Item #1");

            Assert.AreEqual(2, page.Count);
            Assert.AreEqual("Item #2", page[1]);

            Assert.IsFalse(page.IsPageFull);

            // Load 10 000 items
            for (int n = 0; n < 80000; n++)
            { page.Add($"Item #{n}"); }

            Assert.IsTrue(page.IsPageFull);

            Assert.AreEqual(8192, page.Count);
            
            Assert.AreEqual("Item #3998", page[4000]);
        }
    }
}
