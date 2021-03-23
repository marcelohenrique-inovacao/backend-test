using backendtest.Domain.Data.Repositories;
using backendtest.Domain.Domain.Entities;
using backendtest.Shared.Messages;
using FluentValidation.Results; 
using System.Threading;
using System.Threading.Tasks;
using backendtest.Shared.Communication.Mediator;
using MediatR;

namespace backendtest.Domain.Application.Commands.Handlers
{
    public class DesenvolvedorCommandHandler : CommandHandler,
        IRequestHandler<RegistrarDesenvolvedorCommand, ValidationResult>
    {
        private readonly IDesenvolvedorRepository _desenvolvedorRepository;
        private readonly IMediatorHandler _mediatorHandler;


        public DesenvolvedorCommandHandler(IDesenvolvedorRepository desenvolvedorRepository, IMediatorHandler mediatorHandler)
        {
            _desenvolvedorRepository = desenvolvedorRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<ValidationResult> Handle(RegistrarDesenvolvedorCommand request, CancellationToken cancellationToken)
        {
            if (!request.Valido()) return request.ValidationResult;

            var desenvolvedor = new Desenvolvedor(request.Nome, request.Cpf, request.Email);

            var desenvolvedorExiste = await _desenvolvedorRepository.ObterPorCpf(request.Cpf);

            if (desenvolvedorExiste != null)
            {
                AdicionarErro("Já existe um cadastro de Desenvolvedor com este CPF.");
                return ValidationResult;
            }

            _desenvolvedorRepository.Adicionar(desenvolvedor);
            //REVIEW: Adicionar evento de cadastro.
            //desenvolvedor.AdicionarEvento(new DesenvolvedorRegistradoEvent());
            return await PersistirDados(_desenvolvedorRepository.UnitOfWork);
        }

        public async Task<bool> AtualizarEmail(RegistrarDesenvolvedorCommand request)
        {
            if (!request.Valido()) return false;

            var desenvolvedor = await _desenvolvedorRepository.ObterPorId(request.Id);
            desenvolvedor.AtualizarEmail(request.Email);
            _desenvolvedorRepository.Update(desenvolvedor);
            
            await PersistirDados(_desenvolvedorRepository.UnitOfWork);

            return true; 
        }

        public async Task<bool> AtualizarNome(RegistrarDesenvolvedorCommand request)
        {
            if (!request.Valido()) return false;

            var desenvolvedor = await _desenvolvedorRepository.ObterPorId(request.Id);
            desenvolvedor.AtualizarNome(request.Nome);
            _desenvolvedorRepository.Update(desenvolvedor);

            await PersistirDados(_desenvolvedorRepository.UnitOfWork);

            return true;
        }
        public async Task<bool> AtualizarCpf(RegistrarDesenvolvedorCommand request)
        {
            if (!request.Valido()) return false;

            var desenvolvedor = await _desenvolvedorRepository.ObterPorId(request.Id);
            desenvolvedor.AtualizarCpf(request.Cpf);
            _desenvolvedorRepository.Update(desenvolvedor);

            await PersistirDados(_desenvolvedorRepository.UnitOfWork);

            return true;
        }
    }
}