using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spotify_Stats
{
    public partial class PlaylistForm : Form
    {
        string id = "";

        //create a dictionary to store each artist id and name
        Dictionary<string, string> artists = new Dictionary<string, string>();
        //create a dictionary to store the artist name and the number of times it appears in the playlist
        Dictionary<string, int> artistCount = new Dictionary<string, int>();





        public PlaylistForm(System.Drawing.Image playlistimage, string name, string playlistID)
        {
            InitializeComponent();

            pbPlaylistImage.Image = playlistimage;

            id = playlistID;

            GetPlaylistSongs();




            lblPlaylistName.Text = name;
            

            pboxUserPhoto.Image = b64.Base64ToImage(Properties.Settings.Default.UserImageBase64);
            b64.SetCircularProfilePicture(pboxUserPhoto, pboxUserPhoto.Image);
            lblUsername.Text = Properties.Settings.Default.User;
            



        }

        private void PlaylistForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //when this form is closed the main form should be shown
            MainMenu mainForm = new MainMenu();
            mainForm.Show();
        }

        //creat an async method to get the songs from the playlist
        private async void GetPlaylistSongs()
        {
            var playlistsongs = new PlaylistSongs(id);
            var songs = await playlistsongs.GetPlaylistTracks();

            //obtain every artist id and name from the songs and store them in the dictionary
            foreach (var song in songs)
            {
                if (song.ArtistId != null && !artists.ContainsKey(song.Artists[0]))
                {
                    string ArtistName = song.Artists[0];
                    artists.Add(ArtistName, song.ArtistId);

                    if (!artistCount.ContainsKey(ArtistName))
                    {
                        artistCount.Add(ArtistName, 0); 
                    }
                
                   
                }
            }


        
                for (int i = 0; i < songs.Count; i++)
                {
                    string artistName = songs[i].Artists[0]; 
                    artistCount[artistName]++; 
                }



            //show the artists and the number of times they appear in the playlist in a message box
            string message = "Artists in the playlist:\n";
            foreach (var artist in artistCount)
            {
                message += $"{artist.Key}: {artist.Value}\n";
            }
            MessageBox.Show(message, "Artists in the playlist", MessageBoxButtons.OK, MessageBoxIcon.Information);









        }








    }
}
