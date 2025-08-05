using System.Text.Json;
using WebApplicationElevador.Interfaces;
using WebApplicationElevador.Models;

namespace WebApplicationElevador.Utility
{
    public class Utility : IUtility
    {
        public Task<T?> DeserializeData<T>(ResponseApi data) where T : class
        {
            if (data is not null && data.Success && data.Data is not null)
            {
                var jsonElement = (JsonElement)data.Data;
                var result = JsonSerializer.Deserialize<T>(jsonElement.GetRawText());
                return Task.FromResult(result);
            }
            return Task.FromResult<T?>(default);
        }
    }
}
