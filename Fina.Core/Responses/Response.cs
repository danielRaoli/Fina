using System.Text.Json.Serialization;

namespace Fina.Core.Response
{
    public  class Response<T>
    {
        private int _code = Configuration.DefaultStatusCode;

        public T Data { get; set; }
        public string Message { get; set; }

        [JsonIgnore]
        public bool IsSucces => _code >= 200 && _code <= 299;

        [JsonConstructor]
        public Response()
        {
            _code = Configuration.DefaultStatusCode;    
        }

        public Response(T? data, int code = Configuration.DefaultStatusCode, string? message = null)
        {
            Data = data;
            _code = code;
            Message = message;
        }
    }
}
