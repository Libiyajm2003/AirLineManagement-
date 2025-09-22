using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppAirLineManagement.Model
{
// create admin class 
    public class Admin
    {
        //fields / properties 
        //used for login
        public int AdminId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
