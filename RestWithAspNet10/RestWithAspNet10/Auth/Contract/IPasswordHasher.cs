using System.Reflection.Metadata;

namespace RestWithAspNet10.Auth.Contract
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string password, string hashedPassword);
    }
}
