using DemoApp.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp
{
    public class RegisteredType : IRegisteredType
    {
        private readonly Type _type;
        private readonly Action<Func<ILifeTime, object>> _registeredFactory;
        private readonly Func<ILifeTime, object> _factory;
        public RegisteredType(Type type, Action<Func<ILifeTime, object>> registerFactory, Func<ILifeTime, object> factory)
        {
            _type = type;
            _registeredFactory = registerFactory;
            _factory = factory;

            _registeredFactory(_factory);
        }
        public void AsPerScope() => _registeredFactory(lifetime => lifetime.GetPerScope(_type, _factory));

        public void AsSingleton() => _registeredFactory(lifetime => lifetime.GetSingleton(_type, _factory));

        public void AsTransient()
        {
            
        }
    }
}
