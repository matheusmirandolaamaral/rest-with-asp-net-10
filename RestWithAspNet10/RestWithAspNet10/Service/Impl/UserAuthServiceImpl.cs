using RestWithAspNet10.Auth.Contract;
using RestWithAspNet10.Data.DTO.V1;
using RestWithAspNet10.Model;
using RestWithAspNet10.Repository;

namespace RestWithAspNet10.Service.Impl
{
    public class UserAuthServiceImpl : IUserAuthService
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher _passwordHasher;

        public UserAuthServiceImpl(IUserRepository repository, IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public User? FindByUsername(string username)
        {
           return _repository.FindByUserName(username);
        }


        // usado somente para recurso didatico, pois isso é considerado uma falha de segurancaa 
        public User Create(AccountCredentialsDTO dto)
        {
            if(dto == null) throw new ArgumentNullException(nameof(dto));

            var entity = new User
            {
                Username = dto.Username,
                FullName = dto.Fullname,
                Password = _passwordHasher.Hash(dto.Password),
                RefreshToken = string.Empty,
                RefreshTokenExpiryTime = null
            };
            return _repository.Create(entity);
        }

        public bool RevokeToken(string username)
        {
            var user = _repository.FindByUserName(username);
            if(user == null) return false;
            user.RefreshToken = null;
            _repository.Update(user);
            return true;
        }

        public User Update(User user)
        {
            return _repository.Update(user);
        }
    }
}
