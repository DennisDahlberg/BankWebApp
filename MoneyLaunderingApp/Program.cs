using Autofac;
using MoneyLaunderingApp.AutoFac;

namespace MoneyLaunderingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var container = ProgramModule.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var app = container.Resolve<App>();
                app.Run();
            }
        }
    }
}
