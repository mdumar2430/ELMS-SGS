using ELMS_API.Models;

namespace ELMS_API.Interfaces
{
    public interface ILoginService
    {
        Employee Login(string username, string password);
    }
}
