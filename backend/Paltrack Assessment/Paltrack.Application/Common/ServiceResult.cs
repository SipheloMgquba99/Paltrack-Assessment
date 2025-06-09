namespace Paltrack.Application.Common
{
    public class ServiceResult
    {
        public bool IsSuccess { get; }
        public string Message { get; }

        private ServiceResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public static ServiceResult Success() => new ServiceResult(true, string.Empty);
        public static ServiceResult Failure(string message) => new ServiceResult(false, message);
    }

    public class ServiceResult<T>
    {
        public bool IsSuccess { get; }
        public string Message { get; }
        public T? Data { get; }

        private ServiceResult(bool isSuccess, string message, T? data = default)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }

        public static ServiceResult<T> Success(T data) => new ServiceResult<T>(true, string.Empty, data);
        public static ServiceResult<T> Failure(string message) => new ServiceResult<T>(false, message);
    }
}
