namespace OkulProjesi.Models
{
    public class Ders
    {
        public int DersId { get; set; }
        public string DersKodu { get; set; }
        public string DersAdi { get; set; } 
        public byte DersKredi { get; set; }
        public List<OgrenciDers> OgrenciDersler { get; set; } 
    }
}
