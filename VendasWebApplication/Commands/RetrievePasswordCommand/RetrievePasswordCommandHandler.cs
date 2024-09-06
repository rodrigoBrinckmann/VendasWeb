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

namespace VendasWebApplication.Commands.RetrievePasswordCommand
{
    public class RetrievePasswordCommandHandler : IRequestHandler<RetrievePasswordCommand, List<UserDetailedViewModel>>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public RetrievePasswordCommandHandler(IAuthService authService, IUserRepository userRepository, IEmailService emailService)
        {
            _authService = authService;
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task<List<UserDetailedViewModel>> Handle(RetrievePasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetUserByEmail(request.Email);
            List<UserDetailedViewModel> listUserDetailedViewModel = new List<UserDetailedViewModel>();

            if (result is not null)
            {
                foreach (var userAccount in result)
                {
                    Guid newPassword = Guid.NewGuid();
                    var newPasswordHash = _authService.ComputeSha256Hash(newPassword.ToString());
                    await _userRepository.ChangePasswordAsync(userAccount.Email, userAccount.Password, newPasswordHash);
                    //service mandar email
                    await _emailService.ServiceMailProcess(userAccount.Email, newPassword.ToString());
                    //
                    var userDetailedViewModel = new UserDetailedViewModel(userAccount.UserId, userAccount.FullName, userAccount.Email, userAccount.CreatedAt.ToString(CultureInfo.CreateSpecificCulture("pt-BR")), userAccount.Active, userAccount.Role);
                    listUserDetailedViewModel.Add(userDetailedViewModel);
                }
            }
            return listUserDetailedViewModel;
        }
    }
}
