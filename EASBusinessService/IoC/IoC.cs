using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using DataModels;
using LinqToDB.DataProvider.SqlServer;

namespace EASBusinessService.Models.Module
{
    public  static class IoC
    {
        public static IContainer Default { get; }

        static IoC()
        {
            var builder = new Autofac.ContainerBuilder();
            builder.RegisterModule(new MediatrModule());

            builder.RegisterType<WebAPI>().AsSelf().SingleInstance();
            builder.RegisterType<QueryWorker>().AsSelf().SingleInstance();
            builder.RegisterType<ServiceImpl>().AsSelf().SingleInstance();
            
            builder.Register(x =>
                {

                    var sqlString =
                        "Data Source=.;Initial Catalog=BusinessModel;Integrated Security=true;User Id=POSSQL;Password=123456";
                    return SqlServerTools.CreateDataConnection(sqlString);

                }
            ).InstancePerLifetimeScope();
            Default = builder.Build();


            

        }

    }
}
