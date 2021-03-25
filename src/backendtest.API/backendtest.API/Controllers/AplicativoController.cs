using backendtest.Domain.Application.Commands;
using backendtest.Domain.Data.Repositories;
using backendtest.Domain.Domain.Entities;
using backendtest.Shared.Communication.Mediator;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backendtest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AplicativoController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IAplicativoRepository _aplicativoRepository;

        public AplicativoController(IAplicativoRepository aplicativoRepository, IMediatorHandler mediatorHandler)
        {
            _aplicativoRepository = aplicativoRepository;
            _mediatorHandler = mediatorHandler;
        }

        #region GET
        [HttpGet("/v1/aplicativos")]
        public async Task<IEnumerable<Aplicativo>> GetTodosAplicativos()
        {
            return await _aplicativoRepository.ObterTodos();
        }

        [HttpGet("/v1/aplicativo/{id}")]
        public async Task<Aplicativo> GetAplicativo(Guid id)
        {
            return await _aplicativoRepository.ObterPorId(id); 
        }

        [HttpGet("/v1/aplicativo/desenvolvedoresrelacionados/{id}")]
        public async Task<IEnumerable<DesenvolvedorAplicativo>> GetAplicativoDesenvolvedoresRelacionados(Guid id)
        {
            return await _aplicativoRepository.ObterDesenvolvedoresRelacionados(id);
        }

        #endregion

        #region POST
        [HttpPost("/v1/aplicativo/registrar")]
        public async Task<IActionResult> PostCadastrar(RegistrarAplicativoCommand command)
        {
            return CustomResponse(await _mediatorHandler.EnviarComando(command));
        }


        #endregion

        #region PUT
        [HttpPut("/v1/aplicativo/{id}")]
        public async Task<IActionResult> PutAtualizarCadastro(AtualizarAplicativoCommand command)
        {
            return CustomResponse(await _mediatorHandler.EnviarComando(command));
        }


        #endregion

        #region DELETE

        [HttpDelete("/v1/aplicativo/{id}")]
        public async Task<IActionResult> ExcluirAplicativo(Guid id)
        {
            var aplicativo = await _aplicativoRepository.ObterPorIdComTracking(id);

            //REVIEW: Mostrar os nomes dos desenvolvedores na mensagem.
            if (!aplicativo.PermiteExcluir())
            {
                AdicionarErroProcessamento("Impossível excluir, pois este Aplicativo está vinculado à algum Desenvolvedor.");
                return CustomResponse();
            }

            var sucesso = await _aplicativoRepository.Excluir(aplicativo);

            return CustomResponse(sucesso ? "Excluído com sucesso" : "Falha ao excluir");
        }

        #endregion
    }
}
