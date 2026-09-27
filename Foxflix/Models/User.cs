using Microsoft.AspNetCore.Identity;

namespace Foxflix.Models
{
    public class User : IdentityUser
    {
        public virtual ICollection<Movie> Watchlist { get; set; } = new List<Movie>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
