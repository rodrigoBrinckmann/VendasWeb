using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
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
    public class RetrievePasswordCommandValidatorTests
    {
        private readonly Mock<IMediator> _mediatrMock = new();
        private readonly Mock<IValidator<CreateUserCommand>> _createUserCommandValidator = new ();
        private readonly Mock<IValidator<ChangePasswordCommand>> _changePasswordCommandValidator = new();
        private readonly IValidator<RetrievePasswordCommand> _retrievePasswordCommandValidator = new RetrievePasswordCommandValidator();
        private readonly Mock<IValidator<UpdateUserCommand>> _updateUserCommandValidator = new();
        private readonly Mock<IValidator<LoginUserCommand>> _loginUserCommandValidator = new();
        private readonly UserController _userController;

        public RetrievePasswordCommandValidatorTests()
        {
            _userController = new UserController(_mediatrMock.Object, _createUserCommandValidator.Object, _changePasswordCommandValidator.Object,
                _retrievePasswordCommandValidator, _updateUserCommandValidator.Object, _loginUserCommandValidator.Object);
        }

        [Fact(DisplayName = "RetrievePasswordCommandValidatorTests - Empty FullName Validator")]
        public async Task FullName_NOK()
        {
            //arrange
            RetrievePasswordCommand request = new() { Email = "Email" };
            var resultDictionary = new Dictionary<string, List<string>>();
            resultDictionary.Add("Email", new List<string>() { "E-mail não válido!" });

            //act
            var response = await _userController.RetrievePassword(request);

            //assert            
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(resultDictionary);
        }
    }
}
