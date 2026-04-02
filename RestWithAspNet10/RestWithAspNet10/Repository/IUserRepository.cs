using RestWithAspNet10.Model;

namespace RestWithAspNet10.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        User? FindByUserName(string userName);
    }
}
