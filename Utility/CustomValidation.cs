using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleAppAirLineManagement.Utility
{
    public class CustomValidation
    {
        #region 1 - UserName validation
        // User name should not be empty
        // User name should contain only letters, numbers, underscores, and dot
        // User name max length = 10
        public static bool IsValidUserName(string userName)
        {
            return !string.IsNullOrWhiteSpace(userName) &&
                   userName.Length <= 15 &&
                   Regex.IsMatch(userName, @"^[a-zA-Z0-9_.]+$");
        }
        #endregion

        #region 2 - Password validation
        // Password should have at least 4 characters
        // Including at least:
        //   - one uppercase letter
        //   - one lowercase letter
        //   - one digit
        //   - one special character
        public static bool IsValidPassword(string password)
        {
            return !string.IsNullOrWhiteSpace(password) &&
                   Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@\W_]).{4,}$");
        }
        #endregion

        #region 3 - Read password with * mask
        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                // Replace each keystroke with *
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                // Handle backspace
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine(); // move to next line after Enter
            return password;
        }
        #endregion
    }
}
