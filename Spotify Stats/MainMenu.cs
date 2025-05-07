
using Microsoft.VisualBasic.ApplicationServices;
using Spotify_Stats.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Spotify_Stats.UserData;

namespace Spotify_Stats
{
    public partial class MainMenu : Form
    {

        //create an empty dictionAary named playlistsIDs that will store the IDs of the playlists
        public Dictionary<string, string> playlistsIDs = new Dictionary<string, string>();

        public MainMenu()
        {
            InitializeComponent();
            //create an array named playlistsIDs that will store the IDs of the playlists
          


            var settings = Properties.Settings.Default;
            string accessToken = settings.AccessToken;


            // Verificar si el token de acceso es válido
            if (!(string.IsNullOrEmpty(accessToken) || settings.TokenExpiration < DateTime.Now))
            {
                LoadRecentlyPlayedTracks();
                SetupUserProfileUI(accessToken);

            }

        }

        UserData ud = new UserData();
        System.Drawing.Image profileImage;
        //create an instance for the UserLast10Songs class
        UserLast10Songs userLast10Songs = new UserLast10Songs();



        private async Task SetupUserProfileUI(string accessToken)
        {
            try
            {
                // Obtener datos del perfil
                UserProfile profile = await ud.GetUserProfileData(accessToken);

                // Actualizar etiqueta con el nombre
                Properties.Settings.Default.User = profile.DisplayName;
                lblUsername.Text = profile.DisplayName ?? "Usuario de Spotify";

                // Si hay imagen de perfil disponible, descargarla y mostrarla
                if (!string.IsNullOrEmpty(profile.ProfileImageUrl))
                {
                    profileImage = await ud.DownloadProfileImage(profile.ProfileImageUrl);
                    if (profileImage != null)
                    {
                        // Configurar la imagen en un PictureBox circular
                        pboxUserPhoto.Image = profileImage;

                        Properties.Settings.Default.UserImageBase64 = b64.ImageToBase64(pboxUserPhoto.Image);
                        Properties.Settings.Default.Save();

                        // Si quieres hacer la imagen circular (común en interfaces modernas)
                        b64.SetCircularProfilePicture(pboxUserPhoto, profileImage);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar perfil: {ex.Message}");

            }
        }

        

        private void Logout()
        {
            // 1. Limpiar los tokens almacenados
            Properties.Settings.Default.AccessToken = string.Empty;
            Properties.Settings.Default.RefreshToken = string.Empty;
            Properties.Settings.Default.TokenExpiration = DateTime.MinValue;
            Properties.Settings.Default.Save();





        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Logout();


            Form1 loginForm = new Form1();
            loginForm.Show();
            this.Close();
        }

        private void MainMenu_FormClosed(object sender, FormClosedEventArgs e)
        {

            Application.Exit();
        }



        private async Task LoadRecentlyPlayedTracks()
        {
            try
            {
                MainFlowPanel.Controls.Clear();

                var tracks = await userLast10Songs.GetRecentlyPlayedTracks(10);

                foreach (var track in tracks)
                {
                    var trackPanel = new Panel
                    {
                        Width = MainFlowPanel.Width - 25,
                        Height = 80,
                        Margin = new Padding(5),
                        BorderStyle = BorderStyle.FixedSingle
                    };

                    // Imagen del álbum
                    var albumPicture = new PictureBox
                    {
                        Width = 70,
                        Height = 70,
                        Left = 5,
                        Top = 5,
                        SizeMode = PictureBoxSizeMode.StretchImage
                    };

                    if (!string.IsNullOrEmpty(track.AlbumImageUrl))
                    {
                        using (var webClient = new WebClient())
                        {
                            var imageData = webClient.DownloadData(track.AlbumImageUrl);
                            using (var stream = new MemoryStream(imageData))
                            {
                                albumPicture.Image = System.Drawing.Image.FromStream(stream);
                            }
                        }
                    }
                    else
                    {
                        //albumPicture.Image = Properties.Resources.DefaultAlbumImage;
                    }

                    // Información de la canción
                    var trackLabel = new Label
                    {
                        Text = track.TrackName,
                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                        Left = 80,
                        Top = 10,
                        AutoSize = true
                    };

                    var artistLabel = new Label
                    {
                        Text = track.Artists,
                        Font = new Font("Segoe UI", 9),
                        Left = 80,
                        Top = 30,
                        AutoSize = true
                    };

                    var timeLabel = new Label
                    {
                        Text = track.PlayedAtRelative,
                        Font = new Font("Segoe UI", 8),
                        Left = 80,
                        Top = 50,
                        AutoSize = true
                    };

                    trackPanel.Controls.Add(albumPicture);
                    trackPanel.Controls.Add(trackLabel);
                    trackPanel.Controls.Add(artistLabel);
                    trackPanel.Controls.Add(timeLabel);

                    MainFlowPanel.Controls.Add(trackPanel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar historial: {ex.Message}");
            }
        }

        private async void btnPlaylists_Click(object sender, EventArgs e)
        {

            LoadPlaylistsAsync();



        }

        private async Task LoadPlaylistsAsync()
        {
            try
            {
                GetUserPlaylists getUserPlaylists = new GetUserPlaylists();
                var playlists = await getUserPlaylists.GetUserPlaylist();
                //add the playlist name and id to the playlistsIDs dictionary
                foreach (var playlist in playlists)
                {
                    playlistsIDs.Add(playlist.Name, playlist.Id);
                }



                MainFlowPanel.Controls.Clear();

                foreach (var playlist in playlists)
                {
                    var playlistPanel = new Panel
                    {
                        Width = MainFlowPanel.Width - 25,
                        Height = 80,
                        Margin = new Padding(5),
                        BorderStyle = BorderStyle.FixedSingle,
                        Tag = playlist.Id // Guardamos el ID para posibles acciones futuras
                    };

                    // PictureBox para la imagen de la playlist
                    var playlistPicture = new PictureBox
                    {
                        Width = 70,
                        Height = 70,
                        Left = 5,
                        Top = 5,
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Cursor = Cursors.Hand
                    };

                    // Descargar imagen asíncronamente
                    if (!string.IsNullOrEmpty(playlist.ImageUrl))
                    {
                        await LoadImageAsync(playlistPicture, playlist.ImageUrl);
                    }
                    else
                    {
                        //playlistPicture.Image = Properties.Resources.DefaultPlaylistImage;
                    }

                    // Labels con la información
                    var nameLabel = new Label
                    {
                        Text = playlist.Name,
                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                        Left = 80,
                        Top = 10,
                        AutoSize = true,
                        ForeColor = Color.White,
                        Cursor = Cursors.Hand
                    };

                    var detailsLabel = new Label
                    {
                        Text = $"{playlist.TrackCount} canciones • {playlist.OwnerName}",
                        Font = new Font("Segoe UI", 8),
                        Left = 80,
                        Top = 35,
                        AutoSize = true,
                        ForeColor = Color.LightGray
                    };

                    playlistPanel.Click += OpenPlaylist;
                    playlistPicture.Click += OpenPlaylist;
                    nameLabel.Click += OpenPlaylist;

                    // Agregar controles al panel
                    playlistPanel.Controls.Add(playlistPicture);
                    playlistPanel.Controls.Add(nameLabel);
                    playlistPanel.Controls.Add(detailsLabel);

                    // Agregar panel al contenedor principal
                    MainFlowPanel.Controls.Add(playlistPanel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar playlists: {ex.Message}");
            }
        }






        private async Task LoadImageAsync(PictureBox pictureBox, string imageUrl)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var imageBytes = await httpClient.GetByteArrayAsync(imageUrl);
                    using (var ms = new MemoryStream(imageBytes))
                    {
                        pictureBox.Image = System.Drawing.Image.FromStream(ms);
                    }
                }
            }
            catch
            {
                //pictureBox.Image = Properties.Resources.DefaultPlaylistImage;
            }
        }


        private void OpenPlaylist(object sender, EventArgs e)
        {
            string id = "";
           
            Control clickedControl = (Control)sender;

           
            Panel playlistPanel = clickedControl as Panel ?? clickedControl.Parent as Panel;

            if (playlistPanel == null)
                return;

         
            PictureBox playlistImage = null;
            Label nameLabel = null;
            

            foreach (Control ctrl in playlistPanel.Controls)
            {
                if (ctrl is PictureBox)
                    playlistImage = (PictureBox)ctrl;
                else if (ctrl is Label label && label.Font.Bold)
                    nameLabel = label;
            }

            if (playlistImage == null || nameLabel == null)
                return;

            System.Drawing.Image image = playlistImage.Image;

            string playlistName = nameLabel.Text;

            //if the playlist name is in the playlistsIDs dictionary get the id
            if (playlistsIDs.ContainsKey(playlistName))
            {
                id = playlistsIDs[playlistName];
            }
            else
            {
                MessageBox.Show("No se encontró la ID de la lista de reproducción.");
                return;
            }





            // Ejemplo: puedes pasar esta info al nuevo formulario
            PlaylistForm playlistForm = new PlaylistForm(image, playlistName, id);
            playlistForm.Show();
            this.Hide();
        }





    }


}
