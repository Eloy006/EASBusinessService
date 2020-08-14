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

    public class PrintReportSaverModel : LaBusinessServicePrintReport, IBusinessProcess
    {
        

        public static PrintReportSaverModel FromRequest(AttachReportsRequest request, bool shouldUpdate = false)
        {
            


            return new PrintReportSaverModel
            {


                TerminalId = request.ProcessKey.TerminalId,
                DataAreaId = request.ProcessKey.DataAreaId,
                StoreId = request.ProcessKey.StoreId,
                OperationId = Guid.Parse(request.ProcessKey.OperationId),
                ClassId = request.Reports.ClassId,
                EntityId = request.Reports.EntityId,
                Description = request.Reports.Description,
                NumberOfCopies = request.Reports.NumberOfCopies,
                PageCount = request.Reports.PageCount,
                TotalPrintedPages = request.Reports.TotalPrintedPages,
                PrintReportsId = Guid.Parse(request.Reports.ReportsId),

            };
                
            

           

            
            
        }

        public bool ShouldUpdate { get; set; }
    }
}