using ScottPlot;
using ScottPlot.Plottables;
using System;
using System.Collections;
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


        List<PlaylistTrackDisplayItem> songs = new List<PlaylistTrackDisplayItem>();

        private SortableBindingList<PlaylistTrackDisplayItem> filteredTracksList;
        SortableBindingList<PlaylistTrackDisplayItem> top100kSongs;





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


        private async void GetPlaylistSongs()
        {
            var playlistsongs = new PlaylistSongs(id);
            var songsRaw = await playlistsongs.GetPlaylistTracks();

            var displayList = songsRaw.Select(song => new PlaylistTrackDisplayItem
            {
                Name = song.Name,
                Album = song.AlbumName,
                Duration = ConvertDuration(song.DurationMs),
                Artists = CombineArtists(song.Artists),
                AddedAt = song.AddedAt.ToString("dd/MM/yyyy"),

                OriginalTrack = song
            }).ToList();



            songs = displayList;

            //obtain every artist id and name from the songs and store them in the dictionary
            foreach (var song in songs)
            {


                //add the song name, albumname, duration, artists and date added to the treeview
                TreeNode songNode = new TreeNode(song.Name);
                songNode.Nodes.Add("Album: " + song.Album);
                songNode.Nodes.Add("Duration: " + song.Duration);
                songNode.Nodes.Add("Artists: " + song.Artists);
                songNode.Nodes.Add("Date Added: " + song.AddedAt);
                songNode.Nodes.Add("Added By: " + song.OriginalTrack.AddedBy);
                tvSongs.Nodes.Add(songNode);


                if (song.OriginalTrack.ArtistId != null)
                {
                    string artistName = song.Artists.Split(new[] { ",", "&" }, StringSplitOptions.None)[0].Trim();

                    if (!artists.ContainsKey(artistName))
                    {
                        artists.Add(artistName, song.OriginalTrack.ArtistId);
                        artistCount.Add(artistName, 1);
                    }
                    else
                    {
                        artistCount[artistName]++;
                    }
                }

            }



            filteredTracksList = new SortableBindingList<PlaylistTrackDisplayItem>(songs);

            txtboxRawTxt.Clear();
            txtboxRawTxt.Text = string.Join(Environment.NewLine, filteredTracksList.Select(x => $"{x.Name} , {x.Album} , {x.Duration} , {x.Artists} , {x.AddedAt} , {x.OriginalTrack.AddedBy}"));

            dtvPlaylistSongs.DataSource = filteredTracksList;



            ConfigureAutoComplete();

            PopulateplotArtistsSongsCount();


            GetArtistsGenres();




        }


        private string ConvertDuration(long durationMs)
        {
            TimeSpan time = TimeSpan.FromMilliseconds(durationMs);
            return $"{time.Minutes:D2}:{time.Seconds:D2}";
        }




        private void btnViewOption_Click(object sender, EventArgs e)
        {
            if (dtvPlaylistSongs.Visible == true)
            {
                dtvPlaylistSongs.Visible = false;
                txtboxRawTxt.Visible = false;
                tvSongs.Visible = true;
                btnViewOption.Text = "Raw TXT View";
            }
            else if (tvSongs.Visible == true)
            {
                dtvPlaylistSongs.Visible = false;
                tvSongs.Visible = false;
                txtboxRawTxt.Visible = true;
                btnViewOption.Text = "Table View";
            }
            else if (txtboxRawTxt.Visible == true)
            {
                dtvPlaylistSongs.Visible = true;
                tvSongs.Visible = false;
                txtboxRawTxt.Visible = false;
                btnViewOption.Text = "Tree View";

            }
        }



        //create a method to configure autocomplete for the textbox
        private void ConfigureAutoComplete()
        {
            var autoCompleteCollection = new AutoCompleteStringCollection();

            // Llenar con nombres de canciones y artistas (sin duplicados)
            var sugerencias = songs
                .Select(c => c.Name)
                .Concat(songs.Select(c => c.Artists.Split(new[] { ",", "&" }, StringSplitOptions.None)[0].Trim()))
                .Distinct()
                .OrderBy(s => s)
                .ToArray();

            autoCompleteCollection.AddRange(sugerencias);

            // Configurar el TextBox
            txtboxSearch.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtboxSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtboxSearch.AutoCompleteCustomSource = autoCompleteCollection;
        }

        private void txtboxSearch_TextChanged(object sender, EventArgs e)
        {
            _searchDelayTimer.Stop();
            _searchDelayTimer.Start();
        }


        private void ApplySearchFilter(string searchText)
        {
            var filteredSongs = string.IsNullOrWhiteSpace(searchText)
                ? songs
                : songs.Where(song =>
                {
                    var searchContent = $"{song.Name} {song.Artists.Replace(",", " ")} {song.Album}"
                              .ToLower();
                    return searchText.ToLower()
                           .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                           .Where(term => term.Length >= 2)
                           .All(term => searchContent.Contains(term));
                }).ToList();

            filteredTracksList = new SortableBindingList<PlaylistTrackDisplayItem>(filteredSongs);
            dtvPlaylistSongs.DataSource = filteredTracksList;

            txtboxRawTxt.Clear();
            txtboxRawTxt.Text = string.Join(Environment.NewLine, filteredTracksList.Select(x => $"{x.Name} - {x.Album} - {x.Duration} - {x.Artists} - {x.AddedAt} , {x.OriginalTrack.AddedBy}"));

            tvSongs.Nodes.Clear(); // Clear the TreeView before adding new nodes
            foreach (var song in filteredTracksList)
            {
                TreeNode songNode = new TreeNode(song.Name);
                songNode.Nodes.Add("Album: " + song.Album);
                songNode.Nodes.Add("Duration: " + song.Duration);
                songNode.Nodes.Add("Artist: " + song.Artists);
                songNode.Nodes.Add("Date Added: " + song.AddedAt);
                songNode.Nodes.Add("Added By: " + song.OriginalTrack.AddedBy);
                tvSongs.Nodes.Add(songNode);
            }

        }


        private void _searchDelayTimer_Tick(object sender, EventArgs e)
        {
            _searchDelayTimer.Stop();
            ApplySearchFilter(txtboxSearch.Text);
        }



        public class PlaylistTrackDisplayItem
        {
            public string Name { get; set; }
            public string Album { get; set; }
            public string Duration { get; set; }
            public string Artists { get; set; }
            public string AddedAt { get; set; }


            // Referencia al objeto completo (PlaylistTrackItem original)
            public PlaylistTrackItem OriginalTrack { get; set; }
        }


        private string CombineArtists(List<string> artists)
        {
            if (artists == null || artists.Count == 0)
                return string.Empty;
            if (artists.Count == 1)
                return artists[0];
            return string.Join(", ", artists.Take(artists.Count - 1)) + " & " + artists.Last();
        }

        private void dtvPlaylistSongs_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Sort the DataGridView by the clicked column
            if (e.ColumnIndex >= 0 && e.ColumnIndex < dtvPlaylistSongs.Columns.Count)
            {
                var column = dtvPlaylistSongs.Columns[e.ColumnIndex];
                if (column.SortMode != DataGridViewColumnSortMode.NotSortable)
                {
                    // Toggle the sort direction
                    ListSortDirection direction = column.HeaderCell.SortGlyphDirection == SortOrder.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
                    dtvPlaylistSongs.Sort(column, direction);
                }
            }
        }

        private void cbExport_SelectedIndexChanged(object sender, EventArgs e)
        {


            string selectedItem = cbExport.SelectedItem.ToString();

            List<PlaylistTrackItem> fullTracks = filteredTracksList
            .Where(item => item.OriginalTrack != null)
            .Select(item => item.OriginalTrack)
            .ToList();



            string filePath = "";
            string selectedtype = "";

            switch (selectedItem)
            {


                case "CSV":
                    filePath = GetSaveFilePath("csv", "CSV files (*.csv)|*.csv|All files (*.*)|*.*", "Save as CSV");
                    selectedtype = "Csv";
                    if (filePath != null)
                    {
                        Exporter.ExportToCsv(fullTracks, filePath);
                    }
                    break;

                case "TXT":
                    filePath = GetSaveFilePath("txt", "Text files (*.txt)|*.txt|All files (*.*)|*.*", "Save as TXT");
                    selectedtype = "Txt";
                    if (filePath != null)
                    {
                        Exporter.ExportToTxt(fullTracks, filePath);
                    }

                    break;

                case "JSON":
                    filePath = GetSaveFilePath("json", "JSON files (*.json)|*.json|All files (*.*)|*.*", "Save as JSON");
                    selectedtype = "Json";
                    if (filePath != null)
                        Exporter.ExportToJson(fullTracks, filePath);
                    break;

                case "XML":
                    filePath = GetSaveFilePath("xml", "XML files (*.xml)|*.xml|All files (*.*)|*.*", "Save as XML");
                    selectedtype = "Xml";
                    if (filePath != null)
                        Exporter.ExportToXml(fullTracks, filePath);
                    break;

                default:
                    MessageBox.Show("Please select a valid export option.", "Invalid Option", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    selectedtype = "None";
                    break;
            }




            if (rdbtnSendGmail.Checked == true && selectedtype != "None")
            {

                //open send email form
                SendGmail sendEmailForm = new SendGmail(filePath);
                sendEmailForm.ShowDialog(this);
                sendEmailForm.BringToFront();
                sendEmailForm.Focus();


                rdbtnSendGmail.Checked = false;


            }
            else if (string.IsNullOrEmpty(filePath) && filePath != null)
            {
                MessageBox.Show($"Export successful:\n{filePath}", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                

                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(filePath) { UseShellExecute = true });

            }

        }






        private string GetSaveFilePath(string extension, string filter, string title)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = filter,
                DefaultExt = extension,
                Title = title
            };

            return dialog.ShowDialog() == DialogResult.OK ? dialog.FileName : null;
        }


        private void PopulateplotArtistsSongsCount()
        {
            // Tomamos los 10 artistas con más canciones
            var topArtists = artistCount
                .OrderByDescending(kv => kv.Value)
                .Take(20)
                .ToList();

            string[] labels = topArtists.Select(kv => kv.Key).ToArray();
            double[] values = topArtists.Select(kv => (double)kv.Value).ToArray();

            // Limpiar el gráfico anterior
            plotArtistsSongsCount.Plot.Clear();

            // Crear barras con posiciones manuales
            for (int i = 0; i < values.Length; i++)
            {
                double pos = i + 1; // posiciones: 1, 2, 3...
                double val = values[i];
                var bar = plotArtistsSongsCount.Plot.Add.Bar(position: pos, value: val);

                // Colores opcionales
                //bar.FillColor = ScottPlot.Colors.Category10[i % 10];
                //bar.BorderColor = ScottPlot.Color.Black;
                //bar. = 1;
            }

            // Crear etiquetas manuales
            ScottPlot.Tick[] ticks = labels
                .Select((label, i) => new ScottPlot.Tick(i + 1, label))
                .ToArray();

            // Aplicar etiquetas en eje X
            plotArtistsSongsCount.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks);
            plotArtistsSongsCount.Plot.Axes.Bottom.TickLabelStyle.FontSize = 14; // Tamaño más grande
            plotArtistsSongsCount.Plot.Axes.Bottom.TickLabelStyle.Rotation = 45; // Rotar para que no se encimen

            // Opcional: márgenes más amplios
            plotArtistsSongsCount.Plot.Axes.Margins(bottom: 0.2, left: 0.15);




            // Títulos
            plotArtistsSongsCount.Plot.Title("Songs per Artist", size: 20);
            plotArtistsSongsCount.Plot.Axes.Left.Label.Text = "Number of Songs";
            plotArtistsSongsCount.Plot.Axes.Left.Label.FontSize = 14;
            plotArtistsSongsCount.Plot.Axes.Bottom.Label.Text = "Artists";
            plotArtistsSongsCount.Plot.Axes.Bottom.Label.FontSize = 14;

            // Refrescar
            plotArtistsSongsCount.Refresh();
        }





        private void btnChangeGraph_Click(object sender, EventArgs e)
        {

            if (plotArtistsSongsCount.Visible == true)
            {
                plotArtistsSongsCount.Visible = false;
                plotPie.Visible = true;
                btnChangeGraph.Text = "Bar Graph";




            }
            else if (plotPie.Visible == true)
            {


                plotArtistsSongsCount.Visible = true;
                plotPie.Visible = false;
                btnChangeGraph.Text = "Pie Chart";
            }
        }


        private async void GetArtistsGenres()
        {
            List<string> artistIds = artists.Values.ToList();
            List<List<string>> idChunks = SplitIntoChunks(artistIds, 50);
            var batchParameters = idChunks.Select(chunk => string.Join(",", chunk)).ToList();

            var artistsData = new ArtistsData(Properties.Settings.Default.AccessToken);
            List<ArtistInfo> artistsInfo = await artistsData.GetArtistsData(batchParameters);

            var topGenres = GetTopGenres(artistsInfo, 10);

            plotPie_Plot(topGenres);

        }


        public static List<List<string>> SplitIntoChunks(List<string> fullList, int chunkSize)
        {
            var chunks = new List<List<string>>();

            for (int i = 0; i < fullList.Count; i += chunkSize)
            {
                chunks.Add(fullList.GetRange(i, Math.Min(chunkSize, fullList.Count - i)));
            }

            return chunks;
        }



        private void plotPie_Plot(Dictionary<string, int> genres)
        {
            // Limpiar el gráfico anterior
            plotPie.Plot.Clear();

            // Tomar los datos y convertir a arrays
            var labels = genres.Keys.ToArray();
            var values = genres.Values.Select(v => (double)v).ToArray();



            // Crear el gráfico de pie (donut)
            var pie = plotPie.Plot.Add.Pie(values);

            double total = pie.Slices.Select(x => x.Value).Sum();
            for (int i = 0; i < pie.Slices.Count; i++)
            {
                pie.Slices[i].LabelFontSize = 20;
                pie.Slices[i].Label = $"{labels[i]}";
                pie.Slices[i].LegendText = $"{labels[i]} " +
                    $"({pie.Slices[i].Value / total:p1})";
            }



            plotPie.Plot.Legend.Alignment = ScottPlot.Alignment.UpperRight;
            plotPie.Plot.Legend.FontSize = 12;
            plotPie.Plot.Legend.BackgroundFill.Color = ScottPlot.Color.FromHex("#f0f0f0");

            // Títulos
            plotPie.Plot.Title("Top Genres in Playlist", size: 20);





            // Refrescar el control
            plotPie.Refresh();
        }

        public static Dictionary<string, int> GetTopGenres(List<ArtistInfo> artists, int topN)
        {
            // 1. Aplanar todos los géneros de todos los artistas
            var allGenres = artists
                .Where(a => a.Genres != null)
                .SelectMany(a => a.Genres)
                .ToList();

            if (!allGenres.Any())
                return new Dictionary<string, int>();

            // 2. Contar frecuencias y ordenar
            var genreCounts = allGenres
                .GroupBy(g => g.ToLower().Trim()) // Normalizar (evitar "Rock" vs "rock")
                .ToDictionary(
                    g => g.Key,
                    g => g.Count()
                );

            // 3. Seleccionar los topN géneros más comunes
            var topGenres = genreCounts
                .OrderByDescending(g => g.Value)
                .Take(topN)
                .ToDictionary(g => g.Key, g => g.Value);

            // 4. Agregar "Otros" con la suma del resto
            //int othersCount = genreCounts.Sum(g => g.Value) - topGenres.Sum(g => g.Value);
            //if (othersCount > 0)
            //{
            //    topGenres.Add("Otros", othersCount);
            //}

            return topGenres;
        }

        private void btnCompareData_Click(object sender, EventArgs e)
        {

            if (filteredTracksList == top100kSongs)
            {
                return;
            }


            HashSet<string> topSongs = new HashSet<string>();

            using (var reader = new StreamReader("100k_Dataset.csv"))
            {
                reader.ReadLine(); // Saltar encabezado

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var parts = line.Split(',');

                    if (parts.Length >= 2)
                    {
                        string artist = parts[0].Trim().ToLower();
                        string track = parts[1].Trim().ToLower();
                        string clave = track + "|" + artist;
                        topSongs.Add(clave);
                    }
                }
            }


            var songs = new List<PlaylistTrackDisplayItem>();
            top100kSongs = new SortableBindingList<PlaylistTrackDisplayItem>(songs);

            foreach (var track in filteredTracksList)
            {
                string trackName = track.Name.Trim().ToLower();
                string artistName = track.Artists.Trim().ToLower();

                var artistas = artistName.Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
                bool match = false;

                foreach (var artista in artistas)
                {
                    string clave = trackName + "|" + artista.Trim();
                    if (topSongs.Contains(clave))
                    {
                        match = true;
                        break;
                    }
                }

                if (match)
                {
                    top100kSongs.Add(track);
                }
            }

            // Paso 3: Actualizar el listado y el DataGridView
            filteredTracksList = top100kSongs;
            dtvPlaylistSongs.DataSource = null; // Esto a veces ayuda a refrescar
            dtvPlaylistSongs.DataSource = filteredTracksList;





        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {

            //if the dtvPlaylistSongs is not visible
            if (dgvtop100k.Visible == true)
            {
                //hide the dgvtop100k
                dgvtop100k.Visible = false;
                dtvPlaylistSongs.Visible = true;
                btnViewOption.Enabled = true;
                btnCompareData.Enabled = true;
                btnSeeTop100k.Enabled = true;
                btnPrevious.Enabled = true;
                btnChangeGraph.Enabled = true;
                rdbtnSendGmail.Enabled = true;
                cbExport.Enabled = true;
            }


            SortableBindingList<PlaylistTrackDisplayItem> comparer = new SortableBindingList<PlaylistTrackDisplayItem>(songs);
            if (filteredTracksList == comparer)
            {
                return;
            }
            filteredTracksList = comparer;
            dtvPlaylistSongs.DataSource = null;
            dtvPlaylistSongs.DataSource = filteredTracksList;
        }

        private void btnSeeTop100k_Click(object sender, EventArgs e)
        {

           if (dgvtop100k.Visible == true)
            {
               
                return;
            }

            //hide the dtvPlaylistSongs
            dtvPlaylistSongs.Visible = false;

            CargarDatasetTop100k();
            
           dgvtop100k.Visible = true;
            //unnable the search bar and the export options
            txtboxSearch.Enabled = false;
            cbExport.Enabled = false;
            btnViewOption.Enabled = false;
            btnCompareData.Enabled = false;
            btnSeeTop100k.Enabled = false;
            btnPrevious.Enabled = true;
            btnChangeGraph.Enabled = false;
            rdbtnSendGmail.Enabled = false;
            cbExport.Enabled = false;





        }



        public class TopSongEntry
        {
            public string Artist { get; set; }
            public string TrackName { get; set; }
        }


        private void CargarDatasetTop100k()
        {
            var topList = new List<TopSongEntry>();

            using (var reader = new StreamReader("100k_Dataset.csv"))
            {
                reader.ReadLine(); // Saltar encabezado

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var parts = line.Split(',');

                    if (parts.Length >= 2)
                    {
                        string artist = parts[0].Trim();
                        string trackName = parts[1].Trim();

                        topList.Add(new TopSongEntry
                        {
                            Artist = artist,
                            TrackName = trackName
                        });
                    }
                }
            }

            dgvtop100k.DataSource = topList;
        }

    }
}
