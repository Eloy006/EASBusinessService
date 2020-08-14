using System.Threading.Tasks;
using Grpc.Core;

namespace EASBusinessApi.DispatchService
{
    public class BusinessServiceClient
    {
        private static BusinessServiceClient _Default = null;
        public static BusinessServiceClient Default
        {
            get
            {
                if(_Default==null)
                    InitializeDefault("127.0.0.1",8079);
                return _Default;
            }
                

        }
        private Channel _channel;
        private BusinessProcessService.BusinessProcessServiceClient _client;

        public static void InitializeDefault(string ip,int port)
        {
            _Default=new BusinessServiceClient(ip,port);
        }

       


        public BusinessServiceClient(string ip,int port)
        {
            _channel = new Channel($"{ip}:{port}", ChannelCredentials.Insecure);
            _client = new BusinessProcessService.BusinessProcessServiceClient(_channel);
        }

        public async Task<ExecuteResponse> AttachReports(AttachReportsRequest request)
        {
            return await _client.AttachReportsAsync(request);
        }
        public async Task<ExecuteResponse> ProcessCreate(ProcessRequest beginRequest)
        {
            return await _client.ProcessCreateAsync(beginRequest);
        }
        public async Task<ExecuteResponse> DispatchCreate(DispatchRequest beginRequest)
        {
            return await _client.DispatchCreateAsync(beginRequest);
        }
        public async Task<ExecuteResponse> DispatchUpdate(DispatchRequest beginRequest)
        {
            return await _client.DispatchUpdateAsync(beginRequest);
        }

        public async Task<ExecuteResponse> UpdateTransaction(UpdateTransactionRequest beginRequest)
        {
            return await _client.UpdateTransactionAsync(beginRequest);
        }

    }
}
