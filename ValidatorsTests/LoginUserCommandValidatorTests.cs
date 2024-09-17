using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApi.Controllers;
using VendasWebApplication.Commands.ChangePasswordCommand;
using VendasWebApplication.Commands.CreateUserCommand;
using VendasWebApplication.Commands.LoginUserCommands;
using VendasWebApplication.Commands.RetrievePasswordCommand;
using VendasWebApplication.Commands.UpdateUserCommand;
using VendasWebApplication.Validators;

namespace ValidatorsTests
{
    public class LoginUserCommandValidatorTests
    {
        private readonly Mock<IMediator> _mediatrMock = new();
        private readonly Mock<IValidator<CreateUserCommand>> _createUserCommandValidator = new();
        private readonly Mock<IValidator<ChangePasswordCommand>> _changePasswordCommandValidator = new();
        private readonly Mock<IValidator<RetrievePasswordCommand>> _retrievePasswordCommandValidator = new();
        private readonly Mock<IValidator<UpdateUserCommand>> _updateUserCommandValidator = new();
        private readonly IValidator<LoginUserCommand> _loginUserCommandValidator = new LoginUserCommandValidator();
        private readonly UserController _userController;

        public LoginUserCommandValidatorTests()
        {
            _userController = new UserController(_mediatrMock.Object, _createUserCommandValidator.Object, _changePasswordCommandValidator.Object,
                _retrievePasswordCommandValidator.Object, _updateUserCommandValidator.Object, _loginUserCommandValidator);
        }

        [Fact(DisplayName = "LoginUserCommandValidatorTests - Incorrect Email")]
        public async Task Email_NOK()
        {
            //arrange
            LoginUserCommand request = new() { Email = "email.com" };
            var resultDictionary = new Dictionary<string, List<string>>();
            resultDictionary.Add("Email", new List<string>() { "E-mail não válido!" });

            //act
            var response = await _userController.Login(request);

            //assert            
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(resultDictionary);
        }
    }
}
