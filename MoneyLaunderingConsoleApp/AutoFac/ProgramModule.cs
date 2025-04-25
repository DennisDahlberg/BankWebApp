using Autofac;
using DataAccessLayer.Models;
using Services;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MoneyLaunderingConsoleApp.AutoFac
{
    public class ProgramModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<App>().AsSelf();
            builder.RegisterType<BankAppDataContext>().AsSelf();
            builder.RegisterType<TransactionService>().As<ITransactionService>();
            builder.RegisterType<ReportService>().As<IReportService>();
        }

        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<ProgramModule>();
            return builder.Build();
        }
    }
}
