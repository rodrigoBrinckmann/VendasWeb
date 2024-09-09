using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Enums;

namespace VendasWebApplication.Commands.CreateUserCommand
{
    public class CreateUserCommand : IRequest<int>
    {
        public string FullName { get; set; }
        public string Email { get; set; }        
        public string Password { get; set; }
        public Roles Role { get; set; }
    }
}
