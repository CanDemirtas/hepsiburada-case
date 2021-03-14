using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaikinProject.Models
{
    public class ServiceResult<T>
    {
        public T Data { get; set; }
        public ServiceResultType ResultType { get; set; }
        public string Message { get; set; }
        public int? ErrorCode { get; set; }
    }

    public enum ServiceResultType
    {
        Fail = 0,
        Success = 1
    }
}
