using System;


namespace DataStructure
{
    public interface IPage<T> where T : IComparable
    {
        int Count { get; }
        bool Add(T item, long nexRowID);
        void Delete(DataRecord<T> item);
        void Update(DataRecord<T> item);
        DataRecord<T> GetData(long rowID);
    }
}
