using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasWebApplication.Commands.ChangePasswordCommand;
using VendasWebApplication.Commands.CreateUserCommand;
using VendasWebCore.Entities;
using VendasWebCore.Repositories;
using VendasWebCore.Services;
using VendasWebInfrastructure.AuthServices;
using VendasWebInfrastructure.Persistence.Repositories;
using VendasWebCore.Enums;

namespace CommandsTests
{
    public class CreateUserCommandTests
    {
        private readonly Mock<IAuthService> _authServiceMock = new();
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly CreateUserCommandHandler _createUserCommandHandler;
        public CreateUserCommandTests()
        {
            _createUserCommandHandler = new CreateUserCommandHandler(_userRepositoryMock.Object, _authServiceMock.Object);
        }

        [Fact(DisplayName = "CreateUserCommandTests - CreateUserCommand OK")]
        public async void InputDataOk_Executed_ReturnId()
        {            
            //Arrange
            CreateUserCommand command = new CreateUserCommand() { Email = "email@123.com",FullName = "Fullname", Password = "Password@1234", Role = Roles.Admin };            
            _authServiceMock.Setup(h => h.ComputeSha256Hash(command.Password)).Returns("hashPassword");
            
            User user = new User("Fullname","email@123","password", Roles.Admin.ToString());
            _userRepositoryMock.Setup(r => r.CadastrarUserAsync(user));

            //Act
            var id = await _createUserCommandHandler.Handle(command, new CancellationToken());
            //Assert
            _authServiceMock.Verify(c => c.ComputeSha256Hash(It.IsAny<string>()), Times.Once);
            _userRepositoryMock.Verify(r => r.CadastrarUserAsync(It.IsAny<User>()), Times.Once());
            Assert.True(id >=0);
        }
    }
}
