using ReelRoster.Models.Database;
using ReelRoster.Models.TMDB;
using System.Threading.Tasks;

namespace ReelRoster.Services.Interfaces
{
    public interface IDataMappingSerivce
    {
        Task<Movie> MapMovieDetailAsync(MovieDetail movie);
        ActorDetail MapActorDetail(ActorDetail actor);

    }
}
