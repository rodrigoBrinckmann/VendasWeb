using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VendasWebApplication.Commands.ChangePasswordCommand;
using VendasWebApplication.Commands.CreateUserCommand;
using VendasWebApplication.Commands.LoginUserCommands;
using VendasWebApplication.Commands.UpdateUserCommand;
using VendasWebApplication.Queries.GetAllProdutos;
using VendasWebApplication.Queries.GetAllUsers;
using VendasWebApplication.Queries.GetUserById;

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
        [Authorize(Roles ="ADMIN")]
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
        [Authorize(Roles = "ADMIN")]
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
        [Authorize(Roles = "ADMIN, Sales")]
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

        //[HttpPut("retrievePassword")]
        //[AllowAnonymous]
        //public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand request)
        //{
        //vai mandar um email com o novo password para o usuário
        //    try
        //    {
        //        var user = await _mediator.Send(request);
        //        return Ok(user);
        //    }
        //    catch (KeyNotFoundException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPut("changePassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand request)
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
    }
}
