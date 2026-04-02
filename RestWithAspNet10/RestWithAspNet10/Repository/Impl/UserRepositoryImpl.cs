using RestWithAspNet10.Model;
using RestWithAspNet10.Model.Context;

namespace RestWithAspNet10.Repository.Impl
{
    public class UserRepositoryImpl(MSSQLContext context) : GenericRepository<User>(context), IUserRepository
    {
        public User? FindByUserName(string userName)
        {
            return _context.Users.SingleOrDefault(u => u.Username == userName);
        }
    }
}
