using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using EASBusinessService.MediatR.Notification;
using EASBusinessService.Models.Module;
using LinqToDB.Extensions;
using MediatR;

namespace EASBusinessService.Models
{
    public class BusnessProcessWorker
    {
        public void PrepareProcess(IBusinessProcessList buffer)
        {
            using (var scope = IoC.Default.BeginLifetimeScope())
            {
                /*var group= (BusinessProcessList<IBusinessProcess>) buffer;

                var grp=group.GroupBy(x => x.GetType());
                */
                

                var mediator = scope.Resolve<IMediator>();
                mediator.Publish(buffer);
            }

           
        }
    }
}