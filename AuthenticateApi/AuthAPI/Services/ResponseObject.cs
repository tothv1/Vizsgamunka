
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AuthAPI.Services
{
    public class ResponseObject
    {
        public string responseMessage = null!;
        public object responseObject = null!;
        public int status;


        public ResponseObject(object responseObject, string responseMessage, int statusCode)
        {
            this.responseObject = responseObject;
            this.responseMessage = responseMessage;
            this.status = statusCode;
        }

        public ResponseObject()
        {

        }

        public static object create(string v, object value, int status)
        {
            return new ResponseObject()
            {
                responseMessage = v,
                responseObject = value,
                status = status
            };
        }

        public static object create(string v, int status)
        {
            return new ResponseObject()
            {
                responseMessage = v,
                responseObject = null!,
                status = status
            };
        }
    }
}
