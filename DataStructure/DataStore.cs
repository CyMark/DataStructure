using System;
using AlgoLib;

namespace DataStructure
{
    /// <summary>
    /// Stores pages of Data
    /// </summary>
    public class DataStore<T> : IPage<T> //where T : IComparable
    {
        LinkList<Page<T>> pages;
        Page<T> currentPage;
        //long RowID;

        public DataStore()
        {
            RowID = 1;
            pages = new LinkList<Page<T>>();
        }

        public string DataStoreName { get; set; }

        public int PageCount { get { return pages.Count; } }

        public int Count
        {
            get
            {
                if(pages.Count == 0) { return 0; }
                int count = 0;
                foreach(var page in pages) { count += page.Count; }
                return count;
            }
        }

        public long RowID { get => RowID; private set => RowID = value; }

        public bool Add(T item) => Add(item, RowID++);

        public bool Add(T item, long nextRowID)
        {
            if(pages.Count == 0) { pages.Add(new Page<T>(1)); currentPage = pages[0]; }

            if(!currentPage.IsPageFull) { currentPage.Add(item, nextRowID); }

            bool hasAdded = false;
            // sweep from first page to fill in deleted entries
            for(int n = 0; n < pages.Count; n++)
            {
                if(!pages[n].IsPageFull)
                {
                    pages[n].Add(item, nextRowID);
                    hasAdded = true;
                    break;
                }
            }

            if(!hasAdded)
            {
                Page<T> page = new Page<T>(pages.Count);
                currentPage = page;
                page.Add(item, nextRowID);
                pages.Add(page);
            }

            return true;
        }

        public void Delete(DataRecord<T> item)
        {
            throw new NotImplementedException();
        }

        public DataRecord<T> GetData(long rowID)
        {
            throw new NotImplementedException();
        }

        public void Update(DataRecord<T> item)
        {
            throw new NotImplementedException();
        }
    }
}
