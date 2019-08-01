using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public interface IPage<T>
    {
        int Count { get; }
        bool Add(T item);
        void Delete(T item);
        void Update(T item, int idx);
        T GetData(int idx);
    }
}
