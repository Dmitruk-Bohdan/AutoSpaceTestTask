using System.Net;

namespace AutoSpaceTestTask.Application.Models
{
    public class OperationResult
    {
        public OperationResult() { }
        public string? ErrorMessage { get; set; }
        public bool IsSucceess => ErrorMessage == null;
    }
    public class OperationResult<T> : OperationResult
    {
        public OperationResult()
        {
        }
        public OperationResult(T payload)
        {
            Payload = payload;
        }
        public T? Payload { get; set; }
    }
}
