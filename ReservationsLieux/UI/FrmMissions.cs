using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ReservationsLieux.Domain;
using ReservationsLieux.Repositories;

namespace ReservationsLieux.UI
{
    public class FrmMissions : UserControl
    {
        private readonly LieuRepository _lieuRepository = new LieuRepository();
        private readonly PersonnageRepository _personnageRepository = new PersonnageRepository();
        private readonly MissionRepository _missionRepository = new MissionRepository();

        private ComboBox cbLieu;
        private ComboBox cbPersonnage;
        private DateTimePicker dtpDebut;
        private DateTimePicker dtpFin;
        private Button btnCreer;

        private CheckBox chkPeriode;
        private DateTimePicker dtpRechercheDebut;
        private DateTimePicker dtpRechercheFin;
        private ComboBox cbRoyaume;
        private ComboBox cbRace;
        private Button btnRechercher;
        private DataGridView dgvResultats;

        private DataGridView dgvMissions;
        private GroupBox grpCreation;
        private Label lblLieu;
        private Label lblPersonnage;
        private Label lblDebut;
        private Label lblFin;
        private GroupBox grpRecherche;
        private Panel pnlFiltres;
        private Label lblRoyaume;
        private Label lblRace;
        private GroupBox grpListe;
        private Panel pnlListeHaut;
        private Button btnActualiser;

        public FrmMissions()
        {
            InitializeComponent();
            Rafraichir();
        }

