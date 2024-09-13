using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VendasWebApi.Controllers;
using VendasWebApplication.Commands.ChangePasswordCommand;
using VendasWebApplication.Commands.CreateUserCommand;
using VendasWebApplication.Commands.LoginUserCommands;
using VendasWebApplication.Commands.RetrievePasswordCommand;
using VendasWebApplication.Commands.UpdateUserCommand;
using VendasWebApplication.Validators;

namespace ValidatorsTests
{
    public class ChangePasswordCommandValidatorTest
    {
        private readonly Mock<IMediator> _mediatrMock = new();
        private readonly Mock<IValidator<CreateUserCommand>> _createUserCommandValidator = new ();        
        private readonly Mock<IValidator<RetrievePasswordCommand>> _retrievePasswordCommandValidator = new ();
        private readonly Mock<IValidator<UpdateUserCommand>> _updateUserCommandValidator = new ();
        private readonly Mock<IValidator<LoginUserCommand>> _loginUserCommandValidator = new ();

        private readonly IValidator<ChangePasswordCommand> _changePasswordCommandValidator = new ChangePasswordCommandValidator();
        private readonly UserController _userController;

        public ChangePasswordCommandValidatorTest()
        {
            _userController = new UserController(_mediatrMock.Object, _createUserCommandValidator.Object, _changePasswordCommandValidator,
                _retrievePasswordCommandValidator.Object, _updateUserCommandValidator.Object, _loginUserCommandValidator.Object);
        }

        [Fact(DisplayName = "ChangePasswordCommandValidatorTest - Returns password NOK")]
        public async Task Password_NOK()
        {
            //arrange
            ChangePasswordCommand request = new() { Email = "email@gmail.com", NewPassword = "Senha", OldPassword = "OldSenha@123"};
            var resultDictionary = new Dictionary<string, List<string>>();
            resultDictionary.Add("NewPassword", new List<string>() { "Senha deve conter pelo menos 8 caracteres, um número, uma letra maiúscula, uma minúscula, e um caractere especial" });            
            //ValidationResult dfdsf = new() { Errors = };
            //act
            var response = await _userController.ChangePassword(request);

            //assert            
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(resultDictionary);
        }
    }
}