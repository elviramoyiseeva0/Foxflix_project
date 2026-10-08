using Foxflix.Data;
using Foxflix.Interfaces;
using Foxflix.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foxflix.Repositories
{
    public class WatchlistRepository : IWatchlist
    {
        private readonly ApplicationContext _context;

        public WatchlistRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetUserWatchlistAsync(string userId)
        {
            var user = await _context.Users
                .Include(u => u.Watchlist)
                .ThenInclude(m => m.Genres)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user?.Watchlist ?? new List<Movie>();
        }

        public async Task<bool> AddAsync(string userId, int movieId)
        {
            var user = await _context.Users
                .Include(u => u.Watchlist)
                .FirstOrDefaultAsync(u => u.Id == userId);

            var movie = await _context.Movies.FindAsync(movieId);

            if (user == null || movie == null)
            {
                return false;
            }

            if (!user.Watchlist.Any(m => m.Id == movieId))
            {
                user.Watchlist.Add(movie);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> RemoveAsync(string userId, int movieId)
        {
            var user = await _context.Users
                .Include(u => u.Watchlist)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return false;
            }

            var movie = user.Watchlist.FirstOrDefault(m => m.Id == movieId);
            if (movie != null)
            {
                user.Watchlist.Remove(movie);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
