using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Dapper;
using EASBusinessApi;
using EASBusinessService.Models;
using EASBusinessService.Models.Module;
using LinqToDB;
using LinqToDB.Data;
using MediatR;

namespace EASBusinessService
{
    public class QueryWorker
    {
        private ConcurrentQueue<IBusinessProcess> _businessProcess;

        private ManualResetEvent _stopThreadEvent;
        private BusnessProcessWorker _bpw;

        public QueryWorker(BusnessProcessWorker bpw)
        {
            _bpw = bpw;
            _stopThreadEvent = new ManualResetEvent(true);
            _businessProcess = new ConcurrentQueue<IBusinessProcess>();

            
            Task.Factory.StartNew(ThreadWorker);
            
        }


        public void Push(IBusinessProcess request)
        {
            try
            {
                _businessProcess.Enqueue(request);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        private async void ThreadWorker()
        {
            while (_stopThreadEvent.WaitOne(0))
            {
                await Task.Delay(1000);

                if (_businessProcess.Count != 0)
                {
                    try
                    {
                        int wCount = 0;
                        var stopwatch = new Stopwatch();
                        var buffer = new BusinessProcessList<IBusinessProcess>(100);

                        stopwatch.Start();

                        while (_businessProcess.TryDequeue(out var dispatchRequest))
                        {
                            buffer.Add(dispatchRequest);
                            if (buffer.Count >= buffer.Capacity) break;
                            wCount++;
                        }
                        stopwatch.Stop();
                        Logger.Debug(
                            $"Message count: {_businessProcess.Count}, Buferred: {wCount}, TimeWatch: {stopwatch.Elapsed}");

                        stopwatch.Reset();

                        _bpw.PrepareProcess(buffer);
                    }
                    catch (Exception e)
                    {
                        Logger.Debug(e.ToString());
                    }
                }
            }
        }
    }
}