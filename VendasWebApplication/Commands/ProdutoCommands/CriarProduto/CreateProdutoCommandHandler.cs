using MediatR;
using VendasWebCore.Entities;
using VendasWebCore.Repositories;
using VendasWebInfrastructure.AuthServices;
using VendasWebInfrastructure.Persistence.Repositories;

namespace VendasWebApplication.Commands.ProdutoCommands.CriarProduto
{
    public class CreateProdutoCommandHandler : IRequestHandler<CreateProdutoCommand, Unit>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticatedUser _user;
        public CreateProdutoCommandHandler(IProdutoRepository produtoRepository, IAuthenticatedUser user, IUserRepository userRepository)
        {
            _produtoRepository = produtoRepository;
            _userRepository = userRepository;
            _user = user;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(CreateProdutoCommand request, CancellationToken cancellationToken)
        {            
            var user = await _userRepository.GetUserByEmailAndRole(_user.Email, _user.Role);
            var produto = new Produto(request.NomeProduto, request.ValorUnitario, user.UserId);

            

            await _produtoRepository.CadastrarProdutoAsync(produto);

            return Unit.Value;
        }
    }
}
