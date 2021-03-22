using System.Threading.Tasks;

namespace backendtest.Shared.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}