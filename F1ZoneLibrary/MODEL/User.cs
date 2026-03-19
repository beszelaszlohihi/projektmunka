using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1ZoneLibrary.MODEL
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        
        public string PasswordHash { get; set; } = string.Empty;

        // Opcionális: Szerepkör (pl. "Admin" vagy "User")
        public string Role { get; set; } = "User";
    }
}
