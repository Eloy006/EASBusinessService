using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EASBusinessService.MediatR.Notification;
using EASBusinessService.Models;
using MediatR;

namespace EASBusinessService
{
    public class BusinessProcessList<T>:List<T>, IBusinessProcessList
    {


        public BusinessProcessList(int capacity) : base(capacity)
        {
        }

        public BusinessProcessList(IEnumerable<T> collection) : base(collection)
        {

        }

        public BusinessProcessList(IEnumerable<IBusinessProcess> processes):base(processes.OfType<T>())
        {
            
        }

    }
}
