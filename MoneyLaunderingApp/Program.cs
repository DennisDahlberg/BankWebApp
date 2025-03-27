using Autofac;
using MoneyLaunderingApp.AutoFac;

namespace MoneyLaunderingApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var container = ProgramModule.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var app = container.Resolve<App>();
                await app.Run();
            }
        }
    }
}
