using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApplication.ViewModels;
using VendasWebCore.Enums;

namespace VendasWebApplication.Commands.UpdateUserCommand
{
    public class UpdateUserCommand : IRequest<UserDetailedViewModel>
    {
        public int UserId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public bool Active { get; set; } = true;
        public Roles Role { get; set; }
    }
}
