using DemoApp.Abstractions;
using System.Linq.Expressions;
using System.Reflection;

namespace DemoApp
{
    public class Container : IContainer
    {
        private readonly Dictionary<Type, Func<ILifeTime, object>> _registeredTypes = new();
        private readonly ContainerLifeTime _lifeTime;
        public Container()
        {
            _lifeTime = new ContainerLifeTime((type) => _registeredTypes[type]);
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ScopeLifeTime CreateScope() => new ScopeLifeTime(_lifeTime);

        public object? GetService(Type serviceType)
        {
            if (!_registeredTypes.TryGetValue(serviceType, out var registeredType))
            {
                throw new Exception("Need to Register Service");
            }

            return registeredType(_lifeTime);
        }

        public IRegisteredType Register<TInterface, TImplement>()
            where TInterface : class
            where TImplement : class
            => Register(typeof(TInterface), typeof(TImplement));

        public IRegisteredType Register(Type type, Type typeImpl)
            => Register(type, GetFactoryFromType(typeImpl));

        public IRegisteredType Register(Type type)
            => Register(type, GetFactoryFromType(type));

        public IRegisteredType Register(Type type, Func<ILifeTime, object> factory)
            => new RegisteredType(type, f => _registeredTypes[type] = f, factory);

        private static Func<ILifeTime, object> GetFactoryFromType(Type type)
        {
            var constructors = type.GetConstructors();
            if(constructors.Length == 0)
            {
                constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
            }
            var constructor = constructors.First();
            
            //get params from constructor
            var arg = Expression.Parameter(typeof(ILifeTime));
            var body = Expression.New(constructor, constructor.GetParameters().Select(param =>
            {
                var resolveInstance = new Func<ILifeTime, object>(lifetime => lifetime.GetService(param.ParameterType));
                return Expression.Convert(Expression.Call(Expression.Constant(resolveInstance.Target), resolveInstance.Method, arg),param.ParameterType);
            }));

            return (Func<ILifeTime, object>)Expression.Lambda(body, arg).Compile();
        }
        public T Resolve<T>() where T : class
        {
            return (T)Resolve(typeof(T));
        }

        public object Resolve(Type type)
        {
            return GetService(type);
        }
    }
}
