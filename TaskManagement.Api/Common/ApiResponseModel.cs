namespace TaskManagement.Api.Common
{
    public class ApiResponseModel<T>
    { 

        public bool Success { get; set; }
        public string Message { get; set; } = ""; 
        public T? Data { get; set; }
    }
}
