namespace Foxflix.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }

        public string UserId { get; set; }
        public virtual User? User { get; set; }

        public int MovieId { get; set; }
        public virtual Movie? Movie { get; set; }
    }
}
