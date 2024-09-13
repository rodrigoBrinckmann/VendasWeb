using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VendasWebApi.Controllers;
using VendasWebApplication.Commands.ChangePasswordCommand;
using VendasWebApplication.Commands.CreateUserCommand;
using VendasWebApplication.Commands.LoginUserCommands;
using VendasWebApplication.Commands.UpdateUserCommand;
using VendasWebApplication.Queries.GetAllUsers;
using VendasWebApplication.ViewModels;
using VendasWebCore.Models;
using VendasWebApplication.Queries.GetUserByEmail;
using VendasWebApplication.Commands.RetrievePasswordCommand;
using Microsoft.AspNetCore.Http.HttpResults;
using VendasWebApplication.Commands.ProdutoCommands.UpdateProduto;
using FluentValidation;
using FluentValidation.Results;

namespace ControllersTests
{
    public class UserControllerTests
    {
        private readonly Mock<IMediator> _mediatrMock = new();
        private readonly Mock<IValidator<CreateUserCommand>> _createUserCommandValidator = new();
        private readonly Mock<IValidator<ChangePasswordCommand>> _changePasswordCommandValidator = new();
        private readonly Mock<IValidator<RetrievePasswordCommand>> _retrievePasswordCommandValidator = new();
        private readonly Mock<IValidator<UpdateUserCommand>> _updateUserCommandValidator = new();
        private readonly Mock<IValidator<LoginUserCommand>> _loginUserCommandValidator = new();

        private readonly UserController _userController;


        public UserControllerTests()
        {
            _userController = new UserController(_mediatrMock.Object, _createUserCommandValidator.Object, _changePasswordCommandValidator.Object,
                _retrievePasswordCommandValidator.Object, _updateUserCommandValidator.Object, _loginUserCommandValidator.Object);
        }

        [Fact(DisplayName = "UserControllerTests - CadastrarUsuario")]
        public async Task CadastrarUsuario()
        {
            //arrange
            CreateUserCommand command = new();
            int id = 300;
            ValidationResult vr = new();
            _createUserCommandValidator.Setup(v => v.ValidateAsync(command, new CancellationToken())).ReturnsAsync(vr);
            _mediatrMock.Setup(m => m.Send<int>(It.IsAny<CreateUserCommand>(), new CancellationToken())).ReturnsAsync(id);

            //act
            var response = await _userController.CreateUser(command);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            _mediatrMock.Verify(x => x.Send<int>(It.IsAny<CreateUserCommand>(), new CancellationToken()), Times.Once());
        }

        [Fact(DisplayName = "UserControllerTests - GetAllUsers")]
        public async Task GetAllUsers()
        {
            //arrange
            GetAllUsersQuery query = new();
            PaginationResult<UserDetailedViewModel> paginationViewModel = new PaginationResult<UserDetailedViewModel> () { ItemsCount = 1};

            _mediatrMock.Setup(m => m.Send<PaginationResult<UserDetailedViewModel>>(It.IsAny<GetAllUsersQuery>(), new CancellationToken())).ReturnsAsync(paginationViewModel);

            //act
            var response = await _userController.GetAllUsers(query);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            _mediatrMock.Verify(x => x.Send<PaginationResult<UserDetailedViewModel>>(It.IsAny<GetAllUsersQuery>(), new CancellationToken()), Times.Once());
        }

        [Fact(DisplayName = "UserControllerTests - GetAllUsers - No Users Found")]
        public async Task GetAllUsers_NoUsersFound()
        {
            //arrange
            GetAllUsersQuery query = new();
            PaginationResult<UserDetailedViewModel> paginationViewModel = new PaginationResult<UserDetailedViewModel>() { ItemsCount = 0};

            _mediatrMock.Setup(m => m.Send<PaginationResult<UserDetailedViewModel>>(It.IsAny<GetAllUsersQuery>(), new CancellationToken())).ReturnsAsync(paginationViewModel);

            //act
            var response = await _userController.GetAllUsers(query);

            //assert
            var result = response.Should().BeOfType<NotFoundResult>().Subject;            
            _mediatrMock.Verify(x => x.Send<PaginationResult<UserDetailedViewModel>>(It.IsAny<GetAllUsersQuery>(), new CancellationToken()), Times.Once());
        }

