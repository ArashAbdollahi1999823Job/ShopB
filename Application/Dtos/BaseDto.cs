using System.Collections.Generic;

namespace Application.Dtos
{
    public class BaseDto<T>
    {
        public T Data { get; private set; }
        public List<string> Message { get; private set; }
        public bool IsSuccess { get; private set; }


        public BaseDto(T data, List<string> message, bool isSuccess)
        {
            Data = data;
            Message = message;
            IsSuccess = isSuccess;
        }
    }
    public class BaseDto
    {
        public List<string> Message { get; private set; }
        public bool IsSuccess { get; private set; }


        public BaseDto(List<string> message, bool isSuccess)
        {
            Message = message;
            IsSuccess = isSuccess;
        }
    }
}
