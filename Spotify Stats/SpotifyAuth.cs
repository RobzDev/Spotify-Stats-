using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Spotify_Stats
{
    using System;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Security.Policy;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public class SpotifyAuth
    {
      

        private const string AuthUrl = "https://accounts.spotify.com/authorize";

        private const string TokenUrl = "https://accounts.spotify.com/api/token";

        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _redirectUri;

        public SpotifyAuth(string clientId, string clientSecret, string redirectUri)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _redirectUri = redirectUri;
        }






        public  async Task<TokenResponse> Authenticate()
        {
            // Scopes necesarios para tu análisis
            var scopes = new[]
            {
            "user-top-read",          // Para tops de canciones/artistas
            "user-read-recently-played", // Historial de reproducción
            "playlist-read-private",   // Playlists del usuario
            "user-library-read",       // Música guardada
            "user-read-email"         // Info básica del usuario
        };

            var scopeString = string.Join("%20", scopes);

            var authUrl = $"{AuthUrl}?response_type=code&client_id={_clientId}" +
                         $"&redirect_uri={Uri.EscapeDataString(_redirectUri)}" +
                         $"&scope={scopeString}";

            // Iniciar HttpListener
            var listener = new HttpListener();
            listener.Prefixes.Add(_redirectUri + "/");
            listener.Start();

            // Abrir navegador
            OpenBrowserAsync(authUrl);

            // Esperar callback
            var context = await listener.GetContextAsync();
            var code = context.Request.QueryString["code"];

            // Responder al navegador
            var response = context.Response;
            string responseString = "<html><body><h1>Autenticación exitosa!</h1><p>Ya puedes cerrar esta ventana.</p></body></html>";
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
            response.Close();
            listener.Stop();

            if (string.IsNullOrEmpty(code))
                throw new Exception("No se recibió código de autorización");

            return await GetAccessToken(code);
        }
        public static async Task OpenBrowserAsync(string url)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };

                Process.Start(psi);

                // Pequeña espera para asegurar que el navegador se abrió
                await Task.Delay(1000);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir navegador: {ex.Message}");
            }
        }





        public  async Task<TokenResponse> GetAccessToken(string code)
        {
            using (var client = new HttpClient())
            {
                var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_clientId}:{_clientSecret}"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authHeader);

                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", _redirectUri)
            });

                var response = await client.PostAsync(TokenUrl, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Error al obtener token: {responseContent}");

                return JsonConvert.DeserializeObject<TokenResponse>(responseContent);
            }
        }


            public async Task<TokenResponse> RefreshToken(string refreshToken)
        {
            using (var client = new HttpClient())
            {
                var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_clientId}:{_clientSecret}"));
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authHeader);

                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("grant_type", "refresh_token"),
                new KeyValuePair<string, string>("refresh_token", refreshToken)
            });

                var response = await client.PostAsync(TokenUrl, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Error al refrescar token: {responseContent}");

                return JsonConvert.DeserializeObject<TokenResponse>(responseContent);
        
            
            }
        }








    }

        public class TokenResponse
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }

            [JsonProperty("refresh_token")]
            public string RefreshToken { get; set; }

            [JsonProperty("expires_in")]
            public int ExpiresIn { get; set; }


        [JsonIgnore]
        public DateTime ExpirationTime => DateTime.Now.AddSeconds(ExpiresIn);

        public TokenResponse(string accessToken, int expiresIn, string refreshToken = null)
        {
            AccessToken = accessToken;
            ExpiresIn = expiresIn;
            RefreshToken = refreshToken;
        }


    }



 }


