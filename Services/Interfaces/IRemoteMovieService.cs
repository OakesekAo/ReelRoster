using ReelRoster.Enums;
using ReelRoster.Models.TMDB;
using System.Threading.Tasks;

namespace ReelRoster.Services.Interfaces
{
    public interface IRemoteMovieService
    {
        Task<MovieDetail> MovieDetailAsync(int id);
        Task<MovieSearch> SearchMoviesAsync(MovieCategory category, int count);
        Task<ActorDetail> ActorDetailAsync(int id);
    }
}
