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
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, List<UserDetailedViewModel>>
    {
        private readonly IUserRepository _userRepository;
        public GetUserByEmailQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<List<UserDetailedViewModel>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetUserByEmail(request.Email);
            List <UserDetailedViewModel> listUserDetailedViewModel = new List <UserDetailedViewModel>();

            if (result is not null)
            {
                foreach (var item in result)
                {
                    var userDetailedViewModel = new UserDetailedViewModel(item.UserId, item.FullName, item.Email, item.CreatedAt.ToString(CultureInfo.CreateSpecificCulture("pt-BR")), item.Active, item.Role);
                    listUserDetailedViewModel.Add(userDetailedViewModel);
                }
                return listUserDetailedViewModel;
            }
            return null;            
        }
    }
}
