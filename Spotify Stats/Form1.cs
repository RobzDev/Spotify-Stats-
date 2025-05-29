using System;
using System.Net;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Text.Json;
using System.Text.Json.Serialization;



namespace Spotify_Stats
{
    public partial class Form1 : Form
    {


        public const string _clientId = "9567e54c155d41608d8473688b916e90";
        public const string _clientSecret = "3aef81f13c6142499d40004f08e0459d"; 
        public const string _redirectUri = "http://localhost:5000/callback";
        MainMenu mainMenu = new MainMenu();


        private SpotifyAuth _spotifyAuth;




        public Form1()
        {
            InitializeComponent();


            _spotifyAuth = new SpotifyAuth(_clientId, _clientSecret, _redirectUri);
            CheckExistingToken();
            btnLogin.Enabled = false;
        }





        private async void Login_Click(object sender, EventArgs e)
        {
            btnLogin.Enabled = false;

            lblStatus.Text = "Authenticating...";
            await Task.Delay(2000);

            var tokenResponse = await _spotifyAuth.Authenticate();

            var settings = Properties.Settings.Default;
            settings.AccessToken = tokenResponse.AccessToken;
            settings.RefreshToken = tokenResponse.RefreshToken;
            settings.TokenExpiration = tokenResponse.ExpirationTime;
            settings.Save();


            lblStatus.Text = "Logged in correctly";
            // set the lblStatus color to green
            lblStatus.ForeColor = Color.Green;
            await Task.Delay(2000);
            // open the MainMenu form and close this form
            var mainMenu = new MainMenu();
            mainMenu.Show();
            //close this form
            this.Hide();











        }



        private async void CheckExistingToken()
        {
            var settings = Properties.Settings.Default;

            // Si no hay token guardado
            if (string.IsNullOrEmpty(settings.AccessToken))
            {
                lblStatus.Text = "No autenticado";
                // set the lblStatus color to red
                lblStatus.ForeColor = Color.Red;

                await Task.Delay(2000);

                btnLogin.Enabled = true;


                return;
            }

            // Si el token no ha expirado
            if (settings.TokenExpiration > DateTime.Now)
            {
                //_apiClient = new SpotifyApiClient(settings.AccessToken);
                // lblStatus.Text = $"Autenticado (expira: {settings.TokenExpiration:g})";

                lblStatus.Text = "Logged in correctly";

                lblStatus.ForeColor = Color.Green;
                await Task.Delay(2000);

                // open the MainMenu form and close this form
                
                mainMenu.Show();
                this.Hide();


                return;
            }

            // Si hay refresh token pero el access token expiró
            if (!string.IsNullOrEmpty(settings.RefreshToken))
            {
                try
                {
                    lblStatus.Text = "Trying to Log in...";
                    // set the lblStatus color to yellow
                    lblStatus.ForeColor = Color.Yellow;
                    //time sleep for 2 seconds
                    await Task.Delay(2000);

                    var newToken = await _spotifyAuth.RefreshToken(settings.RefreshToken);

                    // Actualizar settings
                    settings.AccessToken = newToken.AccessToken;
                    settings.TokenExpiration = DateTime.Now.AddSeconds(newToken.ExpiresIn);

                    // Spotify puede no devolver un nuevo refresh_token, mantener el existente si es el caso
                    if (!string.IsNullOrEmpty(newToken.RefreshToken))
                    {
                        settings.RefreshToken = newToken.RefreshToken;
                    }
                    settings.Save();

                    //_apiClient = new SpotifyApiClient(settings.AccessToken);
                    lblStatus.Text = "Logged in correctly";
                    lblStatus.ForeColor = Color.Green;

                    await Task.Delay(2000);
                    var mainMenu = new MainMenu();
                    mainMenu.Show();
                    this.Hide();

                }
                catch (Exception ex)
                {
                    lblStatus.ForeColor = Color.Red;
                    lblStatus.Text = "Error renovando token";
                    MessageBox.Show($"Error al renovar token: {ex.Message}");

                    // Limpiar credenciales inválidas
                    settings.AccessToken = string.Empty;
                    settings.RefreshToken = string.Empty;
                    settings.Save();
                }
            }
            else
            {
                lblStatus.Text = "Please log in again";
            }
        }




    }
}
