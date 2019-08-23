using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncognitusBack.API.ViewModels
{
    public class MessageResponseViewModel<T>
    {
        public bool Succesfull { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
