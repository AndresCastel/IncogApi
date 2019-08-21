using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncognitusBack.API.ResponseObject
{
    public class ResponseObjectVM<T>
    {
        public bool Succesfull { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ResponseObjectVM(bool sucesfull, T result, string errormessage = null)
        {
            Succesfull = sucesfull;
            Data = result;
            Message = errormessage;
        }
    }
}
