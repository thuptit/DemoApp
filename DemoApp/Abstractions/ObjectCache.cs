using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Abstractions
{
    public abstract class ObjectCache : IDisposable
    {
        private readonly ConcurrentDictionary<Type, object> _cache = new();
        protected object GetCache(Type type, Func<ILifeTime, object> factory, ILifeTime lifeTime)
            => _cache.GetOrAdd(type, _ => factory(lifeTime));
        public void Dispose()
        {
            foreach (var obj in _cache.Values)
            {
                (obj as IDisposable)?.Dispose();
            }
        }
    }
}
