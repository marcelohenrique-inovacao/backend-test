using backendtest.Domain.Application.Commands;
using backendtest.Domain.Data.Repositories;
using backendtest.Domain.Domain.Entities;
using backendtest.Shared.Communication.Mediator;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backendtest.Shared.Extensions;

namespace backendtest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AplicativoController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IAplicativoRepository _aplicativoRepository;

        public AplicativoController(IAplicativoRepository aplicativoRepository, IMediatorHandler mediatorHandler, IDesenvolvedorRepository desenvolvedorRepository)
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
        public async Task<IActionResult> PutAtualizarCadastro(Guid id, AtualizarAplicativoCommand command)
        {
            if (id == command.Id)
                return CustomResponse(await _mediatorHandler.EnviarComando(command));

            AdicionarErroProcessamento("O id informado no request está diferente do informado no JSON");
            return CustomResponse();
        }


        #endregion

        #region DELETE

        [HttpDelete("/v1/aplicativo/{id}")]
        public async Task<IActionResult> ExcluirAplicativo(Guid id)
        {
            var aplicativo = await _aplicativoRepository.ObterPorIdComTracking(id);

            if (aplicativo == null)
            {
                AdicionarErroProcessamento("Nenhum Aplicativo encontrado com esse Id");
                return CustomResponse();
            }

            if (!aplicativo.PermiteExcluir())
            {
                var desenvolvedores =
                    await _aplicativoRepository.ObterDesenvolvedoresRelacionados(aplicativo.Id);

                var desenvolvedoresVinculados = new StringBuilder();

                desenvolvedoresVinculados.AppendJoin(",",
                    desenvolvedores.Take(desenvolvedores.Count())
                        .ToList()
                        .Select(a => a.FkDesenvolvedorNavigation.Nome));

                AdicionarErroProcessamento($@"Impossível excluir, pois este Aplicativo está vinculado aos Desenvolvedores: {desenvolvedoresVinculados}.");
                return CustomResponse();
            }

            var sucesso = await _aplicativoRepository.Excluir(aplicativo);

            return CustomResponse(sucesso ? "Excluído com sucesso" : "Falha ao excluir");
        }

        #endregion
    }
}
