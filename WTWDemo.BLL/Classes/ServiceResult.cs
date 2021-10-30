using Demo.Bll.Classes;

namespace Demo.Bll.Enums
{
    public class ServiceResult<T>
    {
        public T Payload { get; set; }
        public ServiceResultStatus Status { get; set; }
        public string[] Errors { get; set; }
    }
}
