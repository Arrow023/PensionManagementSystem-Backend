using PensionManagementSystem.Models;

namespace JWTAuthentication.Repository.IRepository
{
    public interface IAuthRepository 
    {
        bool IsUniqueUser(string username);
        User Authenticate(string username, string password);
        User Register(string username, string password);
        string EncryptedPasssword(string plainText);
    }
}
