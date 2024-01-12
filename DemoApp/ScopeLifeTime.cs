using DemoApp.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp
{
    public class ScopeLifeTime : ObjectCache, ILifeTime
    {
        private readonly ContainerLifeTime _parentLifeTime;
        public ScopeLifeTime(ContainerLifeTime containerLifeTime)
        {
            _parentLifeTime = containerLifeTime;
        }
        public object GetPerScope(Type type, Func<ILifeTime, object> factory) => GetCache(type, factory, this);

        public object? GetService(Type serviceType) => _parentLifeTime.GetInstance(serviceType)(this);

        public object GetSingleton(Type type, Func<ILifeTime, object> factory) => _parentLifeTime.GetSingleton(type, factory);

        public object GetTransient(Type type, Func<ILifeTime, object> factory)
        {
            throw new NotImplementedException();
        }
    }
}
