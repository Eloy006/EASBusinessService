using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataModels;
using EASBusinessService.Models.Tables;
using LinqToDB;
using LinqToDB.Data;
using MediatR;

namespace EASBusinessService.Models.DB
{
    public class TransactionUpdater: INotificationHandler<BusinessProcessList<IBusinessProcess>>
    {
        private readonly DataConnection _db;

        public TransactionUpdater(DataConnection bdb)
        {
            _db = bdb;
        }
        public Task Handle(BusinessProcessList<IBusinessProcess> notification, CancellationToken cancellationToken)
        {
            var processList = notification.OfType<TransactionUpdateModel>().ToArray();

            if (!processList.Any()) return Task.FromResult(0);

            var toUpdate = processList.Where(x => x.ShouldUpdate);

                foreach (var businessProcess in toUpdate)
                {
                    var updateLinq = _db.GetTable<LaBusinessService>().Where(p =>
                        p.DataAreaId == businessProcess.DataAreaId && p.StoreId == businessProcess.StoreId &&
                        p.TerminalId == businessProcess.TerminalId &&
                        p.TransactionId == businessProcess.TransactionId);

                    var updatable = updateLinq.Set(x => x.LastUpdateTime, DateTime.Now);

                    if (!string.IsNullOrWhiteSpace(businessProcess.NewTransactionId))
                        updatable = updatable.Set(x => x.TransactionId, businessProcess.NewTransactionId);

                
                    updatable = updatable.Set(x => x.FiscalPrint, businessProcess.FiscalPrint);


                updatable.Update();
                }


            return Task.FromResult(0);
        }
    }
}