        private void InitializeComponent()
        {
            grpCreation = new GroupBox();
            lblLieu = new Label();
            cbLieu = new ComboBox();
            lblPersonnage = new Label();
            cbPersonnage = new ComboBox();
            lblDebut = new Label();
            dtpDebut = new DateTimePicker();
            lblFin = new Label();
            dtpFin = new DateTimePicker();
            btnCreer = new Button();
            grpRecherche = new GroupBox();
            dgvResultats = new DataGridView();
            pnlFiltres = new Panel();
            chkPeriode = new CheckBox();
            dtpRechercheDebut = new DateTimePicker();
            dtpRechercheFin = new DateTimePicker();
            lblRoyaume = new Label();
            cbRoyaume = new ComboBox();
            lblRace = new Label();
            cbRace = new ComboBox();
            btnRechercher = new Button();
            grpListe = new GroupBox();
            dgvMissions = new DataGridView();
            pnlListeHaut = new Panel();
            btnActualiser = new Button();
            grpCreation.SuspendLayout();
            grpRecherche.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResultats).BeginInit();
            pnlFiltres.SuspendLayout();
            grpListe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMissions).BeginInit();
            pnlListeHaut.SuspendLayout();
            SuspendLayout();
            // 
            // grpCreation
            // 
            grpCreation.Controls.Add(lblLieu);
            grpCreation.Controls.Add(cbLieu);
            grpCreation.Controls.Add(lblPersonnage);
            grpCreation.Controls.Add(cbPersonnage);
            grpCreation.Controls.Add(lblDebut);
            grpCreation.Controls.Add(dtpDebut);
            grpCreation.Controls.Add(lblFin);
            grpCreation.Controls.Add(dtpFin);
            grpCreation.Controls.Add(btnCreer);
            grpCreation.Dock = DockStyle.Top;
            grpCreation.Location = new Point(8, 8);
            grpCreation.Name = "grpCreation";
            grpCreation.Padding = new Padding(12);
            grpCreation.Size = new Size(1617, 130);
            grpCreation.TabIndex = 2;
            grpCreation.TabStop = false;
            grpCreation.Text = "Créer une mission";
            // 
            // lblLieu
            // 
            lblLieu.AutoSize = true;
            lblLieu.Location = new Point(16, 28);
            lblLieu.Name = "lblLieu";
            lblLieu.Size = new Size(29, 15);
            lblLieu.TabIndex = 0;
            lblLieu.Text = "Lieu";
            // 
            // cbLieu
            // 
            cbLieu.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLieu.Location = new Point(16, 48);
            cbLieu.Name = "cbLieu";
            cbLieu.Size = new Size(220, 23);
            cbLieu.TabIndex = 1;
            // 
            // lblPersonnage
            // 
            lblPersonnage.AutoSize = true;
            lblPersonnage.Location = new Point(252, 28);
            lblPersonnage.Name = "lblPersonnage";
            lblPersonnage.Size = new Size(69, 15);
            lblPersonnage.TabIndex = 2;
            lblPersonnage.Text = "Personnage";
            // 
            // cbPersonnage
            // 
            cbPersonnage.DropDownStyle = ComboBoxStyle.DropDownList;
            cbPersonnage.Location = new Point(252, 48);
            cbPersonnage.Name = "cbPersonnage";
            cbPersonnage.Size = new Size(220, 23);
            cbPersonnage.TabIndex = 3;
            // 
            // lblDebut
            // 
            lblDebut.AutoSize = true;
            lblDebut.Location = new Point(488, 28);
            lblDebut.Name = "lblDebut";
            lblDebut.Size = new Size(65, 15);
            lblDebut.TabIndex = 4;
            lblDebut.Text = "Date début";
            // 
            // dtpDebut
            // 
            dtpDebut.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpDebut.Format = DateTimePickerFormat.Custom;
            dtpDebut.Location = new Point(488, 48);
            dtpDebut.Name = "dtpDebut";
            dtpDebut.Size = new Size(170, 23);
            dtpDebut.TabIndex = 5;
            dtpDebut.Value = new DateTime(2026, 10, 7, 8, 0, 0, 0);
            // 
            // lblFin
            // 
            lblFin.AutoSize = true;
            lblFin.Location = new Point(674, 28);
            lblFin.Name = "lblFin";
            lblFin.Size = new Size(48, 15);
            lblFin.TabIndex = 6;
            lblFin.Text = "Date fin";
            // 
            // dtpFin
            // 
            dtpFin.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpFin.Format = DateTimePickerFormat.Custom;
            dtpFin.Location = new Point(674, 48);
            dtpFin.Name = "dtpFin";
            dtpFin.Size = new Size(170, 23);
            dtpFin.TabIndex = 7;
            dtpFin.Value = new DateTime(2026, 10, 7, 18, 0, 0, 0);
            // 
            // btnCreer
            // 
            btnCreer.Location = new Point(860, 44);
            btnCreer.Name = "btnCreer";
            btnCreer.Size = new Size(160, 32);
            btnCreer.TabIndex = 8;
            btnCreer.Text = "Créer la mission";
            btnCreer.Click += BtnCreer_Click;
            // 
            // grpRecherche
            // 
            grpRecherche.Controls.Add(dgvResultats);
            grpRecherche.Controls.Add(pnlFiltres);
            grpRecherche.Dock = DockStyle.Top;
            grpRecherche.Location = new Point(8, 138);
            grpRecherche.Name = "grpRecherche";
            grpRecherche.Padding = new Padding(10);
            grpRecherche.Size = new Size(1617, 280);
            grpRecherche.TabIndex = 1;
            grpRecherche.TabStop = false;
            grpRecherche.Text = "Recherche filtrée (mode connecté)";
            // 
            // dgvResultats
            // 
            dgvResultats.AllowUserToAddRows = false;
            dgvResultats.AllowUserToDeleteRows = false;
            dgvResultats.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvResultats.BackgroundColor = Color.White;
            dgvResultats.Dock = DockStyle.Fill;
            dgvResultats.Location = new Point(10, 82);
            dgvResultats.MultiSelect = false;
            dgvResultats.Name = "dgvResultats";
            dgvResultats.ReadOnly = true;
            dgvResultats.RowHeadersVisible = false;
            dgvResultats.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvResultats.Size = new Size(1597, 188);
            dgvResultats.TabIndex = 0;
            // 
            // pnlFiltres
            // 
            pnlFiltres.Controls.Add(chkPeriode);
            pnlFiltres.Controls.Add(dtpRechercheDebut);
            pnlFiltres.Controls.Add(dtpRechercheFin);
            pnlFiltres.Controls.Add(lblRoyaume);
            pnlFiltres.Controls.Add(cbRoyaume);
            pnlFiltres.Controls.Add(lblRace);
            pnlFiltres.Controls.Add(cbRace);
            pnlFiltres.Controls.Add(btnRechercher);
            pnlFiltres.Dock = DockStyle.Top;
            pnlFiltres.Location = new Point(10, 26);
            pnlFiltres.Name = "pnlFiltres";
            pnlFiltres.Size = new Size(1597, 56);
            pnlFiltres.TabIndex = 1;
            // 
            // chkPeriode
            // 
            chkPeriode.AutoSize = true;
            chkPeriode.Location = new Point(6, 18);
            chkPeriode.Name = "chkPeriode";
            chkPeriode.Size = new Size(119, 19);
            chkPeriode.TabIndex = 0;
            chkPeriode.Text = "Filtrer par période";
            chkPeriode.CheckedChanged += ChkPeriode_CheckedChanged;
            // 
            // dtpRechercheDebut
            // 
            dtpRechercheDebut.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpRechercheDebut.Enabled = false;
            dtpRechercheDebut.Format = DateTimePickerFormat.Custom;
            dtpRechercheDebut.Location = new Point(160, 16);
            dtpRechercheDebut.Name = "dtpRechercheDebut";
            dtpRechercheDebut.Size = new Size(170, 23);
            dtpRechercheDebut.TabIndex = 1;
            dtpRechercheDebut.Value = new DateTime(2026, 9, 1, 0, 0, 0, 0);
            // 
            // dtpRechercheFin
            // 
            dtpRechercheFin.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpRechercheFin.Enabled = false;
            dtpRechercheFin.Format = DateTimePickerFormat.Custom;
            dtpRechercheFin.Location = new Point(340, 16);
            dtpRechercheFin.Name = "dtpRechercheFin";
            dtpRechercheFin.Size = new Size(170, 23);
            dtpRechercheFin.TabIndex = 2;
            dtpRechercheFin.Value = new DateTime(2026, 9, 30, 23, 59, 0, 0);
            // 
            // lblRoyaume
            // 
            lblRoyaume.AutoSize = true;
            lblRoyaume.Location = new Point(528, 0);
            lblRoyaume.Name = "lblRoyaume";
            lblRoyaume.Size = new Size(57, 15);
            lblRoyaume.TabIndex = 3;
            lblRoyaume.Text = "Royaume";
            // 
            // cbRoyaume
            // 
            cbRoyaume.DropDownStyle = ComboBoxStyle.DropDownList;
            cbRoyaume.Location = new Point(528, 16);
            cbRoyaume.Name = "cbRoyaume";
            cbRoyaume.Size = new Size(150, 23);
            cbRoyaume.TabIndex = 4;
            // 
            // lblRace
            // 
            lblRace.AutoSize = true;
            lblRace.Location = new Point(690, 0);
            lblRace.Name = "lblRace";
            lblRace.Size = new Size(32, 15);
            lblRace.TabIndex = 5;
            lblRace.Text = "Race";
            // 
            // cbRace
            // 
            cbRace.DropDownStyle = ComboBoxStyle.DropDownList;
            cbRace.Location = new Point(690, 16);
            cbRace.Name = "cbRace";
            cbRace.Size = new Size(140, 23);
            cbRace.TabIndex = 6;
            // 
            // btnRechercher
            // 
            btnRechercher.Location = new Point(848, 14);
            btnRechercher.Name = "btnRechercher";
            btnRechercher.Size = new Size(140, 28);
            btnRechercher.TabIndex = 7;
            btnRechercher.Text = "Rechercher";
            btnRechercher.Click += BtnRechercher_Click;
            // 
            // grpListe
            // 
            grpListe.Controls.Add(dgvMissions);
            grpListe.Controls.Add(pnlListeHaut);
            grpListe.Dock = DockStyle.Fill;
            grpListe.Location = new Point(8, 418);
            grpListe.Name = "grpListe";
            grpListe.Padding = new Padding(10);
            grpListe.Size = new Size(1617, 259);
            grpListe.TabIndex = 0;
            grpListe.TabStop = false;
            grpListe.Text = "Toutes les missions (mode déconnecté)";
            // 
            // dgvMissions
            // 
            dgvMissions.AllowUserToAddRows = false;
            dgvMissions.AllowUserToDeleteRows = false;
            dgvMissions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMissions.BackgroundColor = Color.White;
            dgvMissions.Dock = DockStyle.Fill;
            dgvMissions.Location = new Point(10, 66);
            dgvMissions.MultiSelect = false;
            dgvMissions.Name = "dgvMissions";
            dgvMissions.ReadOnly = true;
            dgvMissions.RowHeadersVisible = false;
            dgvMissions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMissions.Size = new Size(1597, 183);
            dgvMissions.TabIndex = 0;
            // 
            // pnlListeHaut
            // 
            pnlListeHaut.Controls.Add(btnActualiser);
            pnlListeHaut.Dock = DockStyle.Top;
            pnlListeHaut.Location = new Point(10, 26);
            pnlListeHaut.Name = "pnlListeHaut";
            pnlListeHaut.Size = new Size(1597, 40);
            pnlListeHaut.TabIndex = 1;
            // 
            // btnActualiser
            // 
            btnActualiser.Location = new Point(6, 4);
            btnActualiser.Name = "btnActualiser";
            btnActualiser.Size = new Size(160, 28);
            btnActualiser.TabIndex = 0;
            btnActualiser.Text = "Actualiser la liste";
            btnActualiser.Click += BtnActualiser_Click;
            // 
            // FrmMissions
            // 
            Controls.Add(grpListe);
            Controls.Add(grpRecherche);
            Controls.Add(grpCreation);
            Name = "FrmMissions";
            Padding = new Padding(8);
            Size = new Size(1633, 685);
            grpCreation.ResumeLayout(false);
            grpCreation.PerformLayout();
            grpRecherche.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvResultats).EndInit();
            pnlFiltres.ResumeLayout(false);
            pnlFiltres.PerformLayout();
            grpListe.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMissions).EndInit();
            pnlListeHaut.ResumeLayout(false);
            ResumeLayout(false);
        }

        public void Rafraichir()
        {
            ChargerCombos();
            ChargerListeGlobale();
        }

        private void ChargerCombos()
        {
            try
            {
                DataTable lieux = _lieuRepository.ObtenirTous();
                cbLieu.DisplayMember = "NomLieu";
                cbLieu.ValueMember = "IdLieu";
                cbLieu.DataSource = lieux;

                DataTable personnages = _personnageRepository.ObtenirTous();
                cbPersonnage.DisplayMember = "Nom";
                cbPersonnage.ValueMember = "IdPersonnage";
                cbPersonnage.DataSource = personnages;

                List<string> royaumes = _lieuRepository.ObtenirRoyaumes();
                cbRoyaume.Items.Clear();
                cbRoyaume.Items.Add("(tous)");
                foreach (string nom in royaumes)
                {
                    cbRoyaume.Items.Add(nom);
                }
                cbRoyaume.SelectedIndex = 0;

                List<string> races = _personnageRepository.ObtenirRaces();
                cbRace.Items.Clear();
                cbRace.Items.Add("(toutes)");
                foreach (string nom in races)
                {
                    cbRace.Items.Add(nom);
                }
                cbRace.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible de charger les listes." + Environment.NewLine + ex.Message,
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChargerListeGlobale()
        {
            try
            {
                DataTable table = _missionRepository.ObtenirTous();
                dgvMissions.DataSource = table;
                ConfigurerColonnesMissions(dgvMissions);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible de charger les missions." + Environment.NewLine + ex.Message,
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurerColonnesMissions(DataGridView grille)
        {
            CacherColonne(grille, "IdMission");
            CacherColonne(grille, "IdLieu");
            CacherColonne(grille, "IdPersonnage");
            RenommerColonne(grille, "NomLieu", "Lieu");
            RenommerColonne(grille, "NomPersonnage", "Personnage");
            RenommerColonne(grille, "Royaume", "Royaume");
            RenommerColonne(grille, "Race", "Race");
            RenommerColonne(grille, "DateDebut", "Début");
            RenommerColonne(grille, "DateFin", "Fin");

            if (grille.Columns["DateDebut"] != null)
            {
                grille.Columns["DateDebut"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
            if (grille.Columns["DateFin"] != null)
            {
                grille.Columns["DateFin"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
        }

        private static void CacherColonne(DataGridView grille, string nom)
        {
            if (grille.Columns[nom] != null)
            {
                grille.Columns[nom].Visible = false;
            }
        }

        private static void RenommerColonne(DataGridView grille, string nom, string titre)
        {
            if (grille.Columns[nom] != null)
            {
                grille.Columns[nom].HeaderText = titre;
            }
        }

        private void ChkPeriode_CheckedChanged(object sender, EventArgs e)
        {
            dtpRechercheDebut.Enabled = chkPeriode.Checked;
            dtpRechercheFin.Enabled = chkPeriode.Checked;
        }

        private void BtnCreer_Click(object sender, EventArgs e)
        {
            if (cbLieu.SelectedValue == null || cbPersonnage.SelectedValue == null)
            {
                MessageBox.Show("Choisissez un lieu et un personnage.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime debut = dtpDebut.Value;
            DateTime fin = dtpFin.Value;

            if (fin <= debut)
            {
                MessageBox.Show("La date de fin doit être postérieure à la date de début.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idLieu = Convert.ToInt32(cbLieu.SelectedValue);
            int idPersonnage = Convert.ToInt32(cbPersonnage.SelectedValue);
            string nomLieu = cbLieu.Text;

            try
            {
                List<Mission> conflits = _missionRepository.ObtenirConflits(idLieu, debut, fin);
                if (conflits.Count > 0)
                {
                    StringBuilder message = new StringBuilder();
                    message.AppendLine("Impossible d'enregistrer cette mission.");
                    message.AppendLine();
                    message.AppendLine("Le lieu « " + nomLieu + " » est déjà réservé sur une plage qui chevauche :");
                    message.AppendLine(debut.ToString("dd/MM/yyyy HH:mm") + " → " + fin.ToString("dd/MM/yyyy HH:mm"));
                    message.AppendLine();
                    foreach (Mission conflit in conflits)
                    {
                        message.AppendLine("- " + conflit.NomPersonnage + " : " +
                            conflit.DateDebut.ToString("dd/MM/yyyy HH:mm") + " → " +
                            conflit.DateFin.ToString("dd/MM/yyyy HH:mm"));
                    }

                    MessageBox.Show(message.ToString(), "Conflit temporel",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Mission mission = new Mission
                {
                    IdLieu = idLieu,
                    IdPersonnage = idPersonnage,
                    DateDebut = debut,
                    DateFin = fin
                };

                _missionRepository.Creer(mission);
                ChargerListeGlobale();
                MessageBox.Show("La mission a été créée.", "Mission",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la création de la mission." + Environment.NewLine + ex.Message,
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRechercher_Click(object sender, EventArgs e)
        {
            DateTime? dateMin = null;
            DateTime? dateMax = null;

            if (chkPeriode.Checked)
            {
                if (dtpRechercheFin.Value <= dtpRechercheDebut.Value)
                {
                    MessageBox.Show("Dans la recherche, la date de fin doit être postérieure à la date de début.",
                        "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                dateMin = dtpRechercheDebut.Value;
                dateMax = dtpRechercheFin.Value;
            }

            string royaume = null;
            if (cbRoyaume.SelectedIndex > 0)
            {
                royaume = cbRoyaume.SelectedItem.ToString();
            }

            string race = null;
            if (cbRace.SelectedIndex > 0)
            {
                race = cbRace.SelectedItem.ToString();
            }

            try
            {
                DataTable resultats = _missionRepository.Rechercher(dateMin, dateMax, royaume, race);
                dgvResultats.DataSource = resultats;
                ConfigurerColonnesMissions(dgvResultats);

                if (resultats.Rows.Count == 0)
                {
                    MessageBox.Show("Aucune mission ne correspond aux critères.", "Recherche",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la recherche." + Environment.NewLine + ex.Message,
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnActualiser_Click(object sender, EventArgs e)
        {
            ChargerListeGlobale();
        }
    }
}
