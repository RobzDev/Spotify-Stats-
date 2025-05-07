using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify_Stats
{
    public class GetUserPlaylists
    {

        
        public async Task<List<PlaylistItem>> GetUserPlaylist()
        {

            List<PlaylistItem> allPlaylists = new List<PlaylistItem>();
            string nextUrl = "https://api.spotify.com/v1/me/playlists?limit=50"; // Usar el máximo de 50 por página

            HttpClient client = new HttpClient();
            // Agregar el token de acceso en el encabezado de autorización
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Properties.Settings.Default.AccessToken);

            while (!string.IsNullOrEmpty(nextUrl))
            {
                var response = await client.GetAsync(nextUrl);
                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error al obtener listas de reproducción: {content}");
                }

                var playlistsResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<PlaylistsResponse>(content);

                // Añadir las playlists de esta página al resultado total
                allPlaylists.AddRange(playlistsResponse.Items.Select(item => new PlaylistItem
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    ImageUrl = item.Images?.FirstOrDefault()?.Url,
                    TrackCount = item.Tracks?.Total ?? 0,
                    IsPublic = item.IsPublic,
                    OwnerName = item.Owner?.DisplayName
                }));

                // Actualizar nextUrl para la siguiente iteración
                nextUrl = playlistsResponse.Next;
            }

            return allPlaylists;
        }
    }
    public class PlaylistsResponse
    {
        [JsonProperty("items")]
        public List<Playlist> Items { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }

    public class Playlist
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("images")]
        public List<PlaylistImage> Images { get; set; }

        [JsonProperty("tracks")]
        public TracksInfo Tracks { get; set; }

        [JsonProperty("public")]
        public bool IsPublic { get; set; }

        [JsonProperty("owner")]
        public Owner Owner { get; set; }
    }

    public class PlaylistImage
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("height")]
        public int? Height { get; set; }

        [JsonProperty("width")]
        public int? Width { get; set; }
    }

    public class TracksInfo
    {
        [JsonProperty("total")]
        public int Total { get; set; }
    }

    public class Owner
    {
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    
    public class PlaylistItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int TrackCount { get; set; }
        public bool IsPublic { get; set; }
        public string OwnerName { get; set; }
    }


}

