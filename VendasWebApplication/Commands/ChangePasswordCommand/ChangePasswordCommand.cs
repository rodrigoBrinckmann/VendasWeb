using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApplication.ViewModels;

namespace VendasWebApplication.Commands.ChangePasswordCommand
{
    public class ChangePasswordCommand : IRequest<Unit>
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
