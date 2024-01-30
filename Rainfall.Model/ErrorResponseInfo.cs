namespace Rainfall.Model
{
    public class Error
    {
        public string Message { get; set; } = string.Empty;
        public List<ErrorDetail>? Detail { get; set; }
    }
}
