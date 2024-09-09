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

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("createUser")]
        [AllowAnonymous]
        public async Task<IActionResult> CadastrarUsuario([FromBody] CreateUserCommand request)
        {
            var id = await _mediator.Send(request);
            return Ok($"Usuário {id} Cadastrado com sucesso!");
            //return CreatedAtAction(nameof(GetById), new { id = id }, command);
        }        

        [HttpGet("getUsers")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersQuery getAllUsersQuery)
        {
            var user = await _mediator.Send(getAllUsersQuery);
            
            if (user == null)
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
            //validar se o email existe
            //fazer a alteração no db
            //mandar um email com o novo password para o usuário
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
