using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApplication.ViewModels;
using VendasWebCore.Entities;
using VendasWebCore.Repositories;
using VendasWebCore.Services;

namespace VendasWebApplication.Commands.ChangePasswordCommand
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Unit>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public ChangePasswordCommandHandler(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            //utiizar mesmo algoritmo para criar o hash dessa senha
            var passwordHash = _authService.ComputeSha256Hash(request.OldPassword);            
            var newPasswordHash = _authService.ComputeSha256Hash(request.NewPassword);

            await _userRepository.ChangePasswordAsync(request.Email, passwordHash, newPasswordHash);
            return Unit.Value;
        }
    }
}
