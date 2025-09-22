namespace ConsoleAppAirLineManagement.Service
{
    public interface IAdminService
    {
        /// <summary>
        /// Validate admin login credentials
        /// </summary>
        /// <param name="username">Admin username</param>
        /// <param name="password">Admin password</param>
        /// <returns>True if valid, else false</returns>
        bool Login(string username, string password);
    }
}
