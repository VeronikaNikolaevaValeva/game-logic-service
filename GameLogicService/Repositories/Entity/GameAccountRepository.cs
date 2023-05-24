using GameLogicService.DataContext;
using GameLogicService.Models.Entity;
using GameLogicService.Repositories.Entity.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GameLogicService.Repositories.Entity
{
    public class GameAccountRepository : BaseRepository<GameAccount>, IGameAccountRepository
    {
        public GameAccountRepository(DatabaseContext db)
            : base(db)
        {

        }

        public async Task<GameAccount?> GetByEmailAsync(string email) => await _db.GameAccounts.FirstOrDefaultAsync(a => a.EmailAddress == email);

        public async Task<GameAccount?> GetByUsernameAsync(string username) => await _db.GameAccounts.FirstOrDefaultAsync(a => a.Username == username);

        public async Task<GameAccount?> GetByUsernameAndEmailAsync(string username, string email)
            =>  (await _db.GameAccounts
                .Where(ga => ga.Username == username && ga.EmailAddress == email)
                .FirstOrDefaultAsync());
        
    }
}
