using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Repositories;

namespace VendasWebApplication.Commands.ProdutoCommands.DeletarProduto
{
    public class DeleteProdutoCommandHandler : IRequestHandler<DeleteProdutoCommand, Unit>
    {
        private readonly IProdutoRepository _produtoRepository;
        public DeleteProdutoCommandHandler(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }
        public async Task<Unit> Handle(DeleteProdutoCommand request, CancellationToken cancellationToken)
        {
            await _produtoRepository.DeletarProduto(request.Id);
            return Unit.Value;
        }
    }
}
