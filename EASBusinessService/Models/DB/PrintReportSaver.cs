using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using DataModels;
using EASBusinessService.Models.Module;
using LinqToDB;
using LinqToDB.Data;
using MediatR;

namespace EASBusinessService.Models.DB
{
    public class PrintReportSaver : INotificationHandler<BusinessProcessList<IBusinessProcess>>
    {
        private readonly DataConnection _db;

        public PrintReportSaver(DataConnection bdb)
        {
            _db = bdb;
        }

        public Task Handle(BusinessProcessList<IBusinessProcess> processlist, CancellationToken cancellationToken)
        {
            var processList = processlist.OfType<PrintReportSaverModel>().ToArray();

            if (!processList.Any()) return Task.FromResult(0);


        

            
             

            var reports = processList.Where(x => !x.ShouldUpdate).ToList();

            _db.BulkCopy(reports);


            return Task.FromResult(0);
        }
    }
}