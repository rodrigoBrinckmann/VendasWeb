using Moq;
using VendasWebApplication.Commands.ChangePasswordCommand;
using VendasWebCore.Entities;
using VendasWebCore.Enums;
using VendasWebCore.Repositories;
using VendasWebCore.Services;
using VendasWebInfrastructure.Services.PasswordChangeNotificationService;

namespace CommandsTests
{
    public class ChangePasswordCommandTests
    {
        private readonly Mock<IAuthService> _authServiceMock = new();
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly Mock<IPasswordChangeNotificationService> _passwordChangeNotificationServiceMock = new();
        private readonly ChangePasswordCommandHandler _changePasswordCommandHandler;
        public ChangePasswordCommandTests()
        {
            _changePasswordCommandHandler = new ChangePasswordCommandHandler(_authServiceMock.Object, _userRepositoryMock.Object, _passwordChangeNotificationServiceMock.Object);
        }
        

        [Fact(DisplayName = "ChangePasswordCommandTests - ChangePasswordCommand OK")]
        public async void InputDataOk_Executed_ReturnUnitValue()
        {
            //Arrange
            ChangePasswordCommand command = new ChangePasswordCommand() { Email = "email@test.com", NewPassword = "senha@123", OldPassword = "senha@123" };
            User testUser = new User(1,"FullName","email@123.com",true, Roles.User.ToString());
            _authServiceMock.Setup(h => h.ComputeSha256Hash(command.OldPassword)).Returns("hashOld");
            _authServiceMock.Setup(h => h.ComputeSha256Hash(command.NewPassword)).Returns("hashNew");
            _userRepositoryMock.Setup(r => r.GetUserByEmailAndPasswordAndRole(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(testUser);
            _userRepositoryMock.Setup(r => r.ChangePasswordAsync(testUser, command.OldPassword, command.NewPassword));
            
            //Act
            var response = await _changePasswordCommandHandler.Handle(command, new CancellationToken());
            //Assert
            _authServiceMock.Verify(c => c.ComputeSha256Hash(It.IsAny<string>()), Times.Exactly(2));
            _userRepositoryMock.Verify(r => r.ChangePasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }    
        
    }
}
