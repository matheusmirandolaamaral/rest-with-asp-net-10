using RestWithAspNet10.Data.DTO.V1;

namespace RestWithAspNet10.Service
{
    public interface ILoginService
    {
        TokenDTO? ValidateCredentials(UserDTO user);
        TokenDTO? ValidateCredentials(TokenDTO token);
        bool RevokeToken(string username);
        AccountCredentialsDTO Create(AccountCredentialsDTO user);
    }
}
