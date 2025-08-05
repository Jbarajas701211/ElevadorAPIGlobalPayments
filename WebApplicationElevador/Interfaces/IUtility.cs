using WebApplicationElevador.Models;

namespace WebApplicationElevador.Interfaces
{
    public interface IUtility
    {
        Task<T?> DeserializeData<T>(ResponseApi data) where T : class;
    }
}
