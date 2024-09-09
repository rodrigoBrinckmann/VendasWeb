using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApplication.ViewModels;
using VendasWebCore.Entities;
using VendasWebCore.Repositories;
using VendasWebCore.Services;

namespace VendasWebApplication.Commands.UpdateUserCommand
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDetailedViewModel>
    {
        private readonly IUserRepository _userRepository;        

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;            
        }
        public async Task<UserDetailedViewModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userEdit = new User(request.UserId, request.FullName, request.Email,request.Active,request.Role.ToString());
            var user = await  _userRepository.EditarUserAsync(userEdit);
            return new UserDetailedViewModel(user.UserId,user.FullName,user.Email,user.CreatedAt.ToString(CultureInfo.CreateSpecificCulture("pt-BR")), user.Active,user.Role.ToString());
        }
    }
}

