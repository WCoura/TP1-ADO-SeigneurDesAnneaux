using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using ReservationsLieux.Repositories;

namespace ReservationsLieux.UI
{
    public class FrmPersonnages : UserControl
    {
        private readonly PersonnageRepository _repository = new PersonnageRepository();
        private DataTable _table;

        private DataGridView dgvPersonnages;
        private Button btnAjouter;
        private Button btnSauvegarder;
        private Button btnSupprimer;
        private Label lblTitre;
        private Label lblAide;

        public FrmPersonnages()
        {
            InitializeComponent();
            ChargerDonnees();
        }

        private void InitializeComponent()
        {
            dgvPersonnages = new DataGridView();
            btnAjouter = new Button();
            btnSauvegarder = new Button();
            btnSupprimer = new Button();
            lblTitre = new Label();
            lblAide = new Label();
            Panel pnlHaut = new Panel();
            Panel pnlBoutons = new Panel();

            SuspendLayout();

            lblTitre.Dock = DockStyle.Top;
            lblTitre.Height = 36;
            lblTitre.Text = "Personnages";
            lblTitre.Font = new Font("Segoe UI Semibold", 14F);
            lblTitre.TextAlign = ContentAlignment.MiddleLeft;
            lblTitre.Padding = new Padding(8, 0, 0, 0);

            lblAide.Dock = DockStyle.Top;
            lblAide.Height = 28;
            lblAide.Text = "Modifiez les cellules, puis cliquez sur Sauvegarder. Nom, race et faction sont obligatoires.";
            lblAide.ForeColor = Color.DimGray;
            lblAide.Padding = new Padding(8, 0, 0, 0);

            btnAjouter.Text = "Ajouter";
            btnAjouter.Width = 120;
            btnAjouter.Height = 36;
            btnAjouter.Location = new Point(8, 8);
            btnAjouter.Click += BtnAjouter_Click;

            btnSauvegarder.Text = "Sauvegarder";
            btnSauvegarder.Width = 120;
            btnSauvegarder.Height = 36;
            btnSauvegarder.Location = new Point(136, 8);
            btnSauvegarder.Click += BtnSauvegarder_Click;

            btnSupprimer.Text = "Supprimer";
            btnSupprimer.Width = 120;
            btnSupprimer.Height = 36;
            btnSupprimer.Location = new Point(264, 8);
            btnSupprimer.Click += BtnSupprimer_Click;

            pnlBoutons.Dock = DockStyle.Top;
            pnlBoutons.Height = 52;
            pnlBoutons.Controls.Add(btnAjouter);
            pnlBoutons.Controls.Add(btnSauvegarder);
            pnlBoutons.Controls.Add(btnSupprimer);

            pnlHaut.Dock = DockStyle.Top;
            pnlHaut.Height = 116;
            pnlHaut.Controls.Add(pnlBoutons);
            pnlHaut.Controls.Add(lblAide);
            pnlHaut.Controls.Add(lblTitre);

            dgvPersonnages.Dock = DockStyle.Fill;
            dgvPersonnages.AllowUserToAddRows = false;
            dgvPersonnages.AllowUserToDeleteRows = false;
            dgvPersonnages.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPersonnages.MultiSelect = false;
            dgvPersonnages.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPersonnages.RowHeadersVisible = false;
            dgvPersonnages.BackgroundColor = Color.White;
            dgvPersonnages.Name = "dgvPersonnages";

            Controls.Add(dgvPersonnages);
            Controls.Add(pnlHaut);
            Dock = DockStyle.Fill;
            Padding = new Padding(8);
            ResumeLayout(false);
        }

        public void ChargerDonnees()
        {
            try
            {
                _table = _repository.ObtenirTous();
                PreparerColonneIdentite(_table, "IdPersonnage");
                dgvPersonnages.DataSource = _table;
                ConfigurerColonnes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible de charger les personnages." + Environment.NewLine + ex.Message,
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
            if (dgvPersonnages.Columns["IdPersonnage"] != null)
            {
                dgvPersonnages.Columns["IdPersonnage"].HeaderText = "No";
                dgvPersonnages.Columns["IdPersonnage"].ReadOnly = true;
                dgvPersonnages.Columns["IdPersonnage"].FillWeight = 15;
            }
            if (dgvPersonnages.Columns["Nom"] != null)
            {
                dgvPersonnages.Columns["Nom"].HeaderText = "Nom";
                dgvPersonnages.Columns["Nom"].FillWeight = 35;
            }
            if (dgvPersonnages.Columns["Race"] != null)
            {
                dgvPersonnages.Columns["Race"].HeaderText = "Race";
                dgvPersonnages.Columns["Race"].FillWeight = 25;
            }
            if (dgvPersonnages.Columns["Faction"] != null)
            {
                dgvPersonnages.Columns["Faction"].HeaderText = "Faction";
                dgvPersonnages.Columns["Faction"].FillWeight = 25;
            }
        }

        private void BtnAjouter_Click(object sender, EventArgs e)
        {
            if (_table == null)
            {
                return;
            }

            DataRow ligne = _table.NewRow();
            ligne["Nom"] = "Nouveau personnage";
            ligne["Race"] = "À préciser";
            ligne["Faction"] = "À préciser";
            _table.Rows.Add(ligne);

            int index = dgvPersonnages.Rows.Count - 1;
            if (index >= 0)
            {
                dgvPersonnages.ClearSelection();
                dgvPersonnages.Rows[index].Selected = true;
                dgvPersonnages.CurrentCell = dgvPersonnages.Rows[index].Cells["Nom"];
                dgvPersonnages.BeginEdit(true);
            }
        }

        private void BtnSupprimer_Click(object sender, EventArgs e)
        {
            if (dgvPersonnages.CurrentRow == null)
            {
                MessageBox.Show("Sélectionnez un personnage à supprimer.", "Suppression",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataRowView vue = dgvPersonnages.CurrentRow.DataBoundItem as DataRowView;
            if (vue == null)
            {
                return;
            }

            DialogResult confirmation = MessageBox.Show(
                "Supprimer le personnage sélectionné ? L'opération sera enregistrée au moment de Sauvegarder.",
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmation == DialogResult.Yes)
            {
                vue.Row.Delete();
            }
        }

        private void BtnSauvegarder_Click(object sender, EventArgs e)
        {
            dgvPersonnages.EndEdit();
            BindingContext[_table].EndCurrentEdit();

            if (!ValiderLignes())
            {
                return;
            }

            try
            {
                _repository.Sauvegarder(_table);
                ChargerDonnees();
                MessageBox.Show("Les personnages ont été enregistrés.", "Sauvegarde",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    MessageBox.Show(
                        "Impossible de supprimer ce personnage : une ou plusieurs missions y sont encore rattachées.",
                        "Contrainte d'intégrité", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (ex.Number == 2627 || ex.Number == 2601)
                {
                    MessageBox.Show("Ce nom de personnage existe déjà. Choisissez un nom unique.",
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

                string nom = Convert.ToString(ligne["Nom"]);
                string race = Convert.ToString(ligne["Race"]);
                string faction = Convert.ToString(ligne["Faction"]);

                if (string.IsNullOrWhiteSpace(nom) || nom.Trim() == "Nouveau personnage")
                {
                    MessageBox.Show("Chaque personnage doit avoir un nom réel.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(race) || race.Trim() == "À préciser")
                {
                    MessageBox.Show("Chaque personnage doit indiquer une race.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(faction) || faction.Trim() == "À préciser")
                {
                    MessageBox.Show("Chaque personnage doit indiquer une faction.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }
    }
}
