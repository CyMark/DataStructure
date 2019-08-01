using System;
using AlgoLib;

namespace DataStructure
{
    /// <summary>
    /// Stores pages of Data
    /// </summary>
    public class DataStore<T> : IPage<T>
    {
        LinkList<Page<T>> pages;
        Page<T> currentPage;

        public DataStore()
        {
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

        public bool Add(T item)
        {
            if(pages.Count == 0) { pages.Add(new Page<T>(0)); currentPage = pages[0]; }

            if(!currentPage.IsPageFull) { currentPage.Add(item); }

            bool hasAdded = false;
            // sweep from first page to fill in deleted entries
            for(int n = 0; n < pages.Count; n++)
            {
                if(!pages[n].IsPageFull)
                {
                    pages[n].Add(item);
                    hasAdded = true;
                    break;
                }
            }

            if(!hasAdded)
            {
                Page<T> page = new Page<T>(pages.Count);
                currentPage = page;
                page.Add(item);
                pages.Add(page);
            }

            return true;
        }

        public void Delete(T item)
        {
            throw new NotImplementedException();
        }

        public T GetData(int idx)
        {
            throw new NotImplementedException();
        }

        public void Update(T item, int idx)
        {
            throw new NotImplementedException();
        }
    }
}
