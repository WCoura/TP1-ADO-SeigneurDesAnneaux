namespace ReservationsLieux.Domain
{
    public class Personnage
    {
        public int IdPersonnage { get; set; }
        public string Nom { get; set; }
        public string Race { get; set; }
        public string Faction { get; set; }

        public override string ToString()
        {
            return Nom;
        }
    }
}
