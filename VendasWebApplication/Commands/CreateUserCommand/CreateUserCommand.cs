using MediatR;
using VendasWebCore.Enums;

namespace VendasWebApplication.Commands.CreateUserCommand
{
    public class CreateUserCommand : IRequest<int>
    {
        public string FullName { get; set; }
        public string Email { get; set; }        
        public string Password { get; set; }
        public Roles Role { get; set; }
    }
}
