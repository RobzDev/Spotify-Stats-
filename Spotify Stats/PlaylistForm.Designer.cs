namespace Spotify_Stats
{
    partial class PlaylistForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblUsername = new Label();
            pboxUserPhoto = new PictureBox();
            pbPlaylistImage = new PictureBox();
            lblPlaylistName = new Label();
            cbExport = new ComboBox();
            dtvPlaylistSongs = new DataGridView();
            tvSongs = new TreeView();
            btnViewOption = new Button();
            txtboxSearch = new TextBox();
            _searchDelayTimer = new System.Windows.Forms.Timer(components);
            txtboxRawTxt = new TextBox();
            lblExport = new Label();
            plotArtistsSongsCount = new ScottPlot.WinForms.FormsPlot();
            btnChangeGraph = new Button();
            plotPie = new ScottPlot.WinForms.FormsPlot();
            rdbtnSendGmail = new RadioButton();
            btnCompareData = new Button();
            btnPrevious = new Button();
            btnSeeTop100k = new Button();
            dgvtop100k = new DataGridView();
            btnDatabase = new Button();
            panelDatabase = new Panel();
            cbTablesInDataBase = new ComboBox();
            btnBringTable = new Button();
            btnCreateTable = new Button();
            ((System.ComponentModel.ISupportInitialize)pboxUserPhoto).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPlaylistImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dtvPlaylistSongs).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvtop100k).BeginInit();
            panelDatabase.SuspendLayout();
            SuspendLayout();
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Yu Gothic", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUsername.ForeColor = Color.Green;
            lblUsername.Location = new Point(79, 25);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(80, 25);
            lblUsername.TabIndex = 2;
            lblUsername.Text = "Default";
            // 
            // pboxUserPhoto
            // 
            pboxUserPhoto.Location = new Point(12, 12);
            pboxUserPhoto.Name = "pboxUserPhoto";
            pboxUserPhoto.Size = new Size(61, 48);
            pboxUserPhoto.TabIndex = 3;
            pboxUserPhoto.TabStop = false;
            // 
            // pbPlaylistImage
            // 
            pbPlaylistImage.Location = new Point(221, 62);
            pbPlaylistImage.Name = "pbPlaylistImage";
            pbPlaylistImage.Size = new Size(227, 208);
            pbPlaylistImage.SizeMode = PictureBoxSizeMode.StretchImage;
            pbPlaylistImage.TabIndex = 4;
            pbPlaylistImage.TabStop = false;
            // 
            // lblPlaylistName
            // 
            lblPlaylistName.AutoSize = true;
            lblPlaylistName.Font = new Font("Yu Gothic UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPlaylistName.ForeColor = Color.Green;
            lblPlaylistName.Location = new Point(286, 273);
            lblPlaylistName.Name = "lblPlaylistName";
            lblPlaylistName.Size = new Size(86, 37);
            lblPlaylistName.TabIndex = 5;
            lblPlaylistName.Text = "label1";
            // 
            // cbExport
            // 
            cbExport.FormattingEnabled = true;
            cbExport.Items.AddRange(new object[] { "TXT", "JSON", "XML", "CSV" });
            cbExport.Location = new Point(50, 291);
            cbExport.Name = "cbExport";
            cbExport.Size = new Size(75, 23);
            cbExport.TabIndex = 7;
            cbExport.SelectedIndexChanged += cbExport_SelectedIndexChanged;
            // 
            // dtvPlaylistSongs
            // 
            dtvPlaylistSongs.AllowUserToAddRows = false;
            dtvPlaylistSongs.AllowUserToDeleteRows = false;
            dtvPlaylistSongs.AllowUserToOrderColumns = true;
            dtvPlaylistSongs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtvPlaylistSongs.Location = new Point(50, 318);
            dtvPlaylistSongs.Name = "dtvPlaylistSongs";
            dtvPlaylistSongs.ReadOnly = true;
            dtvPlaylistSongs.Size = new Size(672, 559);
            dtvPlaylistSongs.TabIndex = 8;
            dtvPlaylistSongs.ColumnHeaderMouseDoubleClick += dtvPlaylistSongs_ColumnHeaderMouseDoubleClick;
            // 
            // tvSongs
            // 
            tvSongs.Location = new Point(50, 318);
            tvSongs.Name = "tvSongs";
            tvSongs.Size = new Size(672, 559);
            tvSongs.TabIndex = 9;
            tvSongs.Visible = false;
            // 
            // btnViewOption
            // 
            btnViewOption.Location = new Point(470, 290);
            btnViewOption.Name = "btnViewOption";
            btnViewOption.Size = new Size(73, 22);
            btnViewOption.TabIndex = 10;
            btnViewOption.Text = "Tree View";
            btnViewOption.UseVisualStyleBackColor = true;
            btnViewOption.Click += btnViewOption_Click;
            // 
            // txtboxSearch
            // 
            txtboxSearch.Location = new Point(564, 289);
            txtboxSearch.Name = "txtboxSearch";
            txtboxSearch.Size = new Size(158, 23);
            txtboxSearch.TabIndex = 11;
            txtboxSearch.TextChanged += txtboxSearch_TextChanged;
            // 
            // _searchDelayTimer
            // 
            _searchDelayTimer.Interval = 300;
            _searchDelayTimer.Tick += _searchDelayTimer_Tick;
            // 
            // txtboxRawTxt
            // 
            txtboxRawTxt.Location = new Point(50, 317);
            txtboxRawTxt.Multiline = true;
            txtboxRawTxt.Name = "txtboxRawTxt";
            txtboxRawTxt.Size = new Size(672, 557);
            txtboxRawTxt.TabIndex = 12;
            txtboxRawTxt.Visible = false;
            // 
            // lblExport
            // 
            lblExport.AutoSize = true;
            lblExport.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblExport.ForeColor = Color.Green;
            lblExport.Location = new Point(53, 270);
            lblExport.Name = "lblExport";
            lblExport.Size = new Size(60, 18);
            lblExport.TabIndex = 13;
            lblExport.Text = "Export";
            // 
            // plotArtistsSongsCount
            // 
            plotArtistsSongsCount.DisplayScale = 1F;
            plotArtistsSongsCount.Location = new Point(783, 317);
            plotArtistsSongsCount.Name = "plotArtistsSongsCount";
            plotArtistsSongsCount.Size = new Size(538, 366);
            plotArtistsSongsCount.TabIndex = 14;
            // 
            // btnChangeGraph
            // 
            btnChangeGraph.Location = new Point(1012, 288);
            btnChangeGraph.Name = "btnChangeGraph";
            btnChangeGraph.Size = new Size(126, 23);
            btnChangeGraph.TabIndex = 15;
            btnChangeGraph.Text = "Change Graph ";
            btnChangeGraph.UseVisualStyleBackColor = true;
            btnChangeGraph.Click += btnChangeGraph_Click;
            // 
            // plotPie
            // 
            plotPie.DisplayScale = 1F;
            plotPie.Location = new Point(783, 318);
            plotPie.Name = "plotPie";
            plotPie.Size = new Size(538, 365);
            plotPie.TabIndex = 16;
            plotPie.Visible = false;
            // 
            // rdbtnSendGmail
            // 
            rdbtnSendGmail.AutoSize = true;
            rdbtnSendGmail.ForeColor = Color.Green;
            rdbtnSendGmail.Location = new Point(144, 292);
            rdbtnSendGmail.Name = "rdbtnSendGmail";
            rdbtnSendGmail.Size = new Size(85, 19);
            rdbtnSendGmail.TabIndex = 17;
            rdbtnSendGmail.Text = "Send Gmail";
            rdbtnSendGmail.UseVisualStyleBackColor = true;
            // 
            // btnCompareData
            // 
            btnCompareData.Location = new Point(564, 250);
            btnCompareData.Name = "btnCompareData";
            btnCompareData.Size = new Size(75, 23);
            btnCompareData.TabIndex = 18;
            btnCompareData.Text = "100k Top";
            btnCompareData.UseVisualStyleBackColor = true;
            btnCompareData.Click += btnCompareData_Click;
            // 
            // btnPrevious
            // 
            btnPrevious.Location = new Point(564, 221);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Size = new Size(156, 23);
            btnPrevious.TabIndex = 19;
            btnPrevious.Text = "Previous";
            btnPrevious.UseVisualStyleBackColor = true;
            btnPrevious.Click += btnPrevious_Click;
            // 
            // btnSeeTop100k
            // 
            btnSeeTop100k.Location = new Point(647, 250);
            btnSeeTop100k.Name = "btnSeeTop100k";
            btnSeeTop100k.Size = new Size(75, 23);
            btnSeeTop100k.TabIndex = 20;
            btnSeeTop100k.Text = "See 100k";
            btnSeeTop100k.UseVisualStyleBackColor = true;
            btnSeeTop100k.Click += btnSeeTop100k_Click;
            // 
            // dgvtop100k
            // 
            dgvtop100k.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvtop100k.Location = new Point(207, 318);
            dgvtop100k.Name = "dgvtop100k";
            dgvtop100k.ReadOnly = true;
            dgvtop100k.Size = new Size(256, 560);
            dgvtop100k.TabIndex = 21;
            dgvtop100k.Visible = false;
            // 
            // btnDatabase
            // 
            btnDatabase.Location = new Point(553, 192);
            btnDatabase.Name = "btnDatabase";
            btnDatabase.Size = new Size(195, 23);
            btnDatabase.TabIndex = 22;
            btnDatabase.Text = "Connect to Data Base";
            btnDatabase.UseVisualStyleBackColor = true;
            btnDatabase.Click += btnDatabase_Click;
            // 
            // panelDatabase
            // 
            panelDatabase.Controls.Add(cbTablesInDataBase);
            panelDatabase.Controls.Add(btnBringTable);
            panelDatabase.Controls.Add(btnCreateTable);
            panelDatabase.Location = new Point(548, 86);
            panelDatabase.Name = "panelDatabase";
            panelDatabase.Size = new Size(200, 100);
            panelDatabase.TabIndex = 23;
            panelDatabase.Visible = false;
            // 
            // cbTablesInDataBase
            // 
            cbTablesInDataBase.FormattingEnabled = true;
            cbTablesInDataBase.Location = new Point(42, 26);
            cbTablesInDataBase.Name = "cbTablesInDataBase";
            cbTablesInDataBase.Size = new Size(121, 23);
            cbTablesInDataBase.TabIndex = 2;
            cbTablesInDataBase.Visible = false;
            cbTablesInDataBase.SelectedIndexChanged += cbTablesInDataBase_SelectedIndexChanged;
            // 
            // btnBringTable
            // 
            btnBringTable.Location = new Point(122, 74);
            btnBringTable.Name = "btnBringTable";
            btnBringTable.Size = new Size(75, 23);
            btnBringTable.TabIndex = 1;
            btnBringTable.Text = "Bring Table";
            btnBringTable.UseVisualStyleBackColor = true;
            btnBringTable.Click += btnBringTable_Click;
            // 
            // btnCreateTable
            // 
            btnCreateTable.Location = new Point(3, 74);
            btnCreateTable.Name = "btnCreateTable";
            btnCreateTable.Size = new Size(100, 23);
            btnCreateTable.TabIndex = 0;
            btnCreateTable.Text = "Create Table";
            btnCreateTable.UseVisualStyleBackColor = true;
            btnCreateTable.Click += btnCreateTable_Click;
            // 
            // PlaylistForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(1531, 927);
            Controls.Add(panelDatabase);
            Controls.Add(btnDatabase);
            Controls.Add(btnSeeTop100k);
            Controls.Add(btnPrevious);
            Controls.Add(btnCompareData);
            Controls.Add(rdbtnSendGmail);
            Controls.Add(plotPie);
            Controls.Add(btnChangeGraph);
            Controls.Add(plotArtistsSongsCount);
            Controls.Add(lblExport);
            Controls.Add(dtvPlaylistSongs);
            Controls.Add(txtboxSearch);
            Controls.Add(btnViewOption);
            Controls.Add(tvSongs);
            Controls.Add(cbExport);
            Controls.Add(lblPlaylistName);
            Controls.Add(pbPlaylistImage);
            Controls.Add(pboxUserPhoto);
            Controls.Add(lblUsername);
            Controls.Add(txtboxRawTxt);
            Controls.Add(dgvtop100k);
            Name = "PlaylistForm";
            Text = "Playlist";
            FormClosed += PlaylistForm_FormClosed;
            ((System.ComponentModel.ISupportInitialize)pboxUserPhoto).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPlaylistImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)dtvPlaylistSongs).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvtop100k).EndInit();
            panelDatabase.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblUsername;
        private PictureBox pboxUserPhoto;
        private PictureBox pbPlaylistImage;
        private Label lblPlaylistName;
        private ComboBox cbExport;
        private DataGridView dtvPlaylistSongs;
        private TreeView tvSongs;
        private Button btnViewOption;
        private TextBox txtboxSearch;
        private System.Windows.Forms.Timer _searchDelayTimer;
        private TextBox txtboxRawTxt;
        private Label lblExport;
        private ScottPlot.WinForms.FormsPlot plotArtistsSongsCount;
        private Button btnChangeGraph;
        private ScottPlot.WinForms.FormsPlot plotPie;
        private RadioButton rdbtnSendGmail;
        private Button btnCompareData;
        private Button btnPrevious;
        private Button btnSeeTop100k;
        private DataGridView dgvtop100k;
        private Button btnDatabase;
        private Panel panelDatabase;
        private Button btnBringTable;
        private Button btnCreateTable;
        private ComboBox cbTablesInDataBase;
    }
}