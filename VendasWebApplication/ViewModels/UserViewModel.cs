using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendasWebApplication.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string CreatedAt { get; set; }
        public bool Active { get; set; }
        public string Role { get; set; }

        public UserViewModel(int userId, string fullName, string email, string createdAt, bool active, string role)
        {
            UserId = userId;
            FullName = fullName;
            Email = email;
            CreatedAt = createdAt; // .ToString(CultureInfo.CreateSpecificCulture("pt-BR"))
            Active = active;
            Role = role;
        }        
    }
}
