
using Microsoft.Extensions.Caching.Memory;

namespace EmployeePayslip.Utilities;
public interface IMemoryCacheWrapper
{
    bool TryGetValue<TItem>(object key, out TItem? value);
    ICacheEntry CreateEntry(object key);
    TItem Set<TItem>(object key, TItem value);
}

public class MemoryCacheWrapper : IMemoryCacheWrapper
{
    private readonly IMemoryCache _memoryCache;

    public MemoryCacheWrapper(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public bool TryGetValue<TItem>(object key, out TItem? value)
    {
        return _memoryCache.TryGetValue(key, out value);
    }

    public ICacheEntry CreateEntry(object key)
    {
        return _memoryCache.CreateEntry(key);
    }

    public TItem Set<TItem>(object key, TItem value)
    {
        return _memoryCache.Set(key, value);
    }
}
