namespace ApiPruebaTecnica.Models
{
    public class ApiResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object? Data { get; set; }
        public Dictionary<string, object> ? Metadata { get; set; } = new Dictionary<string, object>();
    }
}
