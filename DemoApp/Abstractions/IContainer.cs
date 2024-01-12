using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Abstractions
{
    public interface IContainer : IServiceProvider, IDisposable
    {
        IRegisteredType Register<TInterface, TImplement>() where TInterface : class where TImplement : class;
        IRegisteredType Register(Type type, Type typeImpl);
        IRegisteredType Register(Type type);
        IRegisteredType Register(Type type, Func<ILifeTime, object> factory);

        T Resolve<T>() where T : class;
        object Resolve(Type type);
    }
}
