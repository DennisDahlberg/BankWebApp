using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyLaunderingApp.AutoFac
{
    public class ProgramModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<App>().AsSelf();
        }

        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<ProgramModule>();
            return builder.Build();
        }
    }
}
