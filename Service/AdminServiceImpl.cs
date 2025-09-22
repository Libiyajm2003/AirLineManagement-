using ConsoleAppAirLineManagement.Repository;

namespace ConsoleAppAirLineManagement.Service
{
//Implement the interface class of Admin
    public class AdminServiceImpl : IAdminService
    {
// Fields
        private readonly IAdminRepository _adminRepo;
//Constructors
        public AdminServiceImpl(IAdminRepository adminRepo)
        {
            _adminRepo = adminRepo;
        }
//Methods
        public bool Login(string username, string password)
        {
            // Optional: Add extra validation here before calling repository
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return false;

            // Call repository to check credentials
            return _adminRepo.ValidateLogin(username, password);
        }
    }
}
