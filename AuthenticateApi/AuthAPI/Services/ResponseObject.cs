
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AuthAPI.Services
{
    public class ResponseObject
    {
        public string ResponseMessage { get; set; }
        public object ResObj {  get; set; }
        public int Status { get; set; }


        public ResponseObject(object responseObject, string responseMessage, int statusCode)
        {
            ResObj = responseObject;
            ResponseMessage = responseMessage;
            Status = statusCode;
        }

        public ResponseObject()
        {

        }

        public static object create(string v, object value, int status)
        {
            return new ResponseObject { ResObj = value, ResponseMessage = v, Status = status };
        }

        public static object create(string v, int status)
        {
            return new ResponseObject { ResObj = null!, ResponseMessage = v, Status = status };
        }
    }
}
