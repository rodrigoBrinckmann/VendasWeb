﻿using MediatR;
using VendasWebApplication.ViewModels;

namespace VendasWebApplication.Commands.LoginUserCommands
{
    public class LoginUserCommand : IRequest<LoginUserViewModel>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
