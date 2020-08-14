using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using EASBusinessApi;
using EASBusinessService.Models;
using EASBusinessService.Models.DB;
using EASBusinessService.Models.Tables;
using Grpc.Core;
using LinqToDB;


namespace EASBusinessService
{
    public class ServiceImpl : BusinessProcessService.BusinessProcessServiceBase
    {
        private readonly QueryWorker _worker;
        public ServiceImpl(QueryWorker worker)
        {
            _worker = worker;
        }

        public override async Task<ExecuteResponse> ProcessCreate(ProcessRequest request, ServerCallContext context)
        {
            return await Task.Factory.StartNew(TaskPush, BusinessServiceModel.FromRequest(request));
        }

        public override async Task<ExecuteResponse> UpdateTransaction(UpdateTransactionRequest request, ServerCallContext context)
        {
            return await Task.Factory.StartNew(TaskPush, TransactionUpdateModel.FromRequest(request,true));
        }

        public override async Task<ExecuteResponse> AttachReports(AttachReportsRequest request, ServerCallContext context)
        {
            return await Task.Factory.StartNew(TaskPush, PrintReportSaverModel.FromRequest(request));
        }


        public override async Task<ExecuteResponse> DispatchCreate(DispatchRequest request, ServerCallContext context)
        {
            return await Task.Factory.StartNew(TaskPush, BusinessServiceModel.FromRequest(request));
        }

        public override async Task<ExecuteResponse> DispatchUpdate(DispatchRequest request, ServerCallContext context)
        {
            return await Task.Factory.StartNew(TaskPush, BusinessServiceModel.FromRequest(request, true));
        }

        private ExecuteResponse TaskPush(object state)
        {
            var process = (IBusinessProcess) state;
            _worker.Push(process);
            
            return new ExecuteResponse() { Message = new ErrorMessage(){HasError = false} };
        }


    }
}
