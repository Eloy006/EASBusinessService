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
    public class DispatchSaver : INotificationHandler<BusinessProcessList<IBusinessProcess>>
    {
        private readonly DataConnection _db;

        public DispatchSaver(DataConnection bdb)
        {
            _db = bdb;
        }

        public Task Handle(BusinessProcessList<IBusinessProcess> processlist, CancellationToken cancellationToken)
        {
            var processList = processlist.OfType<BusinessServiceModel>().ToArray();

            if (!processList.Any()) return Task.FromResult(0);

            var toBulkInsert = processList.Where(x => !x.ShouldUpdate).ToList();
            //var toUpdate = processList.Where(x => x.ShouldUpdate);

            try
            {
                if (toBulkInsert.Count > 0)
                    if (toBulkInsert.Count > 1)
                        _db.BulkCopy(toBulkInsert);
                    else
                        _db.Insert(toBulkInsert.Single());

             /*   foreach (var businessProcess in toUpdate)
                {
                    var updateLinq = _db.EASBusinessServices.Where(p =>
                        p.StoreId == businessProcess.StoreId && p.TerminalId == businessProcess.TerminalId &&
                        p.DataAreaId == businessProcess.DataAreaId &&
                        p.TransactionId == businessProcess.TransactionId && p.ObjectId== businessProcess.ObjectId);

                    var updatable = updateLinq.Set(x => x.LastUpdateTime, DateTime.Now);

                    if (!string.IsNullOrWhiteSpace(businessProcess.CashVoucher))
                        updatable = updatable.Set(x => x.CashVoucher, businessProcess.CashVoucher);
                    if (!string.IsNullOrWhiteSpace(businessProcess.DispatchBarcode))
                        updatable = updatable.Set(x => x.DispatchBarcode, businessProcess.DispatchBarcode);
                    if (!string.IsNullOrWhiteSpace(businessProcess.OperatorId))
                        updatable = updatable.Set(x => x.OperatorId, businessProcess.OperatorId);
                    if (businessProcess.BeginDateTime != null)
                        updatable = updatable.Set(x => x.BeginDateTime, businessProcess.BeginDateTime);
                    if (businessProcess.EndDateTime != null)
                        updatable = updatable.Set(x => x.EndDateTime, businessProcess.EndDateTime);


                    updatable.Update();
                }
                */
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
               
            }
            return Task.FromResult(0);
        }
    }
}