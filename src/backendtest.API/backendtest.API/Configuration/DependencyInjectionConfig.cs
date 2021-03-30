using backendtest.Domain.Application.Commands.Aplicativo;
using backendtest.Domain.Application.Commands.Desenvolvedor;
using backendtest.Domain.Application.Commands.Handlers;
using backendtest.Domain.Application.Commands.Vinculacao;
using backendtest.Domain.Data;
using backendtest.Domain.Data.Repositories;
using backendtest.Shared.Communication;
using backendtest.Shared.Communication.Mediator;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace backendtest.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddScoped<IAspNetUser, AspNetUser>(); 
            services.AddScoped<DatabaseContext>(); 

            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<ICommandResult, GenericCommandResult>();

            services.AddScoped<IAplicativoRepository, AplicativoRepository>();
            services.AddScoped<IDesenvolvedorRepository, DesenvolvedorRepository>();

            services.AddScoped<IRequestHandler<RegistrarDesenvolvedorCommand, ICommandResult>, DesenvolvedorCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarDesenvolvedorCommand, ICommandResult>, DesenvolvedorCommandHandler>();
            services.AddScoped<IRequestHandler<RegistrarAplicativoCommand, ValidationResult>, AplicativoCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarAplicativoCommand, ValidationResult>, AplicativoCommandHandler>();

            services.AddScoped<IRequestHandler<AdicionarDesenvolvedorResponsavelCommand, ValidationResult>, VinculacaoAplicativoDesenvolvedorHandler>();
            services.AddScoped<IRequestHandler<RemoverDesenvolvedorResponsavelCommand, ValidationResult>, VinculacaoAplicativoDesenvolvedorHandler>();
            services.AddScoped<IRequestHandler<VincularAplicativoDesenvolvedorCommand, ValidationResult>, VinculacaoAplicativoDesenvolvedorHandler>();
            services.AddScoped<IRequestHandler<DesvincularAplicativoDesenvolvedorCommand, ValidationResult>, VinculacaoAplicativoDesenvolvedorHandler>();
        }
    }
}