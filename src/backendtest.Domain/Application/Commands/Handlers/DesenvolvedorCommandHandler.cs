using backendtest.Domain.Application.Commands.Desenvolvedor;
using backendtest.Domain.Data.Repositories;
using backendtest.Shared.Communication;
using backendtest.Shared.Communication.Mediator;
using backendtest.Shared.Messages;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace backendtest.Domain.Application.Commands.Handlers
{
    public class DesenvolvedorCommandHandler : CommandHandler,
        IRequestHandler<RegistrarDesenvolvedorCommand, ICommandResult>,
        IRequestHandler<AtualizarDesenvolvedorCommand, ICommandResult>,
        IRequestHandler<ExcluirDesenvolvedorCommand, ICommandResult>
    {
        private readonly IDesenvolvedorRepository _desenvolvedorRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly ICommandResult _comandResult;


        public DesenvolvedorCommandHandler(IDesenvolvedorRepository desenvolvedorRepository, IMediatorHandler mediatorHandler, ICommandResult comandResult)
        {
            _desenvolvedorRepository = desenvolvedorRepository;
            _mediatorHandler = mediatorHandler;
            _comandResult = comandResult;
        }

        public async Task<ICommandResult> Handle(RegistrarDesenvolvedorCommand request, CancellationToken cancellationToken)
        {
            if (!request.Valido())
            {
                _comandResult.AddFluentValidation(request.ValidationResult);
                return _comandResult;
            }


            var desenvolvedorExiste = await _desenvolvedorRepository.ObterPorCpf(request.Cpf);

            if (desenvolvedorExiste != null)
            {
                AdicionarErro("Cpf", "Já existe um cadastro de Desenvolvedor com este CPF.");
                _comandResult.AddFluentValidation(ValidationResult);
                return _comandResult;
            }

            var desenvolvedor = new Domain.Entities.Desenvolvedor(request.Nome, request.Cpf, request.Email);

            _desenvolvedorRepository.Adicionar(desenvolvedor);
            //REVIEW: Adicionar evento de cadastro.
            //desenvolvedor.AdicionarEvento(new DesenvolvedorRegistradoEvent());

            var validacaoSalvar = await PersistirDados(_desenvolvedorRepository.UnitOfWork);
            _comandResult.AddFluentValidation(validacaoSalvar);
            _comandResult.AddResult(desenvolvedor.Id);

            return _comandResult;
        }

        // Métodos Desnecessários?
        //public async Task<bool> AtualizarEmail(RegistrarDesenvolvedorCommand request)
        //{
        //    if (!request.Valido()) return false;

        //    var desenvolvedor = await _desenvolvedorRepository.ObterPorId(request.Id);
        //    desenvolvedor.AtualizarEmail(request.Email);
        //    _desenvolvedorRepository.Update(desenvolvedor);

        //    await PersistirDados(_desenvolvedorRepository.UnitOfWork);

        //    return true;
        //}

        //public async Task<bool> AtualizarNome(RegistrarDesenvolvedorCommand request)
        //{
        //    if (!request.Valido()) return false;

        //    var desenvolvedor = await _desenvolvedorRepository.ObterPorId(request.Id);
        //    desenvolvedor.AtualizarNome(request.Nome);
        //    _desenvolvedorRepository.Update(desenvolvedor);

        //    await PersistirDados(_desenvolvedorRepository.UnitOfWork);

        //    return true;
        //}
        //public async Task<bool> AtualizarCpf(RegistrarDesenvolvedorCommand request)
        //{
        //    if (!request.Valido()) return false;

        //    var desenvolvedor = await _desenvolvedorRepository.ObterPorId(request.Id);
        //    desenvolvedor.AtualizarCpf(request.Cpf);
        //    _desenvolvedorRepository.Update(desenvolvedor);

        //    await PersistirDados(_desenvolvedorRepository.UnitOfWork);

        //    return true;
        //}

        public async Task<ICommandResult> Handle(AtualizarDesenvolvedorCommand request, CancellationToken cancellationToken)
        {
            if (!request.Valido())
            {
                _comandResult.AddFluentValidation(request.ValidationResult);
                return _comandResult;
            }

            var desenvolvedor = await _desenvolvedorRepository.ObterPorIdComTracking(request.Id);

            if (desenvolvedor == null)
            {
                AdicionarErro("Id", "Não foi encontrado Desenvolvedor com este Id");
                _comandResult.AddFluentValidation(ValidationResult);
                return _comandResult;
            }

            desenvolvedor.AtualizarNome(request.Nome);
            desenvolvedor.AtualizarCpf(request.Cpf);
            desenvolvedor.AtualizarEmail(request.Email);

            _desenvolvedorRepository.Update(desenvolvedor); 
            var validacaoSalvar = await PersistirDados(_desenvolvedorRepository.UnitOfWork);
            _comandResult.AddFluentValidation(validacaoSalvar);
            _comandResult.AddResult("Alterado com sucesso.");

            return _comandResult;
        }

        public async Task<ICommandResult> Handle(ExcluirDesenvolvedorCommand request, CancellationToken cancellationToken)
        {
            if (!request.Valido())
            {
                _comandResult.AddFluentValidation(request.ValidationResult);
                return _comandResult;
            }

            var desenvolvedor = await _desenvolvedorRepository.ObterPorIdComTracking(request.Id);

            if (desenvolvedor == null)
            {
                AdicionarErro("Id", "Não existe Desenvolvedor com este Id.");
                _comandResult.AddFluentValidation(ValidationResult);
                return _comandResult;
            }

            //REVIEW: Mostrar os nomes dos aplicativos na mensagem.
            if (!desenvolvedor.PermiteExcluir())
            {
                AdicionarErro("Id", "Impossível excluir, pois este Desenvolvedor está vinculado à algum Aplicativo.");
                _comandResult.AddFluentValidation(ValidationResult);
                return _comandResult; 
            }

            var sucesso = await _desenvolvedorRepository.Excluir(desenvolvedor);

            if (sucesso)
                _comandResult.AddResult("Excluído com sucesso");
            else
                _comandResult.AddErro("Id", "Falha ao excluir");

            return _comandResult;
        }
    }
}