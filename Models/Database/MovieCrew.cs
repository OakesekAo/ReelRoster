namespace ReelRoster.Models.Database
{
    public class MovieCrew
    {
        public int Id { get; set; }
        public int MovieId { get; set; }

        public int CrewId { get; set; }
        public string Department { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
        public string ImageUrl { get; set; }

        public Movie Movie { get; set; }
    }
}
