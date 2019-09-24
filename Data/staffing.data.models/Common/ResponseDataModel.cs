namespace staffing.data.models.Common
{
    public class ResponseDataModel<T> : ResponseModel
    {
        public T data { get; set; }
    }
}
