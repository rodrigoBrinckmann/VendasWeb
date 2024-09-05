using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApi.Controllers;
using VendasWebApplication.Commands.CreateUserCommand;
using VendasWebApplication.Commands.LoginUserCommands;
using VendasWebApplication.Queries.GetAllUsers;
using VendasWebApplication.Queries.GetProdutoById;
using VendasWebApplication.ViewModels;
using VendasWebCore.Models;

namespace ControllersTests
{
    public class UserControllerTests
    {
        private readonly Mock<IMediator> _mediatrMock = new();
        private readonly UserController userController;

        public UserControllerTests()
        {
            userController = new UserController(_mediatrMock.Object);
        }

        [Fact(DisplayName = "UserControllerTests - CadastrarUsuario")]
        public async Task CadastrarUsuario()
        {
            //arrange
            CreateUserCommand command = new();
            int id = 300;
                        
            _mediatrMock.Setup(m => m.Send<int>(It.IsAny<CreateUserCommand>(), new CancellationToken())).ReturnsAsync(id);

            //act
            var response = await userController.CadastrarUsuario(command);

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
            PaginationResult<UserViewModel> paginationViewModel = new();

            _mediatrMock.Setup(m => m.Send<PaginationResult<UserViewModel>>(It.IsAny<GetAllUsersQuery>(), new CancellationToken())).ReturnsAsync(paginationViewModel);

            //act
            var response = await userController.GetAllUsers(query);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            _mediatrMock.Verify(x => x.Send<PaginationResult<UserViewModel>>(It.IsAny<GetAllUsersQuery>(), new CancellationToken()), Times.Once());
        }

        [Fact(DisplayName = "UserControllerTests - Login")]
        public async Task Login()
        {
            //arrange
            LoginUserCommand command = new();
            LoginUserViewModel loginUserViewModel = new("email@email","token12345");

            _mediatrMock.Setup(m => m.Send<LoginUserViewModel>(It.IsAny<LoginUserCommand>(), new CancellationToken())).ReturnsAsync(loginUserViewModel);

            //act
            var response = await userController.Login(command);

            //assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
            _mediatrMock.Verify(x => x.Send<LoginUserViewModel>(It.IsAny<LoginUserCommand>(), new CancellationToken()), Times.Once());
        }


    }
}
