using ConsoleAppAirLineManagement.Model;

namespace ConsoleAppAirLineManagement.Repository
{
    public interface IAdminRepository
    {
        bool ValidateLogin(string username, string password);
    }
}


