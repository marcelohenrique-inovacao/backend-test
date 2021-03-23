using System.Security;
using System.Threading;
using System.Threading.Tasks;
using backendtest.Domain.Data.Repositories;
using backendtest.Domain.Domain.Entities;
using backendtest.Shared.Communication.Mediator;
using backendtest.Shared.Messages;
using FluentValidation.Results;
using MediatR;

namespace backendtest.Domain.Application.Commands.Handlers
{
    public class AplicativoCommandHandler : CommandHandler,
        IRequestHandler<RegistrarAplicativoCommand, ValidationResult>
    {
        private readonly AplicativoRepository _aplicativoRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public AplicativoCommandHandler(AplicativoRepository aplicativoRepository, IMediatorHandler mediatorHandler)
        {
            _aplicativoRepository = aplicativoRepository;
            _mediatorHandler = mediatorHandler;
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

            if (request.DesenvolvedorResponsavel == null)
            {
                AdicionarErro("Favor informar um Desenvolvedor.");
                return false;
            }

            var aplicativo = await _aplicativoRepository.ObterPorId(request.Id);
            var desenvolvedorPossuiAplicativoResponsavel =
                await _aplicativoRepository.DesenvolvedorResponsavelPorAplicativo(request.DesenvolvedorResponsavel.Id);

            if (!desenvolvedorPossuiAplicativoResponsavel)
            {
                AdicionarErro("Este desenvolvedor já é responsável por outro aplicativo.");
                return false;
            }

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

        //public async Task<bool> VincularDesenvolvedor(RegistrarAplicativoCommand request)
        //{
        //    var quantidadeVinculos =
        //        await _aplicativoRepository.ObterAplicativosVinculados(request.DesenvolvedorResponsavel.Id);
            
        //    if (quantidadeVinculos.Count >= 3)
        //    {
        //        AdicionarErro("Cada desenvolvedor só pode ter até 3 aplicativos vinculados.");
        //        return false;
        //    }


        //}

        //public async Task<bool> DesvincularDesenvolvedor(RegistrarAplicativoCommand request)
        //{

        //}

    }
}