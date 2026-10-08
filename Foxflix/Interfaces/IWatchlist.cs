using Foxflix.Models;

namespace Foxflix.Interfaces
{
    public interface IWatchlist
    {
        Task<IEnumerable<Movie>> GetUserWatchlistAsync(string userId);
        Task<bool> AddAsync(string userId, int movieId);
        Task<bool> RemoveAsync(string userId, int movieId);
    }
}
