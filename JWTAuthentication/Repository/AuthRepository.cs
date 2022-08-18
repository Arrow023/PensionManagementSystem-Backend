using JWTAuthentication.Repository.IRepository;
using log4net;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JWTAuthentication;
using PensionManagementSystem.Data;
using PensionManagementSystem.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuthentication.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly AppSettings _appSettings;
        private static readonly ILog _log = LogManager.GetLogger(typeof(AuthRepository));
        public AuthRepository(ApplicationDbContext db, IOptions<AppSettings> appSettings)
        {
            _db = db;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Used to authenticate a user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>user object containing token</returns>
        public User Authenticate(string username, string password)
        {
            _log.Error("test error db");
            try
            {
                var user = _db.Users.SingleOrDefault
                (x => x.Username == username &&
                      x.Password == EncryptedPasssword(password));
                if (user == null)
                {
                    _log.Error("JWT Error: Username or password is invalid");
                    return null;
                }

                //Generate JWT token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                        //new Claim(ClaimTypes.Role, user.Role)
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);
                user.Password = "";
                user.ExpiresIn = 30*60;
                return user;
            }
            catch (Exception ex)
            {
                _log.Error("JWT Error: "+ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Checks if the user is unique or an existing one
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Returns true, if the user is already present else returns false</returns>
        public bool IsUniqueUser(string username)
        {
            try
            {
                var user = _db.Users.SingleOrDefault(x => x.Username == username);
                if (user == null)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                _log.Error("JWT Error: "+ex.Message);
                return false;
            }
            
        }

        /// <summary>
        /// Function to register a new user in the database
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Returns a user object</returns>
        public User Register(string username, string password)
        {
            try
            {
                User userobj = new User()
                {
                    Username = username,
                    Password = EncryptedPasssword(password),
                };
                _db.Users.Add(userobj);
                _db.SaveChanges();
                userobj.Password = "";
                return userobj;
            }
            catch (Exception ex)
            {
                _log.Error("JWT Error: "+ex.Message);
                return null;
            }
            
        }

        /// <summary>
        /// Encrypts the password using Base-64 encoding
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns>Base-64 encrypted string</returns>
        public string EncryptedPasssword(string plainText)
        {
            var passwordStream = Encoding.UTF8.GetBytes(plainText);
            var encryptedPassword = Convert.ToBase64String(passwordStream);
            return encryptedPassword;
        }

     
    }
}
