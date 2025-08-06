namespace WebApplicationElevador.Models
{
    public class ResponseApi
    {
        public bool Success { get; set; }
        public List<string>? Erros { get; set; }
        public object? Data { get; set; }
    }
}
