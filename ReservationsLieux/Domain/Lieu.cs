namespace ReservationsLieux.Domain
{
    public class Lieu
    {
        public int IdLieu { get; set; }
        public string NomLieu { get; set; }
        public string Royaume { get; set; }
        public int Dangerosite { get; set; }

        public override string ToString()
        {
            return NomLieu;
        }
    }
}