        [Fact(DisplayName = "UserControllerTests - Login")]
        public async Task Login()
        {
            //arrange
            LoginUserCommand command = new();
            LoginUserViewModel loginUserViewModel = new("email@email","token12345");
            ValidationResult vr = new();
            _loginUserCommandValidator.Setup(v => v.ValidateAsync(command, new CancellationToken())).ReturnsAsync(vr);

            _mediatrMock.Setup(m => m.Send<LoginUserViewModel>(It.IsAny<LoginUserCommand>(), new CancellationToken())).ReturnsAsync(loginUserViewModel);

            //act
            var response = await _userController.Login(command);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            _mediatrMock.Verify(x => x.Send<LoginUserViewModel>(It.IsAny<LoginUserCommand>(), new CancellationToken()), Times.Once());
        }

        [Fact(DisplayName = "UserControllerTests - Failed Login - User not found")]
        public async Task FailedLogin_UserNotFound()
        {
            //arrange
            LoginUserCommand command = new();
            ValidationResult vr = new();
            _loginUserCommandValidator.Setup(v => v.ValidateAsync(command, new CancellationToken())).ReturnsAsync(vr);

            _mediatrMock.Setup(m => m.Send<LoginUserViewModel>(It.IsAny<LoginUserCommand>(), new CancellationToken())).ReturnsAsync((LoginUserViewModel)null);

            //act
            var response = await _userController.Login(command);

            //assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().Be("Usuário não encontrado");
            _mediatrMock.Verify(x => x.Send<LoginUserViewModel>(It.IsAny<LoginUserCommand>(), new CancellationToken()), Times.Once());
        }        

        [Fact(DisplayName = "UserControllerTests - GetUsersByEmail")]
        public async Task GetUsersByEmail()
        {
            //arrange
            GetUserByEmailQuery query = new();
            List<UserDetailedViewModel> userDetailedViewModelList = new();

            _mediatrMock.Setup(m => m.Send<List<UserDetailedViewModel>>(It.IsAny<GetUserByEmailQuery>(), new CancellationToken())).ReturnsAsync(userDetailedViewModelList);

            //act
            var response = await _userController.GetUsersByEmail(query);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            _mediatrMock.Verify(x => x.Send<List<UserDetailedViewModel>>(It.IsAny<GetUserByEmailQuery>(), new CancellationToken()), Times.Once());
        }

        [Fact(DisplayName = "UserControllerTests - GetUsersByEmail - User Not found")]
        public async Task GetUsersByEmail_UserNotFound()
        {
            //arrange
            GetUserByEmailQuery query = new();
            List<UserDetailedViewModel> userDetailedViewModelList = null;

            _mediatrMock.Setup(m => m.Send<List<UserDetailedViewModel>>(It.IsAny<GetUserByEmailQuery>(), new CancellationToken())).ReturnsAsync((List<UserDetailedViewModel>)null);
            
            //act
            var response = await _userController.GetUsersByEmail(query);

            //assert
            var result = response.Should().BeOfType<NotFoundObjectResult>().Subject;
            result.Value.Should().Be("Usuário não encontrado");
            _mediatrMock.Verify(x => x.Send<List<UserDetailedViewModel>>(It.IsAny<GetUserByEmailQuery>(), new CancellationToken()), Times.Once());
        }

        [Fact(DisplayName = "UserControllerTests - UpdateUser")]
        public async Task UpdateUser()
        {
            //arrange
            UpdateUserCommand command = new();
            UserDetailedViewModel userDetailedViewModelList = new("Name","email");
            ValidationResult vr = new();
            _updateUserCommandValidator.Setup(v => v.ValidateAsync(command, new CancellationToken())).ReturnsAsync(vr);

            _mediatrMock.Setup(m => m.Send<UserDetailedViewModel>(It.IsAny<UpdateUserCommand>(), new CancellationToken())).ReturnsAsync(userDetailedViewModelList);

            //act
            var response = await _userController.UpdateUser(command);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            _mediatrMock.Verify(x => x.Send<UserDetailedViewModel>(It.IsAny<UpdateUserCommand>(), new CancellationToken()), Times.Once());
        }

