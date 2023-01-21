namespace ReelRoster.Models.TMDB
{

    public class MovieDetail
    {
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public Belongs_To_Collection belongs_to_collection { get; set; }
        public int budget { get; set; }
        public Genre[] genres { get; set; }
        public string homepage { get; set; }
        public int id { get; set; }
        public string imdb_id { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public float popularity { get; set; }
        public string poster_path { get; set; }
        public Production_Companies[] production_companies { get; set; }
        public Production_Countries[] production_countries { get; set; }
        public string release_date { get; set; }
        public int revenue { get; set; }
        public int runtime { get; set; }
        public Spoken_Languages[] spoken_languages { get; set; }
        public string status { get; set; }
        public string tagline { get; set; }
        public string title { get; set; }
        public bool video { get; set; }
        public float vote_average { get; set; }
        public int vote_count { get; set; }
        public Credits credits { get; set; }
        public Videos videos { get; set; }
        public Images images { get; set; }
        public Release_Dates release_dates { get; set; }
    }

    public class Belongs_To_Collection
    {
        public int id { get; set; }
        public string name { get; set; }
        public string poster_path { get; set; }
        public string backdrop_path { get; set; }
    }

    public class Credits
    {
        public Cast[] cast { get; set; }
        public Crew[] crew { get; set; }
    }

    public class Cast
    {
        public bool adult { get; set; }
        public int gender { get; set; }
        public int id { get; set; }
        public string known_for_department { get; set; }
        public string name { get; set; }
        public string original_name { get; set; }
        public float popularity { get; set; }
        public string profile_path { get; set; }
        public int cast_id { get; set; }
        public string character { get; set; }
        public string credit_id { get; set; }
        public int order { get; set; }
    }

    public class Crew
    {
        public bool adult { get; set; }
        public int gender { get; set; }
        public int id { get; set; }
        public string known_for_department { get; set; }
        public string name { get; set; }
        public string original_name { get; set; }
        public float popularity { get; set; }
        public string profile_path { get; set; }
        public string credit_id { get; set; }
        public string department { get; set; }
        public string job { get; set; }
    }

    public class Videos
    {
        public VideoResult[] results { get; set; }
    }

    public class VideoResult
    {
        public string iso_639_1 { get; set; }
        public string iso_3166_1 { get; set; }
        public string name { get; set; }
        public string key { get; set; }
        public string site { get; set; }
        public int size { get; set; }
        public string type { get; set; }
        public bool official { get; set; }
        public string published_at { get; set; }
        public string id { get; set; }
    }

    public class Images
    {
        public object[] backdrops { get; set; }
        public object[] logos { get; set; }
        public object[] posters { get; set; }
    }

    public class Release_Dates
    {
        public ReleaseResult[] results { get; set; }
    }

    public class ReleaseResult
    {
        public string iso_3166_1 { get; set; }
        public Release_Date[] release_dates { get; set; }
    }

    public class Release_Date
    {
        public string certification { get; set; }
        public object[] descriptors { get; set; }
        public string iso_639_1 { get; set; }
        public string note { get; set; }
        public string release_date { get; set; }
        public int type { get; set; }
    }

    public class Genre
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Production_Companies
    {
        public int id { get; set; }
        public string logo_path { get; set; }
        public string name { get; set; }
        public string origin_country { get; set; }
    }

    public class Production_Countries
    {
        public string iso_3166_1 { get; set; }
        public string name { get; set; }
    }

    public class Spoken_Languages
    {
        public string english_name { get; set; }
        public string iso_639_1 { get; set; }
        public string name { get; set; }
    }

}
