namespace Foxflix.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int YearOfRelease { get; set; }
        public int Duration { get; set; }
        public string PosterPath { get; set; } 
        public string VideoPath { get; set; } 

        public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
        public virtual ICollection<User> Users { get; set; } = new List<User>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
