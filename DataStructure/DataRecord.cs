using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public class DataRecord<T> : IComparable, IComparable<DataRecord<T>>
    {
        public DataRecord(long recID, T data)
        {
            Record = data;
            RowID = recID;
        }

        public DataRecord(DataRecord<T> copy)
        {
            RowID = copy.RowID;
            Record = Record;
        }

        public long RowID { get; set; }
        public T Record { get; set; }


        public int CompareTo(DataRecord<T> other)
        {
            if (other == null) { return -1; }
      
            return RowID.CompareTo(other.RowID);
        }

        int IComparable.CompareTo(object obj)
        {
            if (obj.GetType() != GetType()) { return -1; }
            return CompareTo(obj as DataRecord<T>);
        }
    }
}
