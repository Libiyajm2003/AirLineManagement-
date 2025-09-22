using ConsoleAppAirLineManagement.Model;

namespace ConsoleAppAirLineManagement.Repository
{
//Interface for Admin repository to handle data access related to Admin
    public interface IAdminRepository
    {
        //Validates the admin login credentials
        bool ValidateLogin(string username, string password);
    }
}


