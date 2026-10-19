using System;
using System.Drawing;
using System.Windows.Forms;

namespace ReservationsLieux.UI
{
    public class FrmMain : Form
    {
        private TabControl tabPrincipal;
        private TabPage tabLieux;
        private TabPage tabPersonnages;
        private TabPage tabMissions;
        private FrmLieux frmLieux;
        private FrmPersonnages frmPersonnages;
        private FrmMissions frmMissions;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            tabPrincipal = new TabControl();
            tabLieux = new TabPage();
            tabPersonnages = new TabPage();
            tabMissions = new TabPage();
            frmLieux = new FrmLieux();
            frmPersonnages = new FrmPersonnages();
            frmMissions = new FrmMissions();

            SuspendLayout();

            Text = "Haut Conseil — Réservations de lieux (Terre du Milieu)";
            Name = "FrmMain";
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new Size(1100, 720);
            Size = new Size(1200, 780);
            Font = new Font("Segoe UI", 9.75F);

            tabPrincipal.Name = "tabPrincipal";
            tabPrincipal.Dock = DockStyle.Fill;
            tabPrincipal.SelectedIndexChanged += TabPrincipal_SelectedIndexChanged;

            tabLieux.Text = "Lieux";
            tabLieux.Padding = new Padding(4);
            tabLieux.Controls.Add(frmLieux);

            tabPersonnages.Text = "Personnages";
            tabPersonnages.Padding = new Padding(4);
            tabPersonnages.Controls.Add(frmPersonnages);

            tabMissions.Text = "Missions";
            tabMissions.Padding = new Padding(4);
            tabMissions.Controls.Add(frmMissions);

            tabPrincipal.TabPages.Add(tabLieux);
            tabPrincipal.TabPages.Add(tabPersonnages);
            tabPrincipal.TabPages.Add(tabMissions);

            Controls.Add(tabPrincipal);
            ResumeLayout(false);
        }

        private void TabPrincipal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabPrincipal.SelectedTab == tabLieux)
            {
                frmLieux.ChargerDonnees();
            }
            else if (tabPrincipal.SelectedTab == tabPersonnages)
            {
                frmPersonnages.ChargerDonnees();
            }
            else if (tabPrincipal.SelectedTab == tabMissions)
            {
                frmMissions.Rafraichir();
            }
        }
    }
}
