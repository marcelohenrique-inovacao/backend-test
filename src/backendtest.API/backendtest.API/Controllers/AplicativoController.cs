using backendtest.Domain.Application.Commands.Aplicativo;
using backendtest.Domain.Application.Commands.Vinculacao;
using backendtest.Domain.Data.Repositories;
using backendtest.Domain.Domain.Entities;
using backendtest.Shared.Communication.Mediator;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backendtest.Domain.Application.DTOs;
using backendtest.Shared.Communication;

namespace backendtest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AplicativoController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IAplicativoRepository _aplicativoRepository;
        private readonly ICommandResult _commandResult;

        public AplicativoController(IAplicativoRepository aplicativoRepository, IMediatorHandler mediatorHandler, IDesenvolvedorRepository desenvolvedorRepository, ICommandResult commandResult)
        {
            _aplicativoRepository = aplicativoRepository;
            _mediatorHandler = mediatorHandler;
            _commandResult = commandResult;
        }

        #region GET
        [HttpGet("/v1/aplicativos")]
        public async Task<ICommandResult> GetTodosAplicativos()
        {
            var aplicativos = await _aplicativoRepository.ObterTodos();
            _commandResult.AddResult(aplicativos);
            return _commandResult;
        }

        [HttpGet("/v1/aplicativo/{id}")]
        public async Task<ICommandResult> GetAplicativo(Guid id)
        {
            var aplicativo = await _aplicativoRepository.ObterPorId(id);
            _commandResult.AddResult(aplicativo);
            return _commandResult;
        }

        [HttpGet("/v1/aplicativo/desenvolvedoresrelacionados/{id}")]
        public async Task<ICommandResult> GetAplicativoDesenvolvedoresRelacionados(Guid id)
        {
            var desenvolvedores = await _aplicativoRepository.ObterDesenvolvedoresRelacionados(id);

            if (desenvolvedores.Any())
                _commandResult.AddResult(desenvolvedores);

            return _commandResult;
        }

        #endregion

        #region POST
        [HttpPost("/v1/aplicativo/registrar")]
        public async Task<ICommandResult> PostCadastrar(RegistrarAplicativoCommand command)
        {
            return await _mediatorHandler.EnviarComandoGenerico(command);
        }


        #endregion

        #region PUT
        [HttpPut("/v1/aplicativo/{id}")]
        public async Task<ICommandResult> PutAtualizarCadastro(Guid id, AtualizarAplicativoCommand command)
        {
            if (id == command.Id)
                return await _mediatorHandler.EnviarComandoGenerico(command);

            _commandResult.AddErro("Id", "O id informado no request está diferente do informado no JSON");
            return _commandResult;
        }

        [HttpPut("/v1/aplicativo/vincularDesenvolvedor")]
        public async Task<IActionResult> PutVincularDesenvolvedor(VincularAplicativoDesenvolvedorCommand command)
        {
            return CustomResponse(await _mediatorHandler.EnviarComando(command));
        }

        [HttpPut("/v1/aplicativo/desvincularDesenvolvedor")]
        public async Task<IActionResult> PutDesvincularDesenvolvedor(DesvincularAplicativoDesenvolvedorCommand command)
        {
            return CustomResponse(await _mediatorHandler.EnviarComando(command));
        }
        [HttpPut("/v1/aplicativo/AdicionarDesenvolvedorResponsavel")]
        public async Task<IActionResult> PutAdicionarDesenvolvedorResponsavel(AdicionarDesenvolvedorResponsavelCommand command)
        {
            return ModelState.IsValid ? CustomResponse(await _mediatorHandler.EnviarComando(command)) : CustomResponse("modelo");
        }
        [HttpPut("/v1/aplicativo/RemoverDesenvolvedorResponsavel")]
        public async Task<IActionResult> PutRemoverDesenvolvedorResponsavel(RemoverDesenvolvedorResponsavelCommand command)
        {
            return CustomResponse(await _mediatorHandler.EnviarComando(command));
        }

        #endregion

        #region DELETE

        [HttpDelete("/v1/aplicativo/{id}")]
        public async Task<ICommandResult> ExcluirAplicativo(Guid id)
        {
            var aplicativo = await _aplicativoRepository.ObterPorIdComTracking(id);

            if (aplicativo == null)
            {
                _commandResult.AddErro("Id", "Nenhum Aplicativo encontrado com esse Id");
                return _commandResult;
            }

            if (!aplicativo.PermiteExcluir())
            {
                var desenvolvedores =
                    await _aplicativoRepository.ObterDesenvolvedoresRelacionados(aplicativo.Id);

                var desenvolvedoresVinculados = new StringBuilder();

                desenvolvedoresVinculados.AppendJoin(", ",
                    desenvolvedores.Take(desenvolvedores.Count())
                        .ToList()
                        .Select(a => a.Nome));

                _commandResult.AddErro("Id", $@"Impossível excluir, pois este Aplicativo está vinculado aos Desenvolvedores: {desenvolvedoresVinculados}.");
                return _commandResult;
            }

            var sucesso = await _aplicativoRepository.Excluir(aplicativo);

            if (sucesso)
                _commandResult.AddResult("Excluído com sucesso");
            else
                _commandResult.AddErro("Id", "Falha ao excluir");

            return _commandResult;
        }

        #endregion
    }
}
