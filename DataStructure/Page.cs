using System;
using AlgoLib;


namespace DataStructure
{
    /// <summary>
    /// Contains 8K objects of data
    /// </summary>
    public class Page<T> : IPage<T>
    {
        private readonly LinkList<T> items;
        const int MaxEntries = 8192;
        

        public Page(int pageID)
        {
            items = new LinkList<T>();
            PageID = pageID;
        }

        public int Count { get { return items.Count; } }

        public bool IsPageFull { get { return Count >= MaxEntries; } }

        public int PageID { get; private set; }

        public bool Add(T data)
        {
            if(IsPageFull) { return false; }
            items.Add(data);
            return true;
        }

        public void Delete(T data)
        {
            //ValidateIndex(idx);
            items.Remove(data);
        }

        public T GetData(int idx)
        {
            ValidateIndex(idx);
            return items[idx];
        }


        public void Update(T newData, int idx)
        {
            items[idx] = newData;
        }

        public T this[int idx]
        {
            get { return GetData(idx); }
            set { Update(value, idx); }
        }

        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= MaxEntries) { throw new IndexOutOfRangeException($"*Error: Max Index of {MaxEntries} exeeded!"); }
        }

    }
}
