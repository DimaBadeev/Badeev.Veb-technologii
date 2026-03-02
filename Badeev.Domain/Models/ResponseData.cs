namespace Badeev.Domain.Models
{
    public class ResponseData<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string? ErrorMessage { get; set; } = string.Empty;

        public static ResponseData<T> OK(T? data)
        {
            return new ResponseData<T> { Data = data };
        }

        public static ResponseData<T> Error(string message)
        {
            return new ResponseData<T>
            {
                ErrorMessage = message,
                Success = false
            };
        }
    }
}