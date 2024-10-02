using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Enums;

namespace VendasWebApplication.ViewModels
{
    public class UserDetailedViewModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string CreatedAt { get; set; }
        public bool Active { get; set; }
        public string Role { get; set; }

        public UserDetailedViewModel(){}

        public UserDetailedViewModel(string fullName, string email)
        {            
            FullName = fullName;
            Email = email;            
        }

        public UserDetailedViewModel(int userId, string fullName, string email, string createdAt, bool active, string role)
        {
            UserId = userId;
            FullName = fullName;
            Email = email;
            CreatedAt = createdAt; //.ToString(CultureInfo.CreateSpecificCulture("pt-BR"));
            Active = active;
            Role = role;
        }
    }
}
