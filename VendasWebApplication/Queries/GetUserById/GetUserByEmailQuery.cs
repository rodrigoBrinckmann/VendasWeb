﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApplication.ViewModels;

namespace VendasWebApplication.Queries.GetUserById
{
    public class GetUserByEmailQuery : IRequest<List<UserDetailedViewModel>>
    {
        public string Email { get; set; }
    }
}
