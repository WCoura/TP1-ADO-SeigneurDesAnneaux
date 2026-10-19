using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using ReservationsLieux.Repositories;

namespace ReservationsLieux.UI
{
    public class FrmLieux : UserControl
    {
        private readonly LieuRepository _repository = new LieuRepository();
        private DataTable _table;

        private DataGridView dgvLieux;
        private Button btnAjouter;
        private Button btnSauvegarder;
        private Button btnSupprimer;
        private Label lblTitre;
        private Panel pnlHaut;
        private Panel pnlBoutons;
        private Label lblAide;

        public FrmLieux()
        {
            InitializeComponent();
            ChargerDonnees();
        }

        private void InitializeComponent()
        {
            dgvLieux = new DataGridView();
            btnAjouter = new Button();
            btnSauvegarder = new Button();
            btnSupprimer = new Button();
            lblTitre = new Label();
            lblAide = new Label();
            pnlHaut = new Panel();
            pnlBoutons = new Panel();
            ((System.ComponentModel.ISupportInitialize)dgvLieux).BeginInit();
            pnlHaut.SuspendLayout();
            pnlBoutons.SuspendLayout();
            SuspendLayout();
            // 
            // dgvLieux
            // 
            dgvLieux.AllowUserToAddRows = false;
            dgvLieux.AllowUserToDeleteRows = false;
            dgvLieux.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLieux.BackgroundColor = Color.White;
            dgvLieux.Dock = DockStyle.Fill;
            dgvLieux.Location = new Point(8, 124);
            dgvLieux.MultiSelect = false;
            dgvLieux.Name = "dgvLieux";
            dgvLieux.RowHeadersVisible = false;
            dgvLieux.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLieux.Size = new Size(871, 406);
            dgvLieux.TabIndex = 0;
            // 
            // btnAjouter
            // 
            btnAjouter.Location = new Point(8, 8);
            btnAjouter.Name = "btnAjouter";
            btnAjouter.Size = new Size(120, 36);
            btnAjouter.TabIndex = 0;
            btnAjouter.Text = "Ajouter";
            btnAjouter.Click += BtnAjouter_Click;
            // 
            // btnSauvegarder
            // 
            btnSauvegarder.Location = new Point(136, 8);
            btnSauvegarder.Name = "btnSauvegarder";
            btnSauvegarder.Size = new Size(120, 36);
            btnSauvegarder.TabIndex = 1;
            btnSauvegarder.Text = "Sauvegarder";
            btnSauvegarder.Click += BtnSauvegarder_Click;
            // 
            // btnSupprimer
            // 
            btnSupprimer.Location = new Point(264, 8);
            btnSupprimer.Name = "btnSupprimer";
            btnSupprimer.Size = new Size(120, 36);
            btnSupprimer.TabIndex = 2;
            btnSupprimer.Text = "Supprimer";
            btnSupprimer.Click += BtnSupprimer_Click;
            // 
            // lblTitre
            // 
            lblTitre.Dock = DockStyle.Top;
            lblTitre.Font = new Font("Segoe UI Semibold", 14F);
            lblTitre.Location = new Point(0, 0);
            lblTitre.Name = "lblTitre";
            lblTitre.Padding = new Padding(8, 0, 0, 0);
            lblTitre.Size = new Size(871, 36);
            lblTitre.TabIndex = 2;
            lblTitre.Text = "Lieux de la Terre du Milieu";
            lblTitre.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblAide
            // 
            lblAide.Dock = DockStyle.Top;
            lblAide.ForeColor = Color.DimGray;
            lblAide.Location = new Point(0, 36);
            lblAide.Name = "lblAide";
            lblAide.Padding = new Padding(8, 0, 0, 0);
            lblAide.Size = new Size(871, 28);
            lblAide.TabIndex = 1;
            lblAide.Text = "Modifiez les cellules, puis cliquez sur Sauvegarder. Dangerosité : entier de 1 à 10.";
            // 
            // pnlHaut
            // 
            pnlHaut.Controls.Add(pnlBoutons);
            pnlHaut.Controls.Add(lblAide);
            pnlHaut.Controls.Add(lblTitre);
            pnlHaut.Dock = DockStyle.Top;
            pnlHaut.Location = new Point(8, 8);
            pnlHaut.Name = "pnlHaut";
            pnlHaut.Size = new Size(871, 116);
            pnlHaut.TabIndex = 1;
            // 
            // pnlBoutons
            // 
            pnlBoutons.Controls.Add(btnAjouter);
            pnlBoutons.Controls.Add(btnSauvegarder);
            pnlBoutons.Controls.Add(btnSupprimer);
            pnlBoutons.Dock = DockStyle.Top;
            pnlBoutons.Location = new Point(0, 64);
            pnlBoutons.Name = "pnlBoutons";
            pnlBoutons.Size = new Size(871, 52);
            pnlBoutons.TabIndex = 0;
            // 
            // FrmLieux
            // 
            Controls.Add(dgvLieux);
            Controls.Add(pnlHaut);
            Name = "FrmLieux";
            Padding = new Padding(8);
            Size = new Size(887, 538);
            ((System.ComponentModel.ISupportInitialize)dgvLieux).EndInit();
            pnlHaut.ResumeLayout(false);
            pnlBoutons.ResumeLayout(false);
            ResumeLayout(false);
        }

