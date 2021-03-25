using System;
using backendtest.Domain.Data.Repositories;
using backendtest.Domain.Domain.Entities;
using backendtest.Shared.Communication.Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;
using backendtest.Domain.Application.Commands;
using backendtest.Domain.Application.Commands.Handlers;
using backendtest.Domain.Data;
using FluentValidation.Results;

namespace backendtest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesenvolvedorController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler; 
        private readonly IDesenvolvedorRepository _desenvolvedorRepository;
        public DesenvolvedorController(IMediatorHandler mediatorHandler, IDesenvolvedorRepository desenvolvedorRepository)
        {
            _mediatorHandler = mediatorHandler;
            _desenvolvedorRepository = desenvolvedorRepository;
        }

        [HttpGet("/v1/desenvolvedores")]
        public async Task<IEnumerable<Desenvolvedor>> GetTodosDesenvolvedores()
        {
            return await _desenvolvedorRepository.ObterTodos();
        }
        [HttpPost("/v1/desenvolvedor/registrar")]
        public async Task<IActionResult> PostCadastrar(RegistrarDesenvolvedorCommand command)
        {
            return CustomResponse(await _mediatorHandler.EnviarComando(command)); 
        }

        [HttpPut("/v1/desenvolvedor/{id}")]
        public async Task<IActionResult> PutAtualizarCadastro(AtualizarDesenvolvedorCommand command)
        {
            return CustomResponse(await _mediatorHandler.EnviarComando(command));
        }
    }
}
