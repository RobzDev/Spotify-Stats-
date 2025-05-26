using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify_Stats
{
    public class ArtistsData
    {
        private readonly HttpClient _httpClient;
        private const string ApiBaseUrl = "https://api.spotify.com/v1/artists";

        public ArtistsData(string accessToken)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
        }

        public async Task<List<ArtistInfo>> GetArtistsData(List<string> batchParameters)
        {
            var allArtists = new List<ArtistInfo>();

            foreach (var param in batchParameters)
            {
                try
                {
                    var response = await _httpClient.GetAsync($"{ApiBaseUrl}?ids={param}");
                    response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<SpotifyArtistsResponse>(content);

                    if (result?.Artists != null)
                    {
                        allArtists.AddRange(result.Artists);
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Error al obtener artistas: {ex.Message}");
                    // Continuar con los siguientes batches aunque falle uno
                }
            }

            return allArtists;
        }
    }

    // Clases para deserialización
    public class SpotifyArtistsResponse
    {
        [JsonProperty("artists")]
        public List<ArtistInfo> Artists { get; set; }
    }

    public class ArtistInfo
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("genres")]
        public List<string> Genres { get; set; }

        [JsonProperty("popularity")]
        public int Popularity { get; set; }
    }
}
