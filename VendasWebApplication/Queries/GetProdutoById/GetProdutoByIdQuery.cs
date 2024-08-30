using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Models;
using VendasWebCore.ViewModels;

namespace VendasWebApplication.Queries.GetProdutoById
{
    public class GetProdutoByIdQuery : IRequest<ProdutoViewModel>
    {        
        public int Id { get; set; }
    }
}
