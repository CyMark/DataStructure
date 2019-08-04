using System;
using AlgoLib;


namespace DataStructure
{
    /// <summary>
    /// Contains 8K objects of data
    /// </summary>
    public class Page<T> : IPage<T> where T : IComparable
    {
        private readonly LinkList<DataRecord<T>> items;
        const int MaxEntries = 8192;
        

        public Page(int pageID)//, long NextRowID)
        {
            items = new LinkList<DataRecord<T>>();
            PageID = pageID;
            //RowID = NextRowID;
        }
        /*
        public Page(int pageID)
        {
            items = new LinkList<DataRecord<T>>();
            PageID = pageID;
            if(RowID == 0 ) { RowID = 1; }
        }
        */

        //public long RowID { get; set; }

        public int Count { get { return items.Count; } }

        public bool IsPageFull { get { return Count >= MaxEntries; } }

        public int PageID { get; private set; }

        public bool ContainsRow(long rowID) => RowIDtoIndex(rowID) != -1;

        public bool Add(T data, long nextRowID)
        {
            if(IsPageFull) { return false; }
            DataRecord<T> record = new DataRecord<T>(nextRowID, data);

            items.Add(record);
            return true;
        }

        public void Delete(DataRecord<T> data)
        {
            int idx = -1;
            for(int n = 0; n < items.Count; n++)
            {
                if(data.CompareTo(items[n]) == 0)
                {
                    idx = n;
                    break;
                }
            }
            //idx = items.IndexOf(data);

            //throw new Exception($"*DEBUG: Idx={idx}, items[{idx}].Record={items[idx].Record}");

            

            items.Remove(data);
        }

        public DataRecord<T> GetData(long rowID)
        {
            int idx = RowIDtoIndex(rowID);
            ValidateIndex(idx);
            return items[idx];
        }


        public void Update(DataRecord<T> newData)
        {

            items[RowIDtoIndex(newData.RowID)] = newData;
        }

        public DataRecord<T> this[long rowID]
        {
            get { return GetData(rowID); }
            set { Update(value); }
        }

        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= MaxEntries) { throw new IndexOutOfRangeException($"*Error: Max Index of {MaxEntries} exeeded!"); }
        }

        private int RowIDtoIndex(long rowID)
        {
            for(int n = 0; n < items.Count; n++)
            {
                if(items[n].RowID == rowID) { return n; }
            }
            return -1;
        }

    }
}
