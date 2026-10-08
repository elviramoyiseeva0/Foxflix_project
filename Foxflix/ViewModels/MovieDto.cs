namespace Foxflix.ViewModels
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public string Description { get; set; }
        public int YearOfRelease { get; set; }
        public int Duration { get; set; }
        public string PosterPath { get; set; }
        public string VideoPath { get; set; }
        public List<string> Genres { get; set; } = new List<string>();
    }

    public class CreateMovieDto
    {
        public string Title { get; set; } 
        public string Description { get; set; } 
        public int YearOfRelease { get; set; }
        public int Duration { get; set; }
        public string PosterPath { get; set; }
        public string VideoPath { get; set; } 
        public List<int> GenreIds { get; set; } = new List<int>();
    }
}
