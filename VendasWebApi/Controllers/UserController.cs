using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VendasWebApplication.Commands.ChangePasswordCommand;
using VendasWebApplication.Commands.CreateUserCommand;
using VendasWebApplication.Commands.LoginUserCommands;
using VendasWebApplication.Commands.RetrievePasswordCommand;
using VendasWebApplication.Commands.UpdateUserCommand;
using VendasWebApplication.Queries.GetAllUsers;
using VendasWebApplication.Queries.GetUserByEmail;

namespace VendasWebApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateUserCommand> _createUserCommandValidator;
        private readonly IValidator<ChangePasswordCommand> _changePasswordCommandValidator;
        private readonly IValidator<RetrievePasswordCommand> _retrievePasswordCommandValidator;
        private readonly IValidator<UpdateUserCommand> _updateUserCommandValidator;
        private readonly IValidator<LoginUserCommand> _loginUserCommandValidator;

        public UserController(IMediator mediator,
            IValidator<CreateUserCommand> createUserCommandValidator,
            IValidator<ChangePasswordCommand> changePasswordCommandValidator,
            IValidator<RetrievePasswordCommand> retrievePasswordCommandValidator,
            IValidator<UpdateUserCommand> updateUserCommandValidator,
            IValidator<LoginUserCommand> loginUserCommandValidator)
        {
            _mediator = mediator;
            _createUserCommandValidator = createUserCommandValidator;
            _changePasswordCommandValidator = changePasswordCommandValidator;
            _retrievePasswordCommandValidator = retrievePasswordCommandValidator;
            _updateUserCommandValidator = updateUserCommandValidator;
            _loginUserCommandValidator = loginUserCommandValidator;
        }

        [HttpPost("createUser")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand request)
        {
            var inputValidator = await _createUserCommandValidator.ValidateAsync(request, new CancellationToken());
            if (!inputValidator.IsValid)
            {
                var errors = inputValidator.ToDictionary();

                return new BadRequestObjectResult(errors);
            }

            var id = await _mediator.Send(request);
            return Ok($"Usuário {id} Cadastrado com sucesso!");
            //return CreatedAtAction(nameof(GetById), new { id = id }, command);
        }        

        [HttpGet("getUsers")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersQuery getAllUsersQuery)
        {
            var user = await _mediator.Send(getAllUsersQuery);
            
            if (user.ItemsCount == 0)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("getUsersByEmail")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsersByEmail([FromQuery] GetUserByEmailQuery getUserByEmail)
        {
            var user = await _mediator.Send(getUserByEmail);

            if (user == null)
            {
                return NotFound("Usuário não encontrado");
            }
            return Ok(user);
        }

        [HttpPut("updateUser")]
        [Authorize(Roles = "Admin, Sales")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand request)
        {
            var inputValidator = await _updateUserCommandValidator.ValidateAsync(request, new CancellationToken());
            if (!inputValidator.IsValid)
            {
                var errors = inputValidator.ToDictionary();

                return new BadRequestObjectResult(errors);
            }
            try
            {
                var user = await _mediator.Send(request);                
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand request)
        {
            var inputValidator = await _loginUserCommandValidator.ValidateAsync(request, new CancellationToken());
            if (!inputValidator.IsValid)
            {
                var errors = inputValidator.ToDictionary();

                return new BadRequestObjectResult(errors);
            }
            var user = await _mediator.Send(request);
            if (user == null)
            {
                return BadRequest("Usuário não encontrado");
            }
            return Ok(user);
        }

        [HttpPut("retrievePassword")]
        [AllowAnonymous]
        public async Task<IActionResult> RetrievePassword([FromBody] RetrievePasswordCommand request)
        {
            var inputValidator = await _retrievePasswordCommandValidator.ValidateAsync(request, new CancellationToken());
            if (!inputValidator.IsValid)
            {
                var errors = inputValidator.ToDictionary();

                return new BadRequestObjectResult(errors);
            }            
            try
            {
                var user = await _mediator.Send(request);
                if (user.Count > 0)
                {
                    return Ok(user);
                }
                else 
                {
                    throw new KeyNotFoundException("Usuário inexistente na base de dados");
                }
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("changePassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand request)
        {
            var inputValidator = await _changePasswordCommandValidator.ValidateAsync(request, new CancellationToken());
            if (!inputValidator.IsValid)
            {
                var errors = inputValidator.ToDictionary();

                return new BadRequestObjectResult(errors);
            }
            try
            {
                await _mediator.Send(request);
                return Ok("Password updated!");
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
