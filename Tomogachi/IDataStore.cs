using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tomogachi
{
    internal interface IDataStore<T>
    {
        Task<bool> CreateItem(T Data);
        bool DeleteItem(T Item);
        bool UpdateItem(T Item);
        public Task<T> ReadItem(string id);

    }
}
