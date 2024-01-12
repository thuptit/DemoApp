using DemoApp.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp
{
    public class ContainerLifeTime : ObjectCache, ILifeTime
    {
        public Func<Type, Func<ILifeTime, object>> GetInstance { get; private set; }
        public ContainerLifeTime(Func<Type, Func<ILifeTime, object>> getInstance) => GetInstance = getInstance;

        public object GetPerScope(Type type, Func<ILifeTime, object> factory)
            => GetSingleton(type, factory);

        public object? GetService(Type serviceType) => GetInstance(serviceType)(this);

        public object GetSingleton(Type type, Func<ILifeTime, object> factory)
            => GetCache(type, factory, this);

        public object GetTransient(Type type, Func<ILifeTime, object> factory)
        {
            throw new NotImplementedException();
        }
    }
}
