using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApplication.ViewModels;

namespace VendasWebApplication.Queries.GetUserByEmail
{
    public class GetUserByEmailQuery : IRequest<UserDetailedViewModel>
    {
        public string Email { get; set; }
    }
}