        [Fact(DisplayName = "UserControllerTests - UpdateUser - catch scenario")]
        public async Task UpdateUser_catch_scenario()
        {            
            //arrange
            var expectedException = new KeyNotFoundException("Usuário inexistente na base de dados");
            UpdateUserCommand command = new();
            ValidationResult vr = new();
            _updateUserCommandValidator.Setup(v => v.ValidateAsync(command, new CancellationToken())).ReturnsAsync(vr);

            _mediatrMock.Setup(m => m.Send<UserDetailedViewModel>(It.IsAny<UpdateUserCommand>(), new CancellationToken())).ThrowsAsync(expectedException);

            //act
            var response = await _userController.UpdateUser(command);

            //assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;            
            _mediatrMock.Verify(x => x.Send<UserDetailedViewModel>(It.IsAny<UpdateUserCommand>(), new CancellationToken()), Times.Once());
        }


        [Fact(DisplayName = "UserControllerTests - ChangePassword")]
        public async Task ChangePassword()
        {
            //arrange
            ChangePasswordCommand command = new();
            ValidationResult vr = new();
            _changePasswordCommandValidator.Setup(v => v.ValidateAsync(command, new CancellationToken())).ReturnsAsync(vr);

            _mediatrMock.Setup(m => m.Send<Unit>(It.IsAny<ChangePasswordCommand>(), new CancellationToken()));

            //act
            var response = await _userController.ChangePassword(command);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            _mediatrMock.Verify(x => x.Send<Unit>(It.IsAny<ChangePasswordCommand>(), new CancellationToken()), Times.Once());
        }

        [Fact(DisplayName = "UserControllerTests - ChangePassword - User not found")]
        public async Task ChangePassword_User_Not_Found()
        {
            //arrange
            ChangePasswordCommand command = new();
            var expectedException = new KeyNotFoundException("User not found");
            _mediatrMock.Setup(m => m.Send<Unit>(It.IsAny<ChangePasswordCommand>(), new CancellationToken())).ThrowsAsync(expectedException);
            ValidationResult vr = new();
            _changePasswordCommandValidator.Setup(v => v.ValidateAsync(command, new CancellationToken())).ReturnsAsync(vr);

            //act
            var response = await _userController.ChangePassword(command);

            //assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            _mediatrMock.Verify(x => x.Send<Unit>(It.IsAny<ChangePasswordCommand>(), new CancellationToken()), Times.Once());
        }
        

        [Fact(DisplayName = "UserControllerTests - RetrievePassword")]
        public async Task RetrievePassword()
        {
            //arrange
            RetrievePasswordCommand command = new();
            List<UserDetailedViewModel> userList = [new UserDetailedViewModel("TESTE","email")];
            ValidationResult vr = new();
            _retrievePasswordCommandValidator.Setup(v => v.ValidateAsync(command, new CancellationToken())).ReturnsAsync(vr);

            _mediatrMock.Setup(m => m.Send<List<UserDetailedViewModel>>(It.IsAny<RetrievePasswordCommand>(), new CancellationToken())).ReturnsAsync(userList);

            //act
            var response = await _userController.RetrievePassword(command);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            _mediatrMock.Verify(x => x.Send<List<UserDetailedViewModel>>(It.IsAny<RetrievePasswordCommand>(), new CancellationToken()), Times.Once());
        }

        [Fact(DisplayName = "UserControllerTests - RetrievePassword - User Not Found")]
        public async Task RetrievePassword_User_Not_Found()
        {
            //arrange
            RetrievePasswordCommand command = new();
            var expectedException = new KeyNotFoundException("Usuário inexistente na base de dados");
            ValidationResult vr = new();
            _retrievePasswordCommandValidator.Setup(v => v.ValidateAsync(command, new CancellationToken())).ReturnsAsync(vr);

            _mediatrMock.Setup(m => m.Send<List<UserDetailedViewModel>>(It.IsAny<RetrievePasswordCommand>(), new CancellationToken())).ThrowsAsync(expectedException);

            //act
            var response = await _userController.RetrievePassword(command);

            //assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().Be("Usuário inexistente na base de dados");
            _mediatrMock.Verify(x => x.Send<List<UserDetailedViewModel>>(It.IsAny<RetrievePasswordCommand>(), new CancellationToken()), Times.Once());
        }
    }
}
