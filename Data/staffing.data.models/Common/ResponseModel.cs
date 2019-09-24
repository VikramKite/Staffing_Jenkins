namespace staffing.data.models.Common
{
    public class ResponseModel
    {
        public ResponseModel()
        {
            success = true;
            message = string.Empty;
        }

        public bool success { get; set; }
        public string message { get; set; }
    }
}
