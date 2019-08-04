using System;
using AlgoLib;


namespace DataStructure
{
    /// <summary>
    /// Contains 8K objects of data
    /// </summary>
    public class Page<T> : IPage<T>, IEquatable<Page<T>> where T : IComparable
    {
        private readonly LinkList<DataRecord<T>> items;
        const int MaxEntries = 8192;
        
        public Page(int pageID)//, long NextRowID)
        {
            items = new LinkList<DataRecord<T>>();
            PageID = pageID;
            MaxRowID = MinRowID = 0;
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
        public long MaxRowID { get; private set; }
        public long MinRowID { get; private set; }

        public bool IsPageFull { get { return Count >= MaxEntries; } }
        public bool IsPageEmpty { get { return Count == 0; } }

        public int PageID { get; private set; }

        public bool ContainsRow(long rowID) => RowIDtoIndex(rowID) != -1;

        public bool Add(T data, long nextRowID)
        {
            if(IsPageFull) { throw new InvalidOperationException($"*Error: Page is full! Record Count={Count}"); }
            DataRecord<T> record = new DataRecord<T>(nextRowID, data);

            items.Add(record);

            if(nextRowID > MaxRowID) { MaxRowID = nextRowID; }
            if (nextRowID < MinRowID) { MinRowID = nextRowID; }

            return true;
        }

        public void Delete(long rowID)
        {
            int idx = RowIDtoIndex(rowID);
            items.RemoveNodeAt(idx);
            if(rowID == MinRowID) { CalCulateMinRowID(); }
            if(rowID == MaxRowID) { CalCulateMinRowID(); }
        }

        public void Delete(DataRecord<T> data)
        {
            /*
            int idx = -1;
            for(int n = 0; n < items.Count; n++)
            {
                if(data.Equals(items[n]))
                {
                    idx = n;
                    break;
                }
            }
            idx = items.IndexOf(data);
            */
            //throw new Exception($"*DEBUG: Idx={idx}, items[{idx}].Record={items[idx].Record}");

            items.Remove(data);
        }

        public T GetDataRecord(long rowID) => GetData(rowID).Record;

        public DataRecord<T> GetData(long rowID)
        {
            int idx = RowIDtoIndex(rowID);
            ValidateIndex(idx);
            return items[idx];
        }


        public void Update(DataRecord<T> newData)
        {
            items[RowIDtoIndex(newData.RowID)] = newData;
            if(newData.RowID > MaxRowID) { MaxRowID = newData.RowID; }
            if(newData.RowID < MinRowID) { MinRowID = newData.RowID; }
        }

        public DataRecord<T> this[long rowID]
        {
            get { return GetData(rowID); }
            set { Update(value); }
        }

        public long Search(T data) => Search(data, MinRowID, MaxRowID);
        
        public long Search(T data, long fromRowID, long toRowID)
        {
            for (int n = 0; n < Count; n++)
            {
                if(items[n].RowID < fromRowID || items[n].RowID > toRowID) { continue; }
                if (items[n].Record.Equals(data))
                {
                    return items[n].RowID;
                }
            }
            return -1;
        }



        public LinkList<long> FindRecord(T data)
        {
            LinkList<long> results = new LinkList<long>();

            return results;
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


        private void CalCulateMinRowID()
        {
            foreach(var rec in items)
            {
                if(rec.RowID < MinRowID) { MinRowID = rec.RowID; }
            }
        }

        private void CalCulateMaxRowID()
        {
            foreach (var rec in items)
            {
                if (rec.RowID > MaxRowID) { MaxRowID = rec.RowID; }
            }
        }

        public bool Equals(Page<T> other)
        {
            return PageID == other.PageID;
        }
    }
}
