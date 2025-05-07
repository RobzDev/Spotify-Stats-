using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Spotify_Stats
{
    public class PlaylistSongs
    {
        private string playlistID;
        private HttpClient client;

        public PlaylistSongs(string playlistID)
        {
            this.playlistID = playlistID;
            this.client = new HttpClient();
            // Configurar el cliente HTTP una sola vez
            this.client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Properties.Settings.Default.AccessToken);
        }

        public async Task<List<PlaylistTrackItem>> GetPlaylistTracks()
        {
            List<PlaylistTrackItem> allTracks = new List<PlaylistTrackItem>();
            string nextUrl = $"https://api.spotify.com/v1/playlists/{playlistID}/tracks?limit=50"; // Usar el máximo de 50 por página

            while (!string.IsNullOrEmpty(nextUrl))
            {
                var response = await client.GetAsync(nextUrl);
                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error al obtener canciones de la playlist: {content}");
                }

                var tracksResponse = JsonConvert.DeserializeObject<PlaylistTracksResponse>(content);

                // Añadir las canciones de esta página al resultado total
                allTracks.AddRange(tracksResponse.Items.Select(item => new PlaylistTrackItem
                {
                    Id = item.Track?.Id,
                    Name = item.Track?.Name,
                    DurationMs = item.Track?.DurationMs ?? 0,
                    Artists = item.Track?.Artists?.Select(a => a.Name).ToList(),
                    AlbumName = item.Track?.Album?.Name,
                    AlbumImageUrl = item.Track?.Album?.Images?.FirstOrDefault()?.Url,
                    Popularity = item.Track?.Popularity ?? 0,
                    AddedAt = item.AddedAt,
                    AddedBy = item.AddedBy?.DisplayName,
                    //get the id from the artist
                    ArtistId = item.Track?.Artists?.FirstOrDefault()?.Id


                }));

                // Actualizar nextUrl para la siguiente iteración
                nextUrl = tracksResponse.Next;
            }

            return allTracks;
        }
    }

    // Clases para deserializar la respuesta de la API
    public class PlaylistTracksResponse
    {
        [JsonProperty("items")]
        public List<PlaylistTrack> Items { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }

    public class PlaylistTrack
    {
        [JsonProperty("track")]
        public SongTrack Track { get; set; }

        [JsonProperty("added_at")]
        public DateTime AddedAt { get; set; }

        [JsonProperty("added_by")]
        public AddedBy AddedBy { get; set; }
    }

    public class SongTrack
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("duration_ms")]
        public long DurationMs { get; set; }

        [JsonProperty("artists")]
        public List<SongArtist> Artists { get; set; }

        [JsonProperty("album")]
        public SongAlbum Album { get; set; }

        //add the popularity property
        [JsonProperty("popularity")]
        public int Popularity { get; set; }
    }

    public class SongArtist
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class SongAlbum
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("images")]
        public List<PlaylistImage> Images { get; set; }
    }

    public class AddedBy
    {
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class PlaylistTrackItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public long DurationMs { get; set; }
        public List<string> Artists { get; set; }
        public string AlbumName { get; set; }
        public string AlbumImageUrl { get; set; }
        public DateTime AddedAt { get; set; }
        public string AddedBy { get; set; }

        public string ArtistId { get; set; }

        public int Popularity { get; set; }
    }
}
