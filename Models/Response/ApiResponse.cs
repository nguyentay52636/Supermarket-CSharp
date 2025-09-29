namespace Supermarket.Models.Response
{

    public class ApiResponse<T>
    {

        public bool Success { get; set; }


        public string Message { get; set; } = string.Empty;

        public T? Data { get; set; }
    }

    public static class ApiResponse
    {
        public static ApiResponse<T> Ok<T>(T? data, string message = "") => new ApiResponse<T>
        {
            Success = true,
            Message = string.IsNullOrWhiteSpace(message) ? "" : message,
            Data = data
        };

        public static ApiResponse<T> Fail<T>(string message, T? data = default) => new ApiResponse<T>
        {
            Success = false,
            Message = message,
            Data = data
        };
    }
}