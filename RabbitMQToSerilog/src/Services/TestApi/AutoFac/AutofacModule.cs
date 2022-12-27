using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TestApi.AutoFac
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // scan all assemblies in current application domain and resolve them on convention
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsImplementedInterfaces();
        }

        
    }
}
