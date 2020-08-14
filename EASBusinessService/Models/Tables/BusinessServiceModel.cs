using System;
using System.Collections.Generic;
using System.Linq;
using DataModels;
using EASBusinessApi;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;


namespace EASBusinessService.Models
{

    public class BusinessServiceModel: LaBusinessService,IBusinessProcess
    {

        public static BusinessServiceModel FromRequest(ProcessRequest request, bool shouldUpdate = false)
        {
            var newComDispatch = new BusinessServiceModel
            {

                TerminalId = request.ProcessKey.TerminalId,
                DataAreaId = request.ProcessKey.DataAreaId,
                StoreId = request.ProcessKey.StoreId,
                OperationId = Guid.Parse(request.ProcessKey.OperationId),
                StaffId = request.StaffId,
                OperationType = request.OperationType,
                EntityId = request.EntityId,
                ElementId = request.ElementId,
                TransactionId = request.TransactionId,
                Barcode = request.Barcode,

            };


            if (request.BeginTime != 0) newComDispatch.BeginDateTime = DateTime.FromBinary(request.BeginTime);
            if (request.EndTime != 0) newComDispatch.EndDateTime = DateTime.FromBinary(request.EndTime);

            return newComDispatch;
        }

        public static BusinessServiceModel FromRequest(DispatchRequest request, bool shouldUpdate = false)
        {
            var newComDispatch = new BusinessServiceModel
            {

                TerminalId = request.ProcessKey.TerminalId,
                DataAreaId = request.ProcessKey.DataAreaId,
                StoreId = request.ProcessKey.StoreId,
                OperationId = Guid.Parse(request.ProcessKey.OperationId),
                StaffId = request.StaffId,
                OperationType = request.OperationType,
                EntityId = request.EntityId,
                ElementId = request.DispatchId,
                TransactionId = request.TransactionId,

            };


            if (request.BeginTime != 0) newComDispatch.BeginDateTime = DateTime.FromBinary(request.BeginTime);
            if (request.EndTime != 0) newComDispatch.EndDateTime = DateTime.FromBinary(request.EndTime);

            return newComDispatch;
        }

        public bool ShouldUpdate { get; set; }
    }
}