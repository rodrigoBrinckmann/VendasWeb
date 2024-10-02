using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApplication.ViewModels;
using VendasWebCore.Enums;

namespace VendasWebApplication.Commands.RetrievePasswordCommand
{
    public class RetrievePasswordCommand : IRequest<UserDetailedViewModel>
    {
        public string Email { get; set; }
        public Roles Role { get; set; }
    }
}
