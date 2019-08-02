using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace IncognitusBack.API.App_Start
{
    [DataContract]
    public class ApiResponse<T>
    {
        //[DataMember]
        //public string version { get { return "1.2.3"; } }

        [DataMember]
        public int statuscode { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string errorMessage { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public T data { get; set; }

        public ApiResponse(HttpStatusCode StatusCode, T result, string errormessage = null)
        {
            statuscode = (int)StatusCode;
            data = result;
            errorMessage = errormessage;
        }

    }
}
