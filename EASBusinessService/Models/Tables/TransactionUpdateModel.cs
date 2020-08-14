using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using EASBusinessApi;

namespace EASBusinessService.Models.Tables
{
    public class TransactionUpdateModel : LaBusinessService, IBusinessProcess
    {
        public bool ShouldUpdate { get; set; }
        public string NewTransactionId { get; private set; }

        public static TransactionUpdateModel FromRequest(UpdateTransactionRequest request, bool shouldUpdate = false)
        {
            var updateModel= new TransactionUpdateModel
            {

                TerminalId = request.ProcessKey.TerminalId,
                DataAreaId = request.ProcessKey.DataAreaId,
                StoreId = request.ProcessKey.StoreId,
                OperationId =(string.IsNullOrEmpty(request.ProcessKey.OperationId) ?Guid.Empty:  Guid.Parse(request.ProcessKey.OperationId)),
                TransactionId = request.TransactionId,
                NewTransactionId = request.NewTransactionId,
                ShouldUpdate = shouldUpdate,
                FiscalPrint = request.FiscalPrint,

            };


            

            return updateModel;
        }

    }
}
