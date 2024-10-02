using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApplication.ViewModels;
using VendasWebCore.Models;
using VendasWebCore.Repositories;

namespace VendasWebApplication.Queries.GetUserByEmail
{
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, UserDetailedViewModel>
    {
        private readonly IUserRepository _userRepository;
        public GetUserByEmailQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserDetailedViewModel> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetUserByEmail(request.Email);            
            UserDetailedViewModel userDetailedViewModel = new UserDetailedViewModel();
            if (result is not null)
            {
                userDetailedViewModel = new UserDetailedViewModel(result.UserId, result.FullName, result.Email, result.CreatedAt.ToString(CultureInfo.CreateSpecificCulture("pt-BR")), result.Active, result.Role);
            }            
            return null;            
        }
    }
}
