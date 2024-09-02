﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Entities;
using VendasWebCore.Models;
using VendasWebCore.ViewModels;

namespace VendasWebApplication.Queries.GetAllItensPedidos
{
    public class GetAllItensPedidosQuery : IRequest<PaginationResult<ItensPedido>>
    {
        public string? Query { get; set; }
        public int Page { get; set; } = 1;
    }
}
