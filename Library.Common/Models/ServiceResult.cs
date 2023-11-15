namespace Library.Common.Models
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }

        public ServiceResult(bool success = true, string? message = null)
        {
            Success = success;
            Message = message;
        }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Result { get; set; } = default;

        public ServiceResult(bool success = true, string? message = null) : base(success, message)
        {
        }
    }
}
