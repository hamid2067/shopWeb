using System.Threading;
using System.Threading.Tasks;
using Entities;

namespace Data
{
    public interface IUserRepository : IRepository<User>
    {


        public  Task<User> GetByPhone(string phone, CancellationToken cancellationToken);
        public Task<User> GetByUsers(string username, CancellationToken cancellationToken);
        Task<User> GetByUserAndPass(string username, string password, CancellationToken cancellationToken);

        Task AddAsync(User user, string password, CancellationToken cancellationToken);
        Task UpdateSecuirtyStampAsync(User user, CancellationToken cancellationToken);
        Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken);

        Task<User> GetByActiveCode(string phone, int activecode, CancellationToken cancellationToken);

    }
}