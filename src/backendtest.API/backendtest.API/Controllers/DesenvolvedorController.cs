using backendtest.Domain.Application.Commands.Desenvolvedor;
using backendtest.Domain.Data.Repositories;
using backendtest.Domain.Domain.Entities;
using backendtest.Shared.Communication.Mediator;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backendtest.Domain.Application.DTOs;
using backendtest.Shared.Communication;

namespace backendtest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesenvolvedorController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IDesenvolvedorRepository _desenvolvedorRepository;
        private readonly ICommandResult _comandResult;

        public DesenvolvedorController(IMediatorHandler mediatorHandler, IDesenvolvedorRepository desenvolvedorRepository, ICommandResult comandResult)
        {
            _mediatorHandler = mediatorHandler;
            _desenvolvedorRepository = desenvolvedorRepository;
            _comandResult = comandResult;
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
        public async Task<ICommandResult> PostCadastrar(RegistrarDesenvolvedorCommand command)
        {
            return await _mediatorHandler.EnviarComandoGenerico(command);
        }

        #endregion

        #region PUT

        [HttpPut("/v1/desenvolvedor/{id}")]
        public async Task<ICommandResult> PutAtualizarCadastro(Guid id, AtualizarDesenvolvedorCommand command)
        {
            if (id == command.Id)
                return await _mediatorHandler.EnviarComandoGenerico(command);

            _comandResult.AddErro("Id", "O Id informado no request está diferente do informado no JSON.");
            return _comandResult;
        }

        #endregion

        #region DELETE

        [HttpDelete("/v1/desenvolvedor/{id}")]
        public async Task<ICommandResult> ExcluirDesenvolvedor(Guid id)
        {
            var desenvolvedor = await _desenvolvedorRepository.ObterPorIdComTracking(id);

            if (desenvolvedor == null)
            {
                _comandResult.AddErro("Id", "Não existe Desenvolvedor com este Id");
                return _comandResult;
            }

            //REVIEW: Mostrar os nomes dos aplicativos na mensagem.
            if (!desenvolvedor.PermiteExcluir())
            {
                _comandResult.AddErro("Id", "Impossível excluir, pois este Desenvolvedor está vinculado à algum Aplicativo.");
                return _comandResult;

            }

            var sucesso = await _desenvolvedorRepository.Excluir(desenvolvedor);

            if (sucesso)
                _comandResult.AddResult("Excluído com sucesso");
            else
                _comandResult.AddErro("Id", "Falha ao excluir");

            return _comandResult;
        }
        #endregion
    }
}
