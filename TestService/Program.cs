using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EASBusinessApi;
using EASBusinessApi.DispatchService;
using Newtonsoft.Json;

namespace TestService
{
    class Program
    {


        static void Main(string[] args)
        {
            //EASBusinessApi.DispatchService.DispatchServiceClient client=new DispatchServiceClient("127.0.0.1",8079);
            BusinessServiceClient.InitializeDefault("127.0.0.1",8079);
            EASBusinessApi.DispatchService.BusinessServiceClient client =BusinessServiceClient.Default;

            Stopwatch stopwatch = new Stopwatch();

            
            stopwatch.Start();
            for (int i=0;i<1000;i++)
            client.DispatchCreate(new DispatchRequest()
            {
                
                ProcessKey = new BusinessProcessKey()
                { DataAreaId = "rp", StoreId = "1231312", TerminalId = "1234.00",OperationId = Guid.NewGuid().ToString()},

                DispatchId = $"00{i}",TransactionId = $"00000000{i}",BeginTime = DateTime.Now.ToBinary()
            });
            
            stopwatch.Stop();



            Console.WriteLine($"Time watch {stopwatch.Elapsed}");

            /*stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < 1000; i++)
                client.ComDispatchUpdate(new ComDispatchRequest() { DispatchId = $"00{i}", DataAreaId = "rp", StoreId = "1231312", TerminalId = "1234.00", TransactionId = $"00000000{i}",CashVoucher = $"{i}",EndTime = DateTime.Now.ToBinary()});
            stopwatch.Stop();
           
            Console.WriteLine($"Time watch {stopwatch.Elapsed}");
            */
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
