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
    public class CreateUserCommandValidatorTests
    {
        private readonly Mock<IMediator> _mediatrMock = new();
        private readonly IValidator<CreateUserCommand> _createUserCommandValidator = new CreateUserCommandValidator();
        private readonly Mock<IValidator<ChangePasswordCommand>> _changePasswordCommandValidator = new();
        private readonly Mock<IValidator<RetrievePasswordCommand>> _retrievePasswordCommandValidator = new();
        private readonly Mock<IValidator<UpdateUserCommand>> _updateUserCommandValidator = new();
        private readonly Mock<IValidator<LoginUserCommand>> _loginUserCommandValidator = new();
        private readonly UserController _userController;

        public CreateUserCommandValidatorTests()
        {
            _userController = new UserController(_mediatrMock.Object, _createUserCommandValidator, _changePasswordCommandValidator.Object,
                _retrievePasswordCommandValidator.Object, _updateUserCommandValidator.Object, _loginUserCommandValidator.Object);
        }

        [Fact(DisplayName = "CreateUserCommandValidatorTests - Empty FullName Validator")]
        public async Task FullName_NOK()
        {
            //arrange
            CreateUserCommand request = new() { Email = "email@gmail.com", FullName = "", Password = "Senha@123", Role = VendasWebCore.Enums.Roles.User };
            var resultDictionary = new Dictionary<string, List<string>>();
            resultDictionary.Add("FullName", new List<string>() { "Nome é obrigatório!" });
            
            //act
            var response = await _userController.CreateUser(request);

            //assert            
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(resultDictionary);
        }

        [Fact(DisplayName = "CreateUserCommandValidatorTests - Oversized FullName Validator")]
        public async Task Oversized_fullname_NOK()
        {
            //arrange
            CreateUserCommand request = new() { Email = "email@gmail.com", FullName = "Nome muito grande para caber no campo maior que 60 posiçoes - erro", Password = "Senha@123", Role = VendasWebCore.Enums.Roles.User };
            var resultDictionary = new Dictionary<string, List<string>>();
            resultDictionary.Add("FullName", new List<string>() { "Nome cliente não pode exceder 60 posições" });

            //act
            var response = await _userController.CreateUser(request);

            //assert            
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(resultDictionary);
        }

        [Fact(DisplayName = "CreateUserCommandValidatorTests - Empty Email Validator")]
        public async Task Email_NOK()
        {
            //arrange
            CreateUserCommand request = new() { Email = "", FullName = "Fullname 123", Password = "Senha@123", Role = VendasWebCore.Enums.Roles.User };
            var resultDictionary = new Dictionary<string, List<string>>();
            resultDictionary.Add("Email", new List<string>() { "Email do usuário é obrigatório", "E-mail não válido!" });
            
            //act
            var response = await _userController.CreateUser(request);

            //assert            
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(resultDictionary);
        }

        [Fact(DisplayName = "CreateUserCommandValidatorTests - Oversized Email Validator")]
        public async Task Oversized_email_NOK()
        {
            //arrange
            CreateUserCommand request = new() { Email = "Emailmuitograndeparacabernocampomaiorque60posiçoes-erro@emailerrado.com", FullName = "Fullname 123", Password = "Senha@123", Role = VendasWebCore.Enums.Roles.User };
            var resultDictionary = new Dictionary<string, List<string>>();
            resultDictionary.Add("Email", new List<string>() { "Email cliente não pode exceder 60 posições" });

            //act
            var response = await _userController.CreateUser(request);

            //assert            
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(resultDictionary);
        }

        [Fact(DisplayName = "CreateUserCommandValidatorTests - Password Validator")]
        public async Task Password_NOK()
        {
            //arrange
            CreateUserCommand request = new() { Email = "email@email.com", FullName = "Fullname 123", Password = "123", Role = VendasWebCore.Enums.Roles.User };
            var resultDictionary = new Dictionary<string, List<string>>();
            resultDictionary.Add("Password", new List<string>() { "Senha deve conter pelo menos 8 caracteres, um número, uma letra maiúscula, uma minúscula, e um caractere especial" });

            //act
            var response = await _userController.CreateUser(request);

            //assert            
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(resultDictionary);
        }

    }
}
