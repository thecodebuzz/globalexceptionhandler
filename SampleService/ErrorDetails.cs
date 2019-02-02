using Newtonsoft.Json;

namespace SampleService
{
    internal class ErrorDetails
    {
        public ErrorDetails()
        {
        }

        public int StatusCodeContext { get; internal set; }
        public string Message { get; internal set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}