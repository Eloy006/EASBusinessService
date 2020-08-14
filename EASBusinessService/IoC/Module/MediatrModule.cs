using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using EASBusinessService.Models.DB;
using MediatR;

namespace EASBusinessService.Models.Module
{
    public class MediatrModule:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.Register(x => new BusnessProcessWorker()).AsSelf();

            
            builder.RegisterType<DispatchSaver>().AsImplementedInterfaces();
            builder.RegisterType<PrintReportSaver>().AsImplementedInterfaces();
            builder.RegisterType<TransactionUpdater>().AsImplementedInterfaces();




            //builder.RegisterType<ComDispatch>().As<IBusinessProcess>();
            //builder.RegisterType<Dispatch>().As<IBusinessProcess>();

            //builder.RegisterAssemblyTypes(typeof(BusinessProcessList<ComDispatch>).Assembly).AsImplementedInterfaces();
            //builder.RegisterAssemblyTypes(typeof(BusinessProcessList<Dispatch>).Assembly).AsImplementedInterfaces();

        }

    }
}
