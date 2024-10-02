using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApplication.ViewModels;
using VendasWebCore.Repositories;
using VendasWebCore.Services;
using VendasWebCore.DefaultMessages;
using VendasWebCore.Entities;
using VendasWebInfrastructure.Services.PasswordChangeNotificationService;

namespace VendasWebApplication.Commands.RetrievePasswordCommand
{
    public class RetrievePasswordCommandHandler : IRequestHandler<RetrievePasswordCommand, UserDetailedViewModel>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordChangeNotificationService _passwordChangeNotificationService;

        public RetrievePasswordCommandHandler(IAuthService authService, IUserRepository userRepository, IPasswordChangeNotificationService passwordChangeNotificationService)
        {
            _authService = authService;
            _userRepository = userRepository;
            _passwordChangeNotificationService = passwordChangeNotificationService;
        }

        public async Task<UserDetailedViewModel> Handle(RetrievePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAndRole(request.Email, request.Role.ToString());            
            UserDetailedViewModel userDetailed = new();

            if (user is not null)
            {                
                Guid newPassword = Guid.NewGuid();
                var newPasswordHash = _authService.ComputeSha256Hash(newPassword.ToString());
                await _userRepository.ChangePasswordAsync(user, user.Password, newPasswordHash);
                    
                //service mandar email
                _passwordChangeNotificationService.SendPasswordRetrieveEmailNotification(user.Email, user.FullName, newPasswordHash);

                userDetailed = new UserDetailedViewModel(user.UserId, user.FullName, user.Email, user.CreatedAt.ToString(CultureInfo.CreateSpecificCulture("pt-BR")), user.Active, user.Role);
            }
            return userDetailed;
        }
    }
}
