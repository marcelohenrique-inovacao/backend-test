using backendtest.Domain.Application.Commands.Vinculacao;
using backendtest.Domain.Data.Repositories;
using backendtest.Shared.Communication.Mediator;
using backendtest.Shared.Messages;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace backendtest.Domain.Application.Commands.Handlers
{
    public class VinculacaoAplicativoDesenvolvedorHandler : CommandHandler,
    IRequestHandler<AdicionarDesenvolvedorResponsavelCommand, ValidationResult>,
    IRequestHandler<DesvincularAplicativoDesenvolvedorCommand, ValidationResult>,
    IRequestHandler<RemoverDesenvolvedorResponsavelCommand, ValidationResult>,
    IRequestHandler<VincularAplicativoDesenvolvedorCommand, ValidationResult>
    {
        private readonly IAplicativoRepository _aplicativoRepository;
        private readonly IDesenvolvedorRepository _desenvolvedorRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public VinculacaoAplicativoDesenvolvedorHandler(IAplicativoRepository aplicativoRepository, IDesenvolvedorRepository desenvolvedorRepository, IMediatorHandler mediatorHandler)
        {
            _aplicativoRepository = aplicativoRepository;
            _desenvolvedorRepository = desenvolvedorRepository;
            _mediatorHandler = mediatorHandler;
        }
        public async Task<ValidationResult> Handle(AdicionarDesenvolvedorResponsavelCommand request,
            CancellationToken cancellationToken)
        {
            if (!request.Valido()) return request.ValidationResult;

            var aplicativo = await _aplicativoRepository.ObterPorIdComTracking(request.IdAplicativo);
            if (aplicativo == null)
            {
                AdicionarErro("Aplicativo não encontrado.");
                return ValidationResult;
            }

            var desenvolvedor = await _desenvolvedorRepository.ObterPorIdComTracking(request.IdDesenvolvedor);
            if (desenvolvedor == null)
            {
                AdicionarErro("Desenvolvedor não encontrado.");
                return ValidationResult;
            }

            var aplicativoResponsavel = await _aplicativoRepository.ObterAplicativoResponsavel(request.IdDesenvolvedor);
            if (aplicativoResponsavel != null)
            {
                AdicionarErro($@"O Desenvolvedor {desenvolvedor.Nome}, já é responsável pelo Aplicativo: {aplicativoResponsavel.Nome}");
                return ValidationResult;
            }

            aplicativo.RemoverResponsavel();
            aplicativo.AdicionarResponsavel(desenvolvedor);
            _aplicativoRepository.Update(aplicativo);

            await PersistirDados(_aplicativoRepository.UnitOfWork);
            return ValidationResult;
        }

        public async Task<ValidationResult> Handle(RemoverDesenvolvedorResponsavelCommand request,
            CancellationToken cancellationToken)
        {
            if (!request.Valido()) return request.ValidationResult;

            var aplicativo = await _aplicativoRepository.ObterPorIdComTracking(request.IdAplicativo);
            if (aplicativo == null)
            {
                AdicionarErro("Aplicativo não encontrado.");
                return ValidationResult;
            }

            aplicativo.RemoverResponsavel();
            _aplicativoRepository.Update(aplicativo);

            await PersistirDados(_aplicativoRepository.UnitOfWork);
            return ValidationResult;
        }

        public async Task<ValidationResult> Handle(VincularAplicativoDesenvolvedorCommand request,
            CancellationToken cancellationToken)
        {
            if (!request.Valido()) return request.ValidationResult;

            var aplicativo = await _aplicativoRepository.ObterPorIdComTracking(request.IdAplicativo);
            if (aplicativo == null)
            {
                AdicionarErro("Aplicativo não encontrado.");
                return ValidationResult;
            }

            var desenvolvedor = await _desenvolvedorRepository.ObterPorIdComTracking(request.IdDesenvolvedor);
            if (desenvolvedor == null)
            {
                AdicionarErro("Desenvolvedor não encontrado.");
                return ValidationResult;
            }

            var permiteVincular = await _aplicativoRepository.PermiteVincularDesenvolvedor(request.IdDesenvolvedor);
            if (!permiteVincular)
            {
                AdicionarErro("Este Desenvolvedor já está vinculado ao máximo(3) de Aplicativos permitidos.");
                return ValidationResult;
            }  

            _aplicativoRepository.VincularDesenvolvedor(aplicativo, desenvolvedor);
            _aplicativoRepository.Update(aplicativo);

            await PersistirDados(_aplicativoRepository.UnitOfWork);
            return ValidationResult;
        }

        public async Task<ValidationResult> Handle(DesvincularAplicativoDesenvolvedorCommand request,
            CancellationToken cancellationToken)
        {
            if (!request.Valido()) return request.ValidationResult;

            var aplicativo = await _aplicativoRepository.ObterPorIdComTracking(request.IdAplicativo);
            if (aplicativo == null)
            {
                AdicionarErro("Aplicativo não encontrado.");
                return ValidationResult;
            }

            var desenvolvedor = await _desenvolvedorRepository.ObterPorIdComTracking(request.IdDesenvolvedor);
            if (desenvolvedor == null)
            {
                AdicionarErro("Desenvolvedor não encontrado.");
                return ValidationResult;
            }
             

            await _aplicativoRepository.DesvincularDesenvolvedor(aplicativo, desenvolvedor); 
            _aplicativoRepository.Update(aplicativo);

            await PersistirDados(_aplicativoRepository.UnitOfWork);
            return ValidationResult;
        }
    }
}