namespace Supermarket.Models.Response
{
    /// <summary>
    /// Generic API Response wrapper
    /// </summary>
    /// <typeparam name="T">Type of data</typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Indicates if the operation was successful
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Response message
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Response data
        /// </summary>
        public T? Data { get; set; }
    }
}