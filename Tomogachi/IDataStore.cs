using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tomogachi
{
    internal interface IDataStore<T>
    {
        Task<bool> CreateItem(T Data, string StorageKey);
        bool DeleteItem(T Item);
        bool UpdateItem(T Item, bool IsSleeping);
        public Task<T> ReadItem(string id);

    }
}
