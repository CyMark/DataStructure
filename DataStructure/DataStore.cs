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

        public DataStore()
        {
            pages = new LinkList<Page<T>>();
        }

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
            if(pages.Count == 0) { pages.Add(new Page<T>(0)); }
            
            for(int n = 0; n < pages.Count; n++)
            {

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
