namespace WebApplicationElevador.Interfaces
{
    public interface IHttpClientHelper
    {
        Task<T> GetTAsync<T>(string url, string token = null);
        Task<T> PostAsync<T>(string url, object data, string token = null);

    }
}