        public void ChargerDonnees()
        {
            try
            {
                _table = _repository.ObtenirTous();
                PreparerColonneIdentite(_table, "IdLieu");
                dgvLieux.DataSource = _table;
                ConfigurerColonnes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible de charger les lieux." + Environment.NewLine + ex.Message,
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void PreparerColonneIdentite(DataTable table, string nomColonne)
        {
            if (table == null || !table.Columns.Contains(nomColonne))
            {
                return;
            }

            DataColumn colonne = table.Columns[nomColonne];
            colonne.ReadOnly = true;
            if (!colonne.AutoIncrement)
            {
                colonne.AutoIncrement = true;
                colonne.AutoIncrementSeed = -1;
                colonne.AutoIncrementStep = -1;
            }
        }

        private void ConfigurerColonnes()
        {
            if (dgvLieux.Columns["IdLieu"] != null)
            {
                dgvLieux.Columns["IdLieu"].HeaderText = "No";
                dgvLieux.Columns["IdLieu"].ReadOnly = true;
                dgvLieux.Columns["IdLieu"].FillWeight = 15;
            }
            if (dgvLieux.Columns["NomLieu"] != null)
            {
                dgvLieux.Columns["NomLieu"].HeaderText = "Nom du lieu";
                dgvLieux.Columns["NomLieu"].FillWeight = 40;
            }
            if (dgvLieux.Columns["Royaume"] != null)
            {
                dgvLieux.Columns["Royaume"].HeaderText = "Royaume";
                dgvLieux.Columns["Royaume"].FillWeight = 30;
            }
            if (dgvLieux.Columns["Dangerosite"] != null)
            {
                dgvLieux.Columns["Dangerosite"].HeaderText = "Dangerosité";
                dgvLieux.Columns["Dangerosite"].FillWeight = 20;
            }
        }

        private void BtnAjouter_Click(object sender, EventArgs e)
        {
            if (_table == null)
            {
                return;
            }

            DataRow ligne = _table.NewRow();
            ligne["NomLieu"] = "Nouveau lieu";
            ligne["Royaume"] = "À préciser";
            ligne["Dangerosite"] = 1;
            _table.Rows.Add(ligne);

            int index = dgvLieux.Rows.Count - 1;
            if (index >= 0)
            {
                dgvLieux.ClearSelection();
                dgvLieux.Rows[index].Selected = true;
                dgvLieux.CurrentCell = dgvLieux.Rows[index].Cells["NomLieu"];
                dgvLieux.BeginEdit(true);
            }
        }

        private void BtnSupprimer_Click(object sender, EventArgs e)
        {
            if (dgvLieux.CurrentRow == null)
            {
                MessageBox.Show("Sélectionnez un lieu à supprimer.", "Suppression",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataRowView vue = dgvLieux.CurrentRow.DataBoundItem as DataRowView;
            if (vue == null)
            {
                return;
            }

            DialogResult confirmation = MessageBox.Show(
                "Supprimer le lieu sélectionné ? L'opération sera enregistrée au moment de Sauvegarder.",
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmation == DialogResult.Yes)
            {
                vue.Row.Delete();
            }
        }

        private void BtnSauvegarder_Click(object sender, EventArgs e)
        {
            dgvLieux.EndEdit();
            BindingContext[_table].EndCurrentEdit();

            if (!ValiderLignes())
            {
                return;
            }

            try
            {
                _repository.Sauvegarder(_table);
                ChargerDonnees();
                MessageBox.Show("Les lieux ont été enregistrés.", "Sauvegarde",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    MessageBox.Show(
                        "Impossible de supprimer ce lieu : une ou plusieurs missions y sont encore rattachées.",
                        "Contrainte d'intégrité", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (ex.Number == 2627 || ex.Number == 2601)
                {
                    MessageBox.Show("Ce nom de lieu existe déjà. Choisissez un nom unique.",
                        "Doublon", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Erreur SQL lors de la sauvegarde." + Environment.NewLine + ex.Message,
                        "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                ChargerDonnees();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la sauvegarde." + Environment.NewLine + ex.Message,
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ChargerDonnees();
            }
        }

        private bool ValiderLignes()
        {
            foreach (DataRow ligne in _table.Rows)
            {
                if (ligne.RowState == DataRowState.Deleted)
                {
                    continue;
                }

                string nom = Convert.ToString(ligne["NomLieu"]);
                string royaume = Convert.ToString(ligne["Royaume"]);
                if (string.IsNullOrWhiteSpace(nom) || nom.Trim() == "Nouveau lieu")
                {
                    MessageBox.Show("Chaque lieu doit avoir un nom réel.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(royaume) || royaume.Trim() == "À préciser")
                {
                    MessageBox.Show("Chaque lieu doit indiquer un royaume.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                int dangerosite;
                if (!int.TryParse(Convert.ToString(ligne["Dangerosite"]), out dangerosite)
                    || dangerosite < 1 || dangerosite > 10)
                {
                    MessageBox.Show("La dangerosité doit être un entier entre 1 et 10.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }
    }
}
