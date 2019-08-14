using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Application.ViewModels
{
    public class SimpleResponse
    {
        public object Data { get; set; }

        internal static object Create(object data)
        {
            return new SimpleResponse() { Data = data };
        }
    }

    public class SimpleResponse<T>
    {
        public T Data { get; set; }

        internal static SimpleResponse<T> Create(T data)
        {
            return new SimpleResponse<T>() { Data = data };
        }
    }
}
