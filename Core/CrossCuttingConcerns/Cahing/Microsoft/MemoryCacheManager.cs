using Core.CrossCuttingConcerns.Cahing;
using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Cahing.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        IMemoryCache _memoryCache;
        public MemoryCacheManager()
        {
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        }
        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key,value,TimeSpan.FromMinutes(duration));
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key, out _);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            dynamic cacheEntriesCollection = null;

            // 1. .NET 7 ve 8 için _coherentState ve _entries kontrolü
            var coherentStateField = typeof(MemoryCache).GetField("_coherentState",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            if (coherentStateField != null)
            {
                var coherentState = coherentStateField.GetValue(_memoryCache);
                var entriesField = coherentState.GetType().GetField("_entries",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                if (entriesField != null)
                {
                    cacheEntriesCollection = entriesField.GetValue(coherentState);
                }
            }
            else
            {
                // 2. .NET 6 ve öncesi için yedek plan
                var entriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                if (entriesCollectionDefinition != null)
                {
                    cacheEntriesCollection = entriesCollectionDefinition.GetValue(_memoryCache);
                }
            }

            if (cacheEntriesCollection == null) return;

            var keysToRemove = new List<string>();
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);

            // 3. Eşleşenleri bul ve listeye ekle
            foreach (var cacheItem in cacheEntriesCollection)
            {
                var keyProperty = cacheItem.GetType().GetProperty("Key");
                if (keyProperty != null)
                {
                    var key = keyProperty.GetValue(cacheItem)?.ToString();
                    if (key != null && regex.IsMatch(key))
                    {
                        keysToRemove.Add(key);
                    }
                }
            }

            // 4. Listeye eklenen anahtarları GERÇEKTEN bellekten sil
            foreach (var key in keysToRemove.Distinct())
            {
                _memoryCache.Remove(key);
            }
        }
    }
}
