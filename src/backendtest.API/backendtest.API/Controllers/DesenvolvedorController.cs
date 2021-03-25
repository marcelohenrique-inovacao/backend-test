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

        #region GET

        [HttpGet("/v1/desenvolvedores")]
        public async Task<IEnumerable<Desenvolvedor>> GetTodosDesenvolvedores()
        {
            return await _desenvolvedorRepository.ObterTodos();
        }

        [HttpGet("/v1/desenvolvedor/{id}")]
        public async Task<Desenvolvedor> GetDesenvolvedor(Guid id)
        {
            return await _desenvolvedorRepository.ObterPorId(id);
        }

        [HttpGet("/v1/desenvolvedor/aplicativosrelacionados/{id}")]
        public async Task<IEnumerable<DesenvolvedorAplicativo>> GetDesenvolvedorAplicativosRelacionados(Guid id)
        {
            return await _desenvolvedorRepository.ObterAplicativosDesenvolvedorFazParte(id);
        }
        #endregion

        #region POST

        [HttpPost("/v1/desenvolvedor/registrar")]
        public async Task<IActionResult> PostCadastrar(RegistrarDesenvolvedorCommand command)
        {
            return CustomResponse(await _mediatorHandler.EnviarComando(command));
        }

        #endregion
        [HttpPut("/v1/desenvolvedor/{id}")]
        public async Task<IActionResult> PutAtualizarCadastro(AtualizarDesenvolvedorCommand command)
        {
            return CustomResponse(await _mediatorHandler.EnviarComando(command));
        }

        #region DELETE

        [HttpDelete("/v1/desenvolvedor/{id}")]
        public async Task<IActionResult> ExcluirDesenvolvedor(Guid id)
        {
            var desenvolvedor = await _desenvolvedorRepository.ObterPorIdComTracking(id);

            //REVIEW: Mostrar os nomes dos aplicativos na mensagem.
            if (!desenvolvedor.PermiteExcluir())
            {
                AdicionarErroProcessamento("Impossível excluir, pois este Desenvolvedor está vinculado à algum Aplicativo.");
                return CustomResponse();
            }

            var sucesso = await _desenvolvedorRepository.Excluir(desenvolvedor);

            if (sucesso)
                return CustomResponse("Excluído com sucesso");

            return CustomResponse("Falha ao excluir");

        }
        #endregion
    }
}
