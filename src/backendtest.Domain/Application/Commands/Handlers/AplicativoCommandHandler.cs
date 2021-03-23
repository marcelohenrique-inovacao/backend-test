using System.Security;
using System.Threading;
using System.Threading.Tasks;
using backendtest.Domain.Data.Repositories;
using backendtest.Domain.Domain.Entities;
using backendtest.Shared.Messages;
using FluentValidation.Results;
using MediatR;

namespace backendtest.Domain.Application.Commands.Handlers
{
    public class AplicativoCommandHandler : CommandHandler,
        IRequestHandler<RegistrarAplicativoCommand, ValidationResult>
    {
        private readonly AplicativoRepository _aplicativoRepository;

        public AplicativoCommandHandler(AplicativoRepository aplicativoRepository)
        {
            _aplicativoRepository = aplicativoRepository;
        }

        public async Task<ValidationResult> Handle(RegistrarAplicativoCommand request, CancellationToken cancellationToken)
        {
            if (!request.Valido()) return request.ValidationResult;

            var aplicativo = new Aplicativo(request.Nome, request.DataLancamento, request.TipoPlataforma);
            var aplicativoExiste = await _aplicativoRepository.ObterPorNome(request.Nome);

            if (aplicativoExiste != null)
            {
                AdicionarErro("Já existe um aplicativo com este nome.");
                return ValidationResult;
            }

            _aplicativoRepository.Adicionar(aplicativo);
            return await PersistirDados(_aplicativoRepository.UnitOfWork);
        }
        public async Task<bool> AtualizarNome(RegistrarAplicativoCommand request)
        {
            if (!request.Valido()) return false;

            var aplicativo = await _aplicativoRepository.ObterPorId(request.Id);
            aplicativo.AtualizarNome(request.Nome);
            _aplicativoRepository.Update(aplicativo);

            await PersistirDados(_aplicativoRepository.UnitOfWork);

            return true;
        }

        public async Task<bool> AtualizarDataLancamento(RegistrarAplicativoCommand request)
        {
            if (!request.Valido()) return false;

            var aplicativo = await _aplicativoRepository.ObterPorId(request.Id);
            aplicativo.AtualizarDataLancamento(request.DataLancamento);
            _aplicativoRepository.Update(aplicativo);

            await PersistirDados(_aplicativoRepository.UnitOfWork);

            return true;
        }

        public async Task<bool> AtualizarPlataforma(RegistrarAplicativoCommand request)
        {
            if (!request.Valido()) return false;

            var aplicativo = await _aplicativoRepository.ObterPorId(request.Id);
            aplicativo.AtualizarPlataforma(request.TipoPlataforma);
            _aplicativoRepository.Update(aplicativo);

            await PersistirDados(_aplicativoRepository.UnitOfWork);

            return true;
        }

        public async Task<bool> TornarResponsavel(RegistrarAplicativoCommand request)
        {
            if (!request.Valido()) return false;

            var aplicativo = await _aplicativoRepository.ObterPorId(request.Id); 
            aplicativo.TornarResponsavel(request.DesenvolvedorResponsavel);
            _aplicativoRepository.Update(aplicativo);

            await PersistirDados(_aplicativoRepository.UnitOfWork);

            return true;
        }

        public async Task<bool> RemoverResponsavel(RegistrarAplicativoCommand request)
        {
            if (!request.Valido()) return false;

            var aplicativo = await _aplicativoRepository.ObterPorId(request.Id);
            aplicativo.RemoverResponsavel();
            _aplicativoRepository.Update(aplicativo);

            await PersistirDados(_aplicativoRepository.UnitOfWork);

            return true;
        }

    }
}