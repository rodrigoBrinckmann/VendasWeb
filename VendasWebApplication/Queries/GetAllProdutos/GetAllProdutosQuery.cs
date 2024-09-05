﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Entities;
using VendasWebCore.Models;
using VendasWebApplication.ViewModels;

namespace VendasWebApplication.Queries.GetAllProdutos
{
    public class GetAllProdutosQuery :IRequest<PaginationResult<ProdutoViewModel>>
    {
        public string? Query { get; set; }
        public int Page { get; set; } = 1;
    }
}
