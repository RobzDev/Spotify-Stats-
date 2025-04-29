using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Spotify_Stats
{
    public class UserData
    {
        // Usando Newtonsoft.Json
        public async Task<UserProfile> GetUserProfileData(string accessToken)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                HttpResponseMessage response = await client.GetAsync("https://api.spotify.com/v1/me");

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();

             

                    return Newtonsoft.Json.JsonConvert.DeserializeObject<UserProfile>(responseContent);
                }
                else
                {
                    throw new Exception($"Error al obtener perfil: {response.StatusCode}");
                }
            }
        }

        // Clase para deserializar la respuesta del perfil de usuario
        public class UserProfile
        {
           [Newtonsoft.Json.JsonProperty("display_name")]

            public string DisplayName { get; set; }

            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("email")]
            public string Email { get; set; }

            [JsonPropertyName("images")]
            public List<SpotifyImage> Images { get; set; }

            [JsonPropertyName("country")]
            public string Country { get; set; }

            [JsonPropertyName("product")]
            public string Product { get; set; }

            // Propiedad de conveniencia para obtener la URL de la imagen de perfil
            public string ProfileImageUrl => Images != null && Images.Count > 0 ? Images[0].Url : null;
        }

        public class SpotifyImage
        {
            [JsonPropertyName("url")]
            public string Url { get; set; }

            [JsonPropertyName("height")]
            public int? Height { get; set; }

            [JsonPropertyName("width")]
            public int? Width { get; set; }
        }


        public async Task<Image> DownloadProfileImage(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
                return null;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(imageUrl);

                if (response.IsSuccessStatusCode)
                {
                    byte[] imageData = await response.Content.ReadAsByteArrayAsync();
                    using (MemoryStream ms = new MemoryStream(imageData))
                    {
                        return Image.FromStream(ms);
                    }
                }

                return null;
            }
        }


    }
}
