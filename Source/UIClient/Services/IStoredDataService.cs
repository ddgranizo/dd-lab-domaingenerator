using System;
using System.Collections.Generic;
using System.Text;

namespace UIClient.Services
{
    public  interface IStoredDataService<T>
    {
        T GetStoredData();
        void SaveStoredData(T data);
    }
}
