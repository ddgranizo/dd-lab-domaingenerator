using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Services.System.Interfaces
{
    public  interface IStoredDataService<T>
    {
        T GetStoredData();
        void SaveStoredData(T data);
    }
}
