namespace ReelRoster.Models.Database
{
    public class MovieCollection
    {
        public int Id { get; set; }
        public int CollectionId { get; set; }
        public int MovieId { get; set; }
        public int Order { get; set; }

        public Collection Collection { get; set; }
        public Movie Movie { get; set; }

    }
}
