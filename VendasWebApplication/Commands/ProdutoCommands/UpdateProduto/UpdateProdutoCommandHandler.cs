using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Entities;
using VendasWebCore.Repositories;

namespace VendasWebApplication.Commands.ProdutoCommands.UpdateProduto
{
    public class UpdateProdutoCommandHandler : IRequestHandler<UpdateProdutoCommand, Unit>
    {
        private readonly IProdutoRepository _produtoRepository;
        public UpdateProdutoCommandHandler(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }
        public async Task<Unit> Handle(UpdateProdutoCommand request, CancellationToken cancellationToken)
        {
            var produtoNovo = new Produto(request.NomeProduto, request.Valor);
            await _produtoRepository.EditarProdutoAsync(request.Id, produtoNovo);
            return Unit.Value;
        }
    }
}
