using ReelRoster.Models.Database;
using ReelRoster.Models.TMDB;
using System.Threading.Tasks;

namespace ReelRoster.Services.Interfaces
{
    public interface IDataMappingService
    {
        Task<Movie> MapMovieDetailAsync(MovieDetail movie);
        ActorDetail MapActorDetail(ActorDetail actor);

    }
}
