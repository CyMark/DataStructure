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

            long rec = 1;
            string string1 = "Item #1";
            //DataRecord<string> record = new DataRecord<string>(rec++, string1);
            page.Add(string1, rec++);

            Assert.AreEqual(1, page.Count);
            Assert.AreEqual(string1, page[1].Record);

            //DataRecord<string> record2 = new DataRecord<string>(rec++, "Item #1");
            page.Add("Item #2", rec++);
            Assert.AreEqual(2, page.Count);
            Assert.AreEqual(2, page[2].RowID);
            Assert.AreEqual("Item #2", page[2].Record);

            page.Update(new DataRecord<string>(1, "Item #00"));
            Assert.AreEqual(2, page.Count);
            Assert.AreEqual("Item #00", page[1].Record);
            Assert.AreEqual(1, page[1].RowID);
            
            page.Add("Item #3", rec++);
            Assert.AreEqual(3, page.Count);

            char[] chs = new char[page[2].Record.Length];

            for(int n = 0; n < page[2].Record.Length; n++)
            {
                chs[n] = page[2].Record[n];
            }

            string g = new string(chs);
            Assert.AreEqual("Item #2", g);
            Assert.AreEqual(2, page[2].RowID);
            DataRecord<string> record2 = new DataRecord<string>(2, g);
            
            page.Delete(record2);
            
            Assert.AreEqual(2, page.Count);
            Assert.AreEqual(1, page[1].RowID);
            Assert.AreEqual("Item #00", page[1].Record);
            
            Assert.IsFalse(page.IsPageFull);
            
            // Load 10 000 items
            for (int n = 0; n < 9000; n++)
            {
                if (!page.IsPageFull) { page.Add($"Item #{n}", rec++); }
            }

            Assert.IsTrue(page.IsPageFull);
            
            Assert.AreEqual(8192, page.Count); // full page
            
            Assert.AreEqual("Item #3996", page[4000].Record);  // 3 added then 3999 - 3 = 3996

            // Last record in list:  NO THIS IS NOT THE LAST RECORD, IT is the record with ROWID = 8191
            Assert.AreEqual(8191, page[8191].RowID); // ??this record RowID would have been 8192 (started at 1) if one record was not deleted

            /**/

            // Delete row 1:
            page.Delete(1);
            Assert.IsFalse(page.IsPageFull);
            Assert.AreEqual(8191, page.Count);

            Assert.IsFalse(page.ContainsRow(1));
            Assert.IsFalse(page.ContainsRow(2));

            page.Delete(3);
            Assert.IsFalse(page.IsPageFull);
            Assert.AreEqual(8190, page.Count);
            page.Delete(4);
            page.Delete(5);

            page.Add($"Item #{rec}n", rec++);
            page.Add($"Item #{rec}n", rec++); 
            page.Add($"Item #{rec}n", rec++);
            page.Add($"Item #{rec}n", rec++); 

            Assert.AreEqual(8198, rec);

            Assert.IsTrue(page.IsPageFull);
            Assert.AreEqual(8192, page.Count);
            Assert.AreEqual(8196, page[8196].RowID);
            //Assert.AreEqual("Item #8192n", page[8192].Record);

            Assert.AreEqual(8197, page.Search("Item #8197n"));
        }

        [TestMethod]
        public void TestSearch()
        {
            Page<TestData> page = new Page<TestData>(1);
        }


    }
}
