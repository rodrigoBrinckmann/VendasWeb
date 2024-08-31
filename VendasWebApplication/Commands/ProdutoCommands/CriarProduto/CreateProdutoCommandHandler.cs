using MediatR;
using VendasWebCore.Entities;
using VendasWebCore.Repositories;

namespace VendasWebApplication.Commands.ProdutoCommands.CriarProduto
{
    public class CreateProdutoCommandHandler : IRequestHandler<CreateProdutoCommand, Unit>
    {
        private readonly IProdutoRepository _produtoRepository;
        public CreateProdutoCommandHandler(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<Unit> Handle(CreateProdutoCommand request, CancellationToken cancellationToken)
        {
            var produto = new Produto(request.NomeProduto, request.Valor);

            await _produtoRepository.CadastrarProdutoAsync(produto);

            return Unit.Value;
        }
    }
}
