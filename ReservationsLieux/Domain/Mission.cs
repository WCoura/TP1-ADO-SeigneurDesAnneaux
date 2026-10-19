using System;

namespace ReservationsLieux.Domain
{
    public class Mission
    {
        public int IdMission { get; set; }
        public int IdLieu { get; set; }
        public int IdPersonnage { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }

        public string NomLieu { get; set; }
        public string NomPersonnage { get; set; }
        public string Royaume { get; set; }
        public string Race { get; set; }
    }
}
