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
using VendasWebInfrastructure.Services.PasswordChangeNotificationService;

namespace VendasWebApplication.Commands.ChangePasswordCommand
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Unit>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordChangeNotificationService _passwordChangeNotificationService;

        public ChangePasswordCommandHandler(IAuthService authService, IUserRepository userRepository, IPasswordChangeNotificationService passwordChangeNotificationService)
        {
            _authService = authService;
            _userRepository = userRepository;
            _passwordChangeNotificationService = passwordChangeNotificationService;
        }

        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //utiizar mesmo algoritmo para criar o hash dessa senha
                var passwordHash = _authService.ComputeSha256Hash(request.OldPassword);
                var newPasswordHash = _authService.ComputeSha256Hash(request.NewPassword);
                var user = await _userRepository.GetUserByEmailAndPasswordAndRole(request.Email, passwordHash, request.Role.ToString());                
                if (user is not null)
                {
                    await _userRepository.ChangePasswordAsync(user, passwordHash, newPasswordHash);
                    _passwordChangeNotificationService.SendPasswordChangeEmailNotification(user.Email, user.FullName);
                    return Unit.Value;
                }
                throw new KeyNotFoundException();
            }
            catch (KeyNotFoundException e)
            {
                throw new KeyNotFoundException("Usuário inválido - Não atualizado", e);
            }

        }
    }
}
