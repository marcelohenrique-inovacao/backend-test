using backendtest.Domain.Application.Commands.Vinculacao;
using backendtest.Domain.Data.Repositories;
using backendtest.Shared.Communication.Mediator;
using backendtest.Shared.Messages;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using backendtest.Shared.Communication;

namespace backendtest.Domain.Application.Commands.Handlers
{
    public class VinculacaoAplicativoDesenvolvedorHandler : CommandHandler,
    IRequestHandler<AdicionarDesenvolvedorResponsavelCommand, ICommandResult>,
    IRequestHandler<DesvincularAplicativoDesenvolvedorCommand, ICommandResult>,
    IRequestHandler<RemoverDesenvolvedorResponsavelCommand, ICommandResult>,
    IRequestHandler<VincularAplicativoDesenvolvedorCommand, ICommandResult>
    {
        private readonly IAplicativoRepository _aplicativoRepository;
        private readonly IDesenvolvedorRepository _desenvolvedorRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly ICommandResult _commandResult;

        public VinculacaoAplicativoDesenvolvedorHandler(IAplicativoRepository aplicativoRepository, IDesenvolvedorRepository desenvolvedorRepository, IMediatorHandler mediatorHandler, ICommandResult commandResult)
        {
            _aplicativoRepository = aplicativoRepository;
            _desenvolvedorRepository = desenvolvedorRepository;
            _mediatorHandler = mediatorHandler;
            _commandResult = commandResult;
        }
        public async Task<ICommandResult> Handle(AdicionarDesenvolvedorResponsavelCommand request,
            CancellationToken cancellationToken)
        {
            if (!request.Valido())
            {
                _commandResult.AddFluentValidation(request.ValidationResult);
                return _commandResult;
            }

            var aplicativo = await _aplicativoRepository.ObterPorIdComTracking(request.IdAplicativo);
            if (aplicativo == null)
            {
                AdicionarErro("Id", "Aplicativo não encontrado com este Id.");
                _commandResult.AddFluentValidation(ValidationResult);
                return _commandResult;
            }

            var desenvolvedor = await _desenvolvedorRepository.ObterPorIdComTracking(request.IdDesenvolvedor);
            if (desenvolvedor == null)
            {
                AdicionarErro("Id", "Desenvolvedor não encontrado com este Id.");
                _commandResult.AddFluentValidation(ValidationResult);
                return _commandResult;
            }

            var aplicativoResponsavel = await _aplicativoRepository.ObterAplicativoResponsavel(request.IdDesenvolvedor);
            if (aplicativoResponsavel != null)
            {
                AdicionarErro("Id", $@"O Desenvolvedor: {desenvolvedor.Nome}, com o Id informado, já é responsável pelo Aplicativo: {aplicativoResponsavel.Nome}");
                _commandResult.AddFluentValidation(ValidationResult);
                return _commandResult;
            }

            aplicativo.RemoverResponsavel();
            aplicativo.AdicionarResponsavel(desenvolvedor);
            _aplicativoRepository.Update(aplicativo);

            var validacaoSalvar = await PersistirDados(_aplicativoRepository.UnitOfWork);

            _commandResult.AddFluentValidation(validacaoSalvar);
            if(validacaoSalvar.IsValid)
                _commandResult.AddResult($@"Desenvolvedor {desenvolvedor.Nome}, agora é responsável pelo aplicativo {aplicativo.Nome}.");

            return _commandResult;
        }

        public async Task<ICommandResult> Handle(RemoverDesenvolvedorResponsavelCommand request,
            CancellationToken cancellationToken)
        {
            if (!request.Valido())
            {
                _commandResult.AddFluentValidation(request.ValidationResult);
                return _commandResult;
            }

            var aplicativo = await _aplicativoRepository.ObterPorIdComTracking(request.IdAplicativo);
            if (aplicativo == null)
            {
                AdicionarErro("Id", "Aplicativo não encontrado com este Id.");
                _commandResult.AddFluentValidation(ValidationResult);
                return _commandResult;
            }

            if (aplicativo.IdDesenvolvedorResponsavel == null)
            {
                AdicionarErro("Id", "Não existe responsável por este Aplicativo.");
                _commandResult.AddFluentValidation(ValidationResult);
                return _commandResult;
            }

            aplicativo.RemoverResponsavel();
            _aplicativoRepository.Update(aplicativo);

            var validacaoSalvar = await PersistirDados(_aplicativoRepository.UnitOfWork);
            _commandResult.AddFluentValidation(validacaoSalvar);

            if (validacaoSalvar.IsValid)
                _commandResult.AddResult($@"Foi removido o responsável pelo aplicativo {aplicativo.Nome}.");

            return _commandResult;
        }

        public async Task<ICommandResult> Handle(VincularAplicativoDesenvolvedorCommand request,
            CancellationToken cancellationToken)
        {
            if (!request.Valido())
            {
                _commandResult.AddFluentValidation(request.ValidationResult);
                return _commandResult;
            }

            var aplicativo = await _aplicativoRepository.ObterPorIdComTracking(request.IdAplicativo);
            if (aplicativo == null)
            {
                AdicionarErro("Id", "Aplicativo não encontrado com este Id.");
                _commandResult.AddFluentValidation(ValidationResult);
                return _commandResult;
            }

            var desenvolvedor = await _desenvolvedorRepository.ObterPorIdComTracking(request.IdDesenvolvedor);
            if (desenvolvedor == null)
            {
                AdicionarErro("Id", "Desenvolvedor não encontrado com este Id.");
                _commandResult.AddFluentValidation(ValidationResult);
                return _commandResult;
            }

            var permiteVincular = await _aplicativoRepository.PermiteVincularDesenvolvedor(request.IdDesenvolvedor);
            if (!permiteVincular)
            {
                AdicionarErro("Id", "O Desenvolvedor informado já está vinculado ao máximo(3) de Aplicativos permitidos.");
                _commandResult.AddFluentValidation(ValidationResult);
                return _commandResult;
            }

            _aplicativoRepository.VincularDesenvolvedor(aplicativo, desenvolvedor);
            _aplicativoRepository.Update(aplicativo);

            var validacaoSalvar = await PersistirDados(_aplicativoRepository.UnitOfWork);
            _commandResult.AddFluentValidation(validacaoSalvar);

            if (validacaoSalvar.IsValid)
                _commandResult.AddResult($@"Desenvolvedor {desenvolvedor.Nome}, agora está vinculado ao aplicativo {aplicativo.Nome}.");

            return _commandResult;
        }

        public async Task<ICommandResult> Handle(DesvincularAplicativoDesenvolvedorCommand request,
            CancellationToken cancellationToken)
        {
            if (!request.Valido())
            {
                _commandResult.AddFluentValidation(request.ValidationResult);
                return _commandResult;
            }

            var aplicativo = await _aplicativoRepository.ObterPorIdComTracking(request.IdAplicativo);
            if (aplicativo == null)
            {
                AdicionarErro("Id", "Aplicativo não encontrado com este Id.");
                _commandResult.AddFluentValidation(ValidationResult);
                return _commandResult;
            }

            var desenvolvedor = await _desenvolvedorRepository.ObterPorIdComTracking(request.IdDesenvolvedor);
            if (desenvolvedor == null)
            {
                AdicionarErro("Id", "Desenvolvedor não encontrado com este Id.");
                _commandResult.AddFluentValidation(ValidationResult);
                return _commandResult;
            }


            var sucesso= await _aplicativoRepository.DesvincularDesenvolvedor(aplicativo, desenvolvedor);

            if(!sucesso)
            {
                AdicionarErro("Id", $"Desenvolvedor {desenvolvedor.Nome}, não está viculado ao aplicativo {aplicativo.Nome}.");
                _commandResult.AddFluentValidation(ValidationResult);
                return _commandResult;
            }

            _aplicativoRepository.Update(aplicativo);

            var validacaoSalvar = await PersistirDados(_aplicativoRepository.UnitOfWork);
            _commandResult.AddFluentValidation(validacaoSalvar);

            if (validacaoSalvar.IsValid)
                _commandResult.AddResult($@"Desenvolvedor {desenvolvedor.Nome}, agora está desvinculado do aplicativo {aplicativo.Nome}.");

            return _commandResult;
        }
    }
}