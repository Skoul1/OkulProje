namespace OkulProjesi.Models
{
    public class Ogrenci
    {
        public int OgrenciId { get; set; }
        public string OgrenciAd { get; set; }
        public string OgrenciSoyAd { get; set; }
        public string OgrenciNumarasi { get; set; } 
        public List<OgrenciDers> OgrenciDersler { get; set; } 
    }
}
    