using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Abstractions
{
    public interface ILifeTime : IServiceProvider
    {
        object GetSingleton(Type type, Func<ILifeTime, object> factory);
        object GetPerScope(Type type, Func<ILifeTime, object> factory);
        object GetTransient(Type type, Func<ILifeTime, object> factory);
    }
}
