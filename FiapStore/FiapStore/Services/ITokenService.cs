using FiapStore.Entities;

namespace FiapStore.Services
{
    public interface ITokenService
    {
        string GerarToken(Usuario usuario);
    }
}
