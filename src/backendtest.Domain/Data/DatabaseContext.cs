using System.Linq;
using System.Threading.Tasks;
using backendtest.Domain.Domain.Entities;
using backendtest.Shared.Communication.Mediator;
using backendtest.Shared.Data;
using backendtest.Shared.DomainObjects;
using backendtest.Shared.Messages;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace backendtest.Domain.Data
{
    public class DatabaseContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        protected DatabaseContext(DbContextOptions<DatabaseContext> options, IMediatorHandler mediatorHandler)
        : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
            _mediatorHandler = mediatorHandler;
        }

        public DbSet<Desenvolvedor> Desenvolvedores { get; set; }
        public DbSet<Aplicativo> Aplicativos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            var sucesso = await base.SaveChangesAsync() > 0;
            if (sucesso) await _mediatorHandler.PublicarEventos(this);

            return sucesso;
        }
    } 
}