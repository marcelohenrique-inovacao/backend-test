using backendtest.API.Extensions;
using backendtest.Domain.Application.Commands.Desenvolvedor;
using backendtest.Domain.Data.Queries;
using backendtest.Domain.Data.Repositories;
using backendtest.Shared.Communication;
using backendtest.Shared.Communication.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace backendtest.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class DesenvolvedorController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IDesenvolvedorRepository _desenvolvedorRepository;
        private readonly ICommandResult _comandResult;
        private readonly IDesenvolvedorQueries _desenvolvedorQueries;

        public DesenvolvedorController(IMediatorHandler mediatorHandler, IDesenvolvedorRepository desenvolvedorRepository,
            ICommandResult comandResult, IDesenvolvedorQueries desenvolvedorQueries, IUser user) : base(user)
        {
            _mediatorHandler = mediatorHandler;
            _desenvolvedorRepository = desenvolvedorRepository;
            _comandResult = comandResult;
            _desenvolvedorQueries = desenvolvedorQueries;
        }

        #region GET
        [AllowAnonymous]
        [HttpGet("/v1/desenvolvedores")]
        public async Task<ICommandResult> GetTodosDesenvolvedores()
        {
            var desenvolvedores = await _desenvolvedorRepository.ObterTodos();
            _comandResult.AddResult(desenvolvedores);
            return _comandResult;
        }

        [HttpGet("/v1/desenvolvedor/{id}")]
        public async Task<ICommandResult> GetDesenvolvedor(Guid id)
        {
            var desenvolvedor = await _desenvolvedorRepository.ObterPorId(id);

            _comandResult.AddResult(desenvolvedor);
            return _comandResult;
        }

        [HttpGet("/v1/desenvolvedor/aplicativos-relacionados/{id}")]
        public async Task<ICommandResult> GetDesenvolvedorAplicativosRelacionados(Guid id)
        {
            var aplicativos = await _desenvolvedorRepository.ObterAplicativosRelacionados(id);
            if (aplicativos.Any())
                _comandResult.AddResult(aplicativos);
            return _comandResult;
        }

        [HttpGet("/v1/desenvolvedor/listar-todos-aplicativos-relacionados/{id}")]
        public async Task<ICommandResult> GetDesenvolvedorRelacaoAplicativos(Guid id)
        {
            var desenvolvedorRelacaoAplicativos = await _desenvolvedorQueries.ObterDesenvolvedorRelacaoAplicativos(id);
            _comandResult.AddResult(desenvolvedorRelacaoAplicativos);
            return _comandResult;
        }

        #endregion

        #region POST
        [ClaimsAuthorize("desenvolvedor", "registrar")]
        [HttpPost("/v1/desenvolvedor/registrar")]
        public async Task<ICommandResult> PostCadastrar(RegistrarDesenvolvedorCommand command)
        {
            return await _mediatorHandler.EnviarComandoGenerico(command);
        }

        #endregion

        #region PUT
        [ClaimsAuthorize("desenvolvedor", "atualizar")]
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
        [ClaimsAuthorize("desenvolvedor", "excluir")]
        [HttpDelete("/v1/desenvolvedor/{id}")]
        public async Task<ICommandResult> ExcluirDesenvolvedor(Guid id)
        {
            return await _mediatorHandler.EnviarComandoGenerico(new ExcluirDesenvolvedorCommand(id));
        }
        #endregion
    }
}
