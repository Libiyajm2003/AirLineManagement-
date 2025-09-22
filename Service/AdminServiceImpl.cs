using ConsoleAppAirLineManagement.Repository;

namespace ConsoleAppAirLineManagement.Service
{
    public class AdminServiceImpl : IAdminService
    {
        private readonly IAdminRepository _adminRepo;

        public AdminServiceImpl(IAdminRepository adminRepo)
        {
            _adminRepo = adminRepo;
        }

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
