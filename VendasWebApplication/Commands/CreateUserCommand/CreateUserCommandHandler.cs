using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebCore.Entities;
using VendasWebCore.Repositories;
using VendasWebCore.Services;

namespace VendasWebApplication.Commands.CreateUserCommand
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public CreateUserCommandHandler(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmail(request.Email);
            if (user != null)
            {
                throw new Exception("User email already exists. Please contact support to change your role if necessary, or try to retrieve your password.");
            }
            var passwordHash = _authService.ComputeSha256Hash(request.Password);            
            var newUser = new User(request.FullName,request.Email, passwordHash, request.Role.ToString());

            await _userRepository.CadastrarUserAsync(newUser);
            return newUser.UserId;
        }
    }
}
