using backendtest.Domain.Data.Repositories;
using backendtest.Domain.Domain.Entities;
using backendtest.Shared.Communication.Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using backendtest.Domain.Application.Commands;
using backendtest.Domain.Application.Commands.Handlers;
using FluentValidation.Results;

namespace backendtest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesenvolvedorController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public DesenvolvedorController(IMediatorHandler mediatorHandler) 
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpGet("/v1/desenvolvedores")]
        public async Task<IEnumerable<Desenvolvedor>> GetTodosDesenvolvedores([FromServices] IDesenvolvedorRepository desenvolvedorRepository)
        {
            return await desenvolvedorRepository.ObterTodos();
        }

        [HttpPost("/v1/desenvolvedor/registrar")]
        public async Task<ValidationResult> PostCadastrar([FromBody] RegistrarDesenvolvedorCommand command, 
            [FromServices] DesenvolvedorCommandHandler handler)
        {
            return await handler.Handle(command, new CancellationToken());
        }
        [HttpPost("/v2/desenvolvedor/registrar")]
        public async Task<IActionResult> PostCadastrarDev(RegistrarDesenvolvedorCommand command)
        {
            return CustomResponse(await _mediatorHandler.EnviarComando(command));
        }
    }
}
