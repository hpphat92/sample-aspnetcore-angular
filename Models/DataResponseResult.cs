namespace NewApp.Models
{
    public class DataResponseResult<T> where T : class
    {
        public int Status { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
    }

    public class CommonWithoutDataResModel
    {
        public int Status { get; set; }
        public string Message { get; set; }
    }
}
