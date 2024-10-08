﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApplication.ViewModels;
using VendasWebCore.Models;
using VendasWebCore.Repositories;

namespace VendasWebApplication.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, PaginationResult<UserDetailedViewModel>>
    {
        private readonly IUserRepository _userRepository;
        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<PaginationResult<UserDetailedViewModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var paginatedResults = await _userRepository.GetListOfUsers(request.Query, request.Page);
            
            var userViewModel = paginatedResults
                .Data
                .Select(u => new UserDetailedViewModel(u.UserId,u.FullName,u.Email,u.CreatedAt.ToString(CultureInfo.CreateSpecificCulture("pt-BR")), u.Active,u.Role))
                .ToList();

            var paginationResult = new PaginationResult<UserDetailedViewModel>(
               paginatedResults.Page,
               paginatedResults.TotalPages,
               paginatedResults.PageSize,
               paginatedResults.ItemsCount,
               userViewModel
               );
            return paginationResult;
        }
    }
}
