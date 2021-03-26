using System;
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
        IRequestHandler<RegistrarAplicativoCommand, ValidationResult>,
        IRequestHandler<AtualizarAplicativoCommand, ValidationResult>
    {
        private readonly IAplicativoRepository _aplicativoRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public AplicativoCommandHandler(IAplicativoRepository aplicativoRepository, IMediatorHandler mediatorHandler)
        {
            _aplicativoRepository = aplicativoRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<ValidationResult> Handle(RegistrarAplicativoCommand request, CancellationToken cancellationToken)
        {
            if (!request.Valido()) return request.ValidationResult;

            var aplicativo = new Aplicativo(request.Nome, DateTime.Parse(request.DataLancamento), request.TipoPlataforma);
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
            aplicativo.AtualizarDataLancamento(DateTime.Parse(request.DataLancamento));
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
            var aplicativoResponsavel =
                await _aplicativoRepository.ObterAplicativoResponsavel(request.DesenvolvedorResponsavel.Id);

            if (aplicativoResponsavel != null)
            {
                AdicionarErro($@"Este desenvolvedor já é responsável pelo aplicativo: {aplicativoResponsavel.Nome}.");
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
            aplicativo.RemoverResponsavel(request.DesenvolvedorResponsavel);
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

        public async Task<ValidationResult> Handle(AtualizarAplicativoCommand request, CancellationToken cancellationToken)
        {
            if (!request.Valido()) return request.ValidationResult;

            var aplicativo = await _aplicativoRepository.ObterPorIdComTracking(request.Id);

            if (aplicativo == null)
            {
                AdicionarErro("Não foi encontrado Aplicativo com este Id");
                return ValidationResult;
            }

            aplicativo.AtualizarNome(request.Nome); 
            aplicativo.AtualizarDataLancamento(DateTime.Parse(request.DataLancamento));
            aplicativo.AtualizarPlataforma(request.TipoPlataforma);

            _aplicativoRepository.Update(aplicativo);
            return await PersistirDados(_aplicativoRepository.UnitOfWork);
        }
    }
}