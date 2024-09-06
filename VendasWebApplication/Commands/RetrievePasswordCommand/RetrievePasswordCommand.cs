using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApplication.ViewModels;

namespace VendasWebApplication.Commands.RetrievePasswordCommand
{
    public class RetrievePasswordCommand : IRequest<List<UserDetailedViewModel>>
    {
        public string Email { get; set; }
    }
}
