using RestWithAspNet10.Data.DTO.V1;
using RestWithAspNet10.Model;

namespace RestWithAspNet10.Service
{
    public interface IUserAuthService
    {
        User? FindByUsername(string username);
        User Create(AccountCredentialsDTO dto); // usado somente para recurso didatico
        bool RevokeToken(string username);
        User Update(User user);
    }
}
