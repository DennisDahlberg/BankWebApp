using Autofac;
using MoneyLaunderingConsoleApp.AutoFac;

namespace MoneyLaunderingConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var container = ProgramModule.Configure();

                using (var scope = container.BeginLifetimeScope())
                {
                    var app = container.Resolve<App>();
                    app.Run().Wait();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
