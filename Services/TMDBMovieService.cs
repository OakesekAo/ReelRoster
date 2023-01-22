using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using ReelRoster.Enums;
using ReelRoster.Models.Settings;
using ReelRoster.Models.TMDB;
using ReelRoster.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace ReelRoster.Services
{
    public class TMDBMovieService : IRemoteMovieService
    {
        private readonly AppSettings _appSettings;
        private readonly IHttpClientFactory _httpClient;

        public TMDBMovieService(IOptions<AppSettings> appSettings, IHttpClientFactory httpClient)
        {
            _appSettings = appSettings.Value;
            _httpClient = httpClient;
        }

        public Task<ActorDetail> ActorDetailAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<MovieDetail> MovieDetailAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<MovieSearch> SearchMoviesAsync(MovieCategory category, int count)
        {
            //Setup a default instance of MovieSearch
            MovieSearch movieSearch = new();

            //Assemble the full request uri string
            var query = $"{_appSettings.TMDBSettings.BaseUrl}/movie/{category}";

            var queryParams = new Dictionary<string, string>()
            {
                {"api_key", _appSettings.ReelRosterSettings.TmDbApiKey },
                {"language", _appSettings.TMDBSettings.QueryOptions.Language },
                {"page", _appSettings.TMDBSettings.QueryOptions.Page }
            };

            var requestUri = QueryHelpers.AddQueryString(query, queryParams);

            // Create a lient and execute the reuest
            var client = _httpClient.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var response = await client.SendAsync(request);

            //Return the MovieSearch object
            if (response.IsSuccessStatusCode)
            {
                var dcjs = new DataContractJsonSerializer(typeof(MovieSearch));
                using var responseStream = await response.Content.ReadAsStreamAsync();
                movieSearch = (MovieSearch)dcjs.ReadObject(responseStream);
                movieSearch.results = movieSearch.results.Take(count).ToArray();
                movieSearch.results.ToList().ForEach(r => r.poster_path = $"{_appSettings.TMDBSettings.BaseImagePath}/{_appSettings.ReelRosterSettings.DefualtPosterSize}/{r.poster_path}");
            }

            return movieSearch;
        }
    }
}
