using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApis.Contract
{
    public class ApiResponse
    {
        public string Message { get; set; }
        public dynamic Data { get; set; }
        public int Status { get; set; }
        public string Token { get; set; }
    }
}
