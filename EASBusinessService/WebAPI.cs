using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Autofac;
using EASBusinessApi;
using EASBusinessService.Models.Module;
using Grpc.Core;

namespace EASBusinessService
{
    public class WebAPI
    {
        private AutoResetEvent eventExit=new AutoResetEvent(false);
        private ServiceImpl _service;
        public WebAPI(ServiceImpl service)
        {
            _service = service;
        }

        public void Stop()
        {
            eventExit.Set();
        }

        public void Start()
        {
           

            Server server = new Server
            {
                Services = { BusinessProcessService.BindService(_service) },
                Ports ={ {  "localhost", 8079, ServerCredentials.Insecure } }
            };

            server.Start();
            eventExit.WaitOne();
        }


    }
}
