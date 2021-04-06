using backendtest.Domain.Application.Commands.Aplicativo;
using backendtest.Domain.Data.Repositories;
using backendtest.Shared.Communication.Mediator;
using backendtest.Shared.Messages;
using FluentValidation.Results;
using MediatR;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using backendtest.Shared.Communication;

namespace backendtest.Domain.Application.Commands.Handlers
{
    public class AplicativoCommandHandler : CommandHandler,
        IRequestHandler<RegistrarAplicativoCommand, ICommandResult>,
        IRequestHandler<AtualizarAplicativoCommand, ICommandResult>,
        IRequestHandler<ExcluirAplicativoCommand, ICommandResult>
    {
        private readonly IAplicativoRepository _aplicativoRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly ICommandResult _commandResult;

        public AplicativoCommandHandler(IAplicativoRepository aplicativoRepository, IMediatorHandler mediatorHandler, ICommandResult commandResult)
        {
            _aplicativoRepository = aplicativoRepository;
            _mediatorHandler = mediatorHandler;
            _commandResult = commandResult;
        }

        public async Task<ICommandResult> Handle(RegistrarAplicativoCommand request, CancellationToken cancellationToken)
        {
            if (!request.Valido())
            {
                _commandResult.AddFluentValidation(request.ValidationResult);
                return _commandResult;
            }

            var aplicativo = new Domain.Entities.Aplicativo(request.Nome, DateTime.Parse(request.DataLancamento), request.TipoPlataforma);
            var aplicativoExiste = await _aplicativoRepository.ObterPorNome(request.Nome);

            if (aplicativoExiste != null)
            {
                AdicionarErro("Nome","Já existe um aplicativo com este nome.");
                _commandResult.AddFluentValidation(ValidationResult);
                return _commandResult;
            }

            _aplicativoRepository.Adicionar(aplicativo);

            var validacaoSalvar = await PersistirDados(_aplicativoRepository.UnitOfWork);
            _commandResult.AddFluentValidation(validacaoSalvar);
            _commandResult.AddResult(aplicativo.Id);

            return _commandResult;
        }
        //public async Task<bool> AtualizarNome(RegistrarAplicativoCommand request)
        //{
        //    if (!request.Valido()) return false;

        //    var aplicativo = await _aplicativoRepository.ObterPorIdComTracking(request.Id);
        //    aplicativo.AtualizarNome(request.Nome);
        //    _aplicativoRepository.Update(aplicativo);

        //    await PersistirDados(_aplicativoRepository.UnitOfWork);

        //    return true;
        //}

        //public async Task<bool> AtualizarDataLancamento(RegistrarAplicativoCommand request)
        //{
        //    if (!request.Valido()) return false;

        //    var aplicativo = await _aplicativoRepository.ObterPorIdComTracking(request.Id);
        //    aplicativo.AtualizarDataLancamento(DateTime.Parse(request.DataLancamento));
        //    _aplicativoRepository.Update(aplicativo);

        //    await PersistirDados(_aplicativoRepository.UnitOfWork);

        //    return true;
        //}

        //public async Task<bool> AtualizarPlataforma(RegistrarAplicativoCommand request)
        //{
        //    if (!request.Valido()) return false;

        //    var aplicativo = await _aplicativoRepository.ObterPorIdComTracking(request.Id);
        //    aplicativo.AtualizarPlataforma(request.TipoPlataforma);
        //    _aplicativoRepository.Update(aplicativo);

        //    await PersistirDados(_aplicativoRepository.UnitOfWork);

        //    return true;
        //}  

        public async Task<ICommandResult> Handle(AtualizarAplicativoCommand request, CancellationToken cancellationToken)
        {
            if (!request.Valido())
            {
                _commandResult.AddFluentValidation(request.ValidationResult);
                return _commandResult;
            }


            var aplicativo = await _aplicativoRepository.ObterPorIdComTracking(request.Id);

            if (aplicativo == null)
            {
                AdicionarErro("Id","Não foi encontrado Aplicativo com este Id");
                _commandResult.AddFluentValidation(ValidationResult);
                return _commandResult;
            }

            aplicativo.AtualizarNome(request.Nome); 
            aplicativo.AtualizarDataLancamento(DateTime.Parse(request.DataLancamento));
            aplicativo.AtualizarPlataforma(request.TipoPlataforma);

            _aplicativoRepository.Update(aplicativo);

            var validacaoSalvar = await PersistirDados(_aplicativoRepository.UnitOfWork);
            _commandResult.AddFluentValidation(validacaoSalvar);
            _commandResult.AddResult(aplicativo.Id);

            return _commandResult;
        }

        public async Task<ICommandResult> Handle(ExcluirAplicativoCommand request, CancellationToken cancellationToken)
        {
            if (!request.Valido())
            {
                _commandResult.AddFluentValidation(request.ValidationResult);
                return _commandResult;
            }

            var aplicativo = await _aplicativoRepository.ObterPorIdComTracking(request.Id);

            if (aplicativo == null)
            {
                AdicionarErro("Id", "Nenhum Aplicativo encontrado com esse Id");
                _commandResult.AddFluentValidation(ValidationResult);
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

                AdicionarErro("Id", $@"Impossível excluir, pois este Aplicativo está vinculado aos Desenvolvedores: {desenvolvedoresVinculados}.");
                _commandResult.AddFluentValidation(ValidationResult);
                return _commandResult;
            }

            var sucesso = await _aplicativoRepository.Excluir(aplicativo);

            if (sucesso)
                _commandResult.AddResult("Excluído com sucesso");
            else
                _commandResult.AddErro("Id", "Falha ao excluir");

            return _commandResult;
        }
    }
}