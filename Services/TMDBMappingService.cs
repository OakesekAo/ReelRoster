using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ReelRoster.Enums;
using ReelRoster.Models.Database;
using ReelRoster.Models.Settings;
using ReelRoster.Models.TMDB;
using ReelRoster.Services.Interfaces;

namespace ReelRoster.Services
{
    public class TMDBMappingService : IDataMappingSerivce
    {
        private AppSettings _appSettings;
        private readonly IImageService _imageService;

        public TMDBMappingService(IOptions<AppSettings> appSettings, IImageService imageService)
        {
            _appSettings = appSettings.Value;
            _imageService = imageService;
        }

        public async Task<Movie> MapMovieDetailAsync(MovieDetail movie)
        {
            Movie newMovie = null;
            try
            {
                newMovie = new Movie()
                {
                    MovieId = movie.id,
                    Title = movie.title,
                    TagLine = movie.tagline,
                    Overview = movie.overview,
                    RunTime = movie.runtime,
                    Backdrop = await EncodeBackdropImageAsync(movie.backdrop_path),
                    BackdropType = BuildImageType(movie.backdrop_path),
                    Poster = await EncodePosterImageAsync(movie.poster_path),
                    PosterType = BuildImageType(movie.poster_path),
                    Rating = GetRating(movie.release_dates),
                    ReleaseDate = DateTime.Parse(movie.release_date),
                    TrailerUrl = BuildTrailerPath(movie.videos),
                    VoteAverage = movie.vote_average
                };

                //The same person can be in the Cast/Crew list more than once but I only want 1 instance...
                //var castMembers = movie.credits.cast.OrderByDescending(c => c.popularity).GroupBy(c => c.cast_id).Select(g => g.First()).Take(20);                
                //foreach (var cast in castMembers)
                //{                   
                //    newMovie.Cast.Add(new MovieCast()
                //    {
                //        CastId = cast.id,
                //        Department = cast.known_for_department,
                //        Name = cast.name,
                //        Character = cast.character,                        
                //        ImageUrl = BuildCastImage(cast.profile_path),                        
                //    });
                //}

                var castMembers = movie.credits.cast.OrderByDescending(c => c.popularity).GroupBy(c => c.cast_id).Select(g => g.FirstOrDefault()).Take(20).ToList();
                castMembers.ForEach(member =>
                {
                    newMovie.Cast.Add(new MovieCast()
                    {
                        CastId = member.id,
                        Department = member.known_for_department,
                        Name = member.name,
                        Character = member.character,
                        ImageUrl = BuildCastImage(member.profile_path),
                    });
                });

                var crewMembers = movie.credits.crew.OrderByDescending(c => c.popularity).GroupBy(c => c.id).Select(g => g.First()).Take(20);
                foreach (var crew in crewMembers)
                {
                    newMovie.Crew.Add(new MovieCrew()
                    {
                        CrewId = crew.id,
                        Department = crew.department,
                        Name = crew.name,
                        Job = crew.job,
                        ImageUrl = BuildCastImage(crew.profile_path)
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in MapMovieDetailAsync: {ex.Message}");
            }
            return newMovie;
        }
        public ActorDetail MapActorDetail(ActorDetail actor)
        {
            //1. Image
            actor.profile_path = BuildCastImage(actor.profile_path);

            //2. Bio
            if (string.IsNullOrEmpty(actor.biography))
                actor.biography = "Not Available";

            //Place of birth
            if (string.IsNullOrEmpty(actor.place_of_birth))
                actor.place_of_birth = "Not Available";

            //Birthday
            if (string.IsNullOrEmpty(actor.birthday))
                actor.birthday = "Not Available";
            else
                actor.birthday = DateTime.Parse(actor.birthday).ToString("MMM dd, yyyy");

            return actor;
        }
        public string BuildCastImage(string profilePath)
        {
            if (string.IsNullOrEmpty(profilePath))
                return _appSettings.ReelRosterSettings.DefaultCastImage;

            return $"{_appSettings.TMDBSettings.BaseImagePath}/{_appSettings.ReelRosterSettings.DefaultPosterSize}/{profilePath}";
        }

        private async Task<byte[]> EncodeBackdropImageAsync(string path)
        {
            var backdropPath = $"{_appSettings.TMDBSettings.BaseImagePath}/{_appSettings.ReelRosterSettings.DefaultBackdropSize}/{path}";
            return await _imageService.EncodeImageURLAsync(backdropPath);
        }
        private async Task<byte[]> EncodePosterImageAsync(string path)
        {
            var posterPath = $"{_appSettings.TMDBSettings.BaseImagePath}/{_appSettings.ReelRosterSettings.DefaultPosterSize}/{path}";
            return await _imageService.EncodeImageURLAsync(posterPath);
        }
        private string BuildImageType(string path)
        {
            if (string.IsNullOrEmpty(path))
                return path;

            return $"image/{Path.GetExtension(path).TrimStart('.')}";
        }
        private MovieRating GetRating(Release_Dates dates)
        {
            var movieRating = MovieRating.NR;
            var certification = dates.results.FirstOrDefault(r => r.iso_3166_1 == "US");
            if (certification is not null)
            {
                var apiRating = certification.release_dates.FirstOrDefault(c => c.certification != "")?.certification.Replace("-", "");
                if (!string.IsNullOrEmpty(apiRating))
                {
                    movieRating = (MovieRating)Enum.Parse(typeof(MovieRating), apiRating, true);
                }
            }
            return movieRating;
        }
        private string BuildTrailerPath(Videos videos)
        {
            var videoKey = videos.results.FirstOrDefault(r => r.type.ToLower().Trim() == "trailer" && r.key != "")?.key;
            return string.IsNullOrEmpty(videoKey) ? videoKey : $"{_appSettings.TMDBSettings.BaseYouTubePath}{videoKey}";
        }
    }
}