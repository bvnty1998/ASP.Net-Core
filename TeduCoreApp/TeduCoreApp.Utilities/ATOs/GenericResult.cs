using System;
using System.Collections.Generic;
using System.Text;

namespace TeduCoreApp.Utilities.ATOs
{
   public class GenericResult
    {
        public GenericResult()
        {

        }
        public object Data { set; get; }
        public bool Success { set; get; }
        public string Message { set; get; }
        public object Error { get; set; }
        public GenericResult(bool success)
        {
            Success = success;
        }
        public GenericResult(bool success,string message)
        {
            Success = success;
            Message = message;
        }
        public GenericResult(bool success, object data)
        {
            Success = success;
            Data = data;
        }

    }
}
