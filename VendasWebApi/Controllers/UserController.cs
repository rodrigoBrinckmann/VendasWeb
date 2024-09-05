using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VendasWebApplication.Commands.CreateUserCommand;
using VendasWebApplication.Commands.LoginUserCommands;
using VendasWebApplication.Queries.GetAllProdutos;
using VendasWebApplication.Queries.GetAllUsers;

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

        [HttpGet("getUsers")]
        //[Authorize(Roles ="ADMIN")]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersQuery getAllUsersQuery)
        {
            var user = await _mediator.Send(getAllUsersQuery);
            
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
