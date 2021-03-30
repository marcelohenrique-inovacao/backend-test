using backendtest.Domain.Application.Commands.Desenvolvedor;
using backendtest.Domain.Data.Repositories;
using backendtest.Domain.Domain.Entities;
using backendtest.Shared.Communication.Mediator;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backendtest.Domain.Application.DTOs;

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
        public async Task<IEnumerable<DesenvolvedorDto>> GetTodosDesenvolvedores()
        {
            return await _desenvolvedorRepository.ObterTodos();
        }

        [HttpGet("/v1/desenvolvedor/{id}")]
        public async Task<DesenvolvedorDto> GetDesenvolvedor(Guid id)
        {
            return await _desenvolvedorRepository.ObterPorId(id);
        }

        [HttpGet("/v1/desenvolvedor/aplicativosrelacionados/{id}")]
        public async Task<IEnumerable<AplicativoDto>> GetDesenvolvedorAplicativosRelacionados(Guid id)
        {
            return await _desenvolvedorRepository.ObterAplicativosRelacionados(id);
        }
        #endregion

        #region POST

        [HttpPost("/v1/desenvolvedor/registrar")]
        public async Task<IActionResult> PostCadastrar(RegistrarDesenvolvedorCommand command)
        {
            return CustomResponse(await _mediatorHandler.EnviarComando(command));
        }

        #endregion

        #region PUT

        [HttpPut("/v1/desenvolvedor/{id}")]
        public async Task<IActionResult> PutAtualizarCadastro(Guid id, AtualizarDesenvolvedorCommand command)
        {
            if (id == command.Id)
                return CustomResponse(await _mediatorHandler.EnviarComando(command));

            AdicionarErroProcessamento("O Id informado no request está diferente do informado no JSON.");
            return CustomResponse();
        }

        #endregion

        #region DELETE

        [HttpDelete("/v1/desenvolvedor/{id}")]
        public async Task<IActionResult> ExcluirDesenvolvedor(Guid id)
        {
            var desenvolvedor = await _desenvolvedorRepository.ObterPorIdComTracking(id);

            if (desenvolvedor == null)
            {
                AdicionarErroProcessamento("Não existe Desenvolvedor com este Id");
                return CustomResponse();
            }

            //REVIEW: Mostrar os nomes dos aplicativos na mensagem.
            if (!desenvolvedor.PermiteExcluir())
            {
                AdicionarErroProcessamento("Impossível excluir, pois este Desenvolvedor está vinculado à algum Aplicativo.");
                return CustomResponse();
            }

            var sucesso = await _desenvolvedorRepository.Excluir(desenvolvedor);

            return CustomResponse(sucesso ? "Excluído com sucesso" : "Falha ao excluir");
        }
        #endregion
    }
}
