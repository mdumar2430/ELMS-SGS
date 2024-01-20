using ELMS_API.Data;
using ELMS_API.Interfaces;
using ELMS_API.Models;
using System.Text;

namespace ELMS_API.Services
{
    public class LoginService : ILoginService
    {
        private readonly IAppDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public LoginService(IAppDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        private string ValidateUser(string email, string pswd)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pswd))
            {
                return "User field is null";
            }

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                if (addr.Address == email)
                {
                    return "Valid user";
                }
                else
                {
                    return "Invalid user";
                }
            }
            catch (Exception e)
            {
                return "Invalid email format";
            }


        }

        public Employee Login(string emailid, string password)
        {

            string result = ValidateUser(emailid, password);
            if (result == "Valid user")
            {
                byte[] data = Convert.FromBase64String(password);
                string decodedPassword = Encoding.UTF8.GetString(data);
                var user = _context.Employees.FirstOrDefault(x => emailid.Equals(x.Email));
                if (user != null)
                {
                    if (_passwordHasher.VerifyPassword(user.Password, decodedPassword))
                    {
                        return user;
                    }

                }
            }
            return null;
        }
    }
}
