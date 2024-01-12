using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Abstractions
{
    public interface IRegisteredType
    {
        void AsSingleton();
        void AsPerScope();
        void AsTransient();
    }
}
