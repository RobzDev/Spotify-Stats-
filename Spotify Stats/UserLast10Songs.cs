using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Spotify_Stats
{
    public class UserLast10Songs

        
    {
        
        
        public async Task<List<RecentlyPlayedItem>> GetRecentlyPlayedTracks(int limit = 10)
        {
            HttpClient client = new HttpClient();

            // Agregar el token de acceso en el encabezado de autorización
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Properties.Settings.Default.AccessToken);


            var response = await client.GetAsync($"https://api.spotify.com/v1/me/player/recently-played?limit={limit}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al obtener historial: {content}");
            }

            var history = JsonConvert.DeserializeObject<RecentlyPlayedResponse>(content);

            return history.Items.Select(item => new RecentlyPlayedItem
            {
                TrackName = item.Track.Name,
                Artists = string.Join(", ", item.Track.Artists.Select(a => a.Name)),
                AlbumName = item.Track.Album.Name,
                AlbumImageUrl = item.Track.Album.Images.FirstOrDefault()?.Url,
                PlayedAt = item.PlayedAt.ToLocalTime(),
                Duration = TimeSpan.FromMilliseconds(item.Track.DurationMs)
            }).ToList();
        }

        public class RecentlyPlayedItem
        {
            public string TrackName { get; set; }
            public string Artists { get; set; }
            public string AlbumName { get; set; }
            public string AlbumImageUrl { get; set; }
            public DateTime PlayedAt { get; set; }
            public TimeSpan Duration { get; set; }

            public string PlayedAtRelative => GetRelativeTime(PlayedAt);

            private string GetRelativeTime(DateTime date)
            {
                var span = DateTime.Now - date;
                if (span.TotalDays > 1) return $"Hace {Math.Floor(span.TotalDays)} días";
                if (span.TotalHours > 1) return $"Hace {Math.Floor(span.TotalHours)} horas";
                if (span.TotalMinutes > 1) return $"Hace {Math.Floor(span.TotalMinutes)} minutos";
                return "Hace unos momentos";
            }
        }





    }
    public class Track
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("duration_ms")]
        public long DurationMs { get; set; }

        [JsonProperty("artists")]
        public List<Artist> Artists { get; set; }

        [JsonProperty("album")]
        public Album Album { get; set; }
    }

    public class Artist
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Album
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("images")]
        public List<Image> Images { get; set; }
    }

    public class Image
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }
    }



    public class RecentlyPlayedResponse
    {
        [JsonProperty("items")]
        public List<PlayHistory> Items { get; set; }
    }

    public class PlayHistory
    {
        [JsonProperty("track")]
        public Track Track { get; set; }

        [JsonProperty("played_at")]
        public DateTime PlayedAt { get; set; }

        [JsonProperty("context")]
        public Context Context { get; set; }
    }

    public class Context
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }


}
