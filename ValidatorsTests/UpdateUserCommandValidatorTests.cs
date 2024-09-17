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
    public class UpdateUserCommandValidatorTests
    {
        private readonly Mock<IMediator> _mediatrMock = new();
        private readonly Mock<IValidator<CreateUserCommand>> _createUserCommandValidator = new ();
        private readonly Mock<IValidator<ChangePasswordCommand>> _changePasswordCommandValidator = new();
        private readonly Mock<IValidator<RetrievePasswordCommand>> _retrievePasswordCommandValidator = new();
        private readonly IValidator<UpdateUserCommand> _updateUserCommandValidator = new UpdateUserCommandValidator();
        private readonly Mock<IValidator<LoginUserCommand>> _loginUserCommandValidator = new();
        private readonly UserController _userController;

        public UpdateUserCommandValidatorTests()
        {
            _userController = new UserController(_mediatrMock.Object, _createUserCommandValidator.Object, _changePasswordCommandValidator.Object,
                _retrievePasswordCommandValidator.Object, _updateUserCommandValidator, _loginUserCommandValidator.Object);
        }

        [Fact(DisplayName = "CreateUserCommandValidatorTests - FullName  acima do limite Validator")]
        public async Task FullName_excede_limite_NOK()
        {
            //arrange
            UpdateUserCommand request = new() { Email = "email@gmail.com", FullName = "Fullname12 Fullname34 Fullname56 Fullname78 Fullname910 Fullname00", Role = VendasWebCore.Enums.Roles.User };
            var resultDictionary = new Dictionary<string, List<string>>();
            resultDictionary.Add("FullName", new List<string>() { "Nome cliente não pode exceder 60 posições" });

            //act
            var response = await _userController.UpdateUser(request);

            //assert            
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(resultDictionary);
        }

        [Fact(DisplayName = "CreateUserCommandValidatorTests - Email Inválido")]
        public async Task EmailInválido_NOK()
        {
            //arrange
            UpdateUserCommand request = new() { Email = "emailgmail.com", FullName = "Test", Role = VendasWebCore.Enums.Roles.User };
            var resultDictionary = new Dictionary<string, List<string>>();
            resultDictionary.Add("Email", new List<string>() { "E-mail não válido!" });

            //act
            var response = await _userController.UpdateUser(request);

            //assert            
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(resultDictionary);
        }

        [Fact(DisplayName = "CreateUserCommandValidatorTests - Email muito longo")]
        public async Task Email_longo_NOK()
        {
            //arrange
            UpdateUserCommand request = new() { Email = "email123456789email123456789email123456789email123456789@gmail.com", FullName = "", Role = VendasWebCore.Enums.Roles.User };
            var resultDictionary = new Dictionary<string, List<string>>();
            resultDictionary.Add("Email", new List<string>() { "Email cliente não pode exceder 60 posições" });

            //act
            var response = await _userController.UpdateUser(request);

            //assert            
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(resultDictionary);
        }
    }
}
