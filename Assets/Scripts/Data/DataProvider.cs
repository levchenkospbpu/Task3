using System.Collections.Generic;

public struct DataProvider
{
    private readonly object[] _data;

    public DataProvider(params object[] data)
    {
        _data = data;
    }

    public T GetData<T>()
    {
        foreach (var data in _data)
        {
            if (data is T tData) return tData;
        }
        return default;
    }

    public IEnumerable<object> GetAllData()
    {
        return _data;
    }
}