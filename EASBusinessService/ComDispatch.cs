using System;
using System.Collections.Generic;
using System.Linq;
using EASBusinessApi;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;

namespace EASBusinessService.Models
{
    [Table(Name = "TestServices")]
    public class ComDispatch : IBusinessProcess
    {
        //public bool ShouldUpdate { get; set; }

        //DataAreaId(ASC), StoreId(ASC), TerminalId(ASC), TransactionId(ASC), DispatchId(ASC)
        [Column(Order = 5)]
        public string DispatchId { get; set; }

        [Column]
        public string DispatchBarcode { get; set; }

        [Column(Order = 4)]
        public string TransactionId { get; set; }

        [Column]
        public string CashVoucher { get; set; }

        [Column]
        public string OperatorId { get; set; }

        [Column(Order = 1)]
        public string DataAreaId { get; set; }

        [Column(Order = 3)]
        public string TerminalId { get; set; }

        [Column(Order = 2)]
        public string StoreId { get; set; }

        [Column]
        public DateTime? BeginDateTime { get; set; }

        [Column]
        public DateTime? EndDateTime { get; set; }

        [Column]
        public DateTime? LastUpdateTime { get; set; }

        public bool ShouldUpdate { get; set; }

        public static ComDispatch FromRequest(ComDispatchRequest request, bool shouldUpdate = false)
        {
            var newComDispatch = new ComDispatch
            {
                DispatchId = request.DispatchId,
                DispatchBarcode = request.DispatchBarcode,
                CashVoucher = request.CashVoucher,
                DataAreaId = request.DataAreaId,
                OperatorId = request.OperatorId,
                StoreId = request.StoreId,
                TerminalId = request.TerminalId,
                TransactionId = request.TransactionId,
                ShouldUpdate = shouldUpdate
            };


            if (request.BeginTime != 0) newComDispatch.BeginDateTime = DateTime.FromBinary(request.BeginTime);
            if (request.EndTime != 0) newComDispatch.EndDateTime = DateTime.FromBinary(request.EndTime);

            return newComDispatch;
        }

        public static bool SaveToDb(BusinessProcessDB db, List<ComDispatch> processList)
        {
            var toBulkInsert = processList.Where(x => !x.ShouldUpdate).ToList();
            var toUpdate = processList.Where(x => x.ShouldUpdate);

            if (toBulkInsert.Count > 0)
                if (toBulkInsert.Count > 1)
                    db.BulkCopy(toBulkInsert);
                else
                    db.Insert(toBulkInsert.Single());

            foreach (var comDispatch in toUpdate)
            {
                var updateLinq = db.Product.Where(p =>
                    p.StoreId == comDispatch.StoreId && p.TerminalId == comDispatch.TerminalId &&
                    p.DataAreaId == comDispatch.DataAreaId &&
                    p.TransactionId == comDispatch.TransactionId && p.DispatchId == comDispatch.DispatchId);

                var updatable = updateLinq.Set(x => x.LastUpdateTime, DateTime.Now);

                if (!string.IsNullOrWhiteSpace(comDispatch.CashVoucher))
                    updatable = updatable.Set(x => x.CashVoucher, comDispatch.CashVoucher);
                if (!string.IsNullOrWhiteSpace(comDispatch.DispatchBarcode))
                    updatable = updatable.Set(x => x.DispatchBarcode, comDispatch.DispatchBarcode);
                if (!string.IsNullOrWhiteSpace(comDispatch.OperatorId))
                    updatable = updatable.Set(x => x.OperatorId, comDispatch.OperatorId);
                if (comDispatch.BeginDateTime != null)
                    updatable = updatable.Set(x => x.BeginDateTime, comDispatch.BeginDateTime);
                if (comDispatch.EndDateTime != null)
                    updatable = updatable.Set(x => x.EndDateTime, comDispatch.EndDateTime);


                updatable.Update();
            }


            return true;
        }
    }
}