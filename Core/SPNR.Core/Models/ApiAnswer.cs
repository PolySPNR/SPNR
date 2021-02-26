using System;
using System.Net;

namespace SPNR.Core.Models
{
    public class ApiAnswer<T>
    {
        public T Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public Exception Exception { get; set; }
    }

    public class ApiAnswer
    {
        public HttpStatusCode StatusCode { get; set; }
        public Exception Exception { get; set; }
    }
}