using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApplication.ViewModels;
using VendasWebCore.Models;

namespace VendasWebApplication.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<PaginationResult<UserViewModel>>
    {
        public string? Query { get; set; }
        public int Page { get; set; } = 1;
    }
}
