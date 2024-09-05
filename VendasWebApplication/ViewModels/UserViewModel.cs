using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendasWebApplication.ViewModels
{
    public class UserViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }

        public UserViewModel(string fullName, string email)
        {
            FullName = fullName;
            Email = email;
        }
    }
}
