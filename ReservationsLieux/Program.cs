using System;
using System.Windows.Forms;
using ReservationsLieux.UI;

namespace ReservationsLieux
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                DatabaseInitializer.Initialiser();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Impossible d'initialiser la base de données TP1_LSDA." +
                    Environment.NewLine + Environment.NewLine +
                    "Vérifiez que SQL Server LocalDB est installé, puis relancez l'application." +
                    Environment.NewLine + Environment.NewLine +
                    ex.Message,
                    "Erreur de démarrage",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            Application.Run(new FrmMain());
        }
    }
}
