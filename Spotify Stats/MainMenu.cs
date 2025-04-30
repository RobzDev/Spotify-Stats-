
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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


        public MainMenu()
        {
            InitializeComponent();


            var settings = Properties.Settings.Default;
            string accessToken = settings.AccessToken;

            LoadRecentlyPlayedTracks();
            SetupUserProfileUI(accessToken);

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
                lblUsername.Text = profile.DisplayName ?? "Usuario de Spotify";

                // Si hay imagen de perfil disponible, descargarla y mostrarla
                if (!string.IsNullOrEmpty(profile.ProfileImageUrl))
                {
                    profileImage = await ud.DownloadProfileImage(profile.ProfileImageUrl);
                    if (profileImage != null)
                    {
                        // Configurar la imagen en un PictureBox circular
                        pboxUserPhoto.Image = profileImage;

                        // Si quieres hacer la imagen circular (común en interfaces modernas)
                        SetCircularProfilePicture(pboxUserPhoto, profileImage);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar perfil: {ex.Message}");
            }
        }

        public void SetCircularProfilePicture(PictureBox pb, System.Drawing.Image originalImage)
        {
            if (originalImage == null || pb == null)
                return;

            // Crear un nuevo bitmap cuadrado basado en el tamaño más pequeño
            int sideLength = Math.Min(originalImage.Width, originalImage.Height);
            Bitmap squareImage = new Bitmap(sideLength, sideLength);

            using (Graphics g = Graphics.FromImage(squareImage))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                // Dibujar la imagen recortándola como cuadrado en el centro
                Rectangle destRect = new Rectangle(0, 0, sideLength, sideLength);
                Rectangle srcRect = new Rectangle(
                    (originalImage.Width - sideLength) / 2,
                    (originalImage.Height - sideLength) / 2,
                    sideLength,
                    sideLength
                );

                g.DrawImage(originalImage, destRect, srcRect, GraphicsUnit.Pixel);
            }

            // Ahora crear una imagen circular
            Bitmap circularImage = new Bitmap(sideLength, sideLength);
            using (Graphics g = Graphics.FromImage(circularImage))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    path.AddEllipse(0, 0, sideLength, sideLength);
                    g.SetClip(path);
                    g.DrawImage(squareImage, 0, 0);
                }
            }

            // Ajustar el PictureBox
            pb.Image = circularImage;
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            pb.BackColor = Color.Transparent;

            // Opcional: recortar el PictureBox también
            System.Drawing.Drawing2D.GraphicsPath pathCircle = new System.Drawing.Drawing2D.GraphicsPath();
            pathCircle.AddEllipse(0, 0, pb.Width, pb.Height);
            pb.Region = new Region(pathCircle);
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
                recentlyPlayedPanel.Controls.Clear();

                var tracks = await userLast10Songs.GetRecentlyPlayedTracks(10);

                foreach (var track in tracks)
                {
                    var trackPanel = new Panel
                    {
                        Width = recentlyPlayedPanel.Width - 25,
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

                    recentlyPlayedPanel.Controls.Add(trackPanel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar historial: {ex.Message}");
            }
        }


    }
}
