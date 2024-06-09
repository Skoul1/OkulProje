using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OkulProjesi.Models;

namespace OkulProjesi.Connect
{
    public class OkulDbContext : DbContext
    {
        public DbSet<Ogrenci> Ogrenciler { get; set; }
        public DbSet<Ogretmen> Ogretmenler { get; set; }
        public DbSet<Ders> Dersler { get; set; }
        public DbSet<OgrenciDers> OgrenciDersler { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Microsoft SQL Server veritabanı bağlantı dizesi kullanıldı
            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=OkulDb; User Id=sa; Password=abc123; TrustServerCertificate=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ogrenci>(entity =>
            {
                entity.ToTable("tblOgrenciler");
                entity.Property(o => o.OgrenciAd).HasMaxLength(50).IsRequired();
                entity.Property(o => o.OgrenciSoyAd).HasMaxLength(50).IsRequired();
                entity.Property(o => o.OgrenciNumarasi).HasMaxLength(50).IsRequired();
            });

            modelBuilder.Entity<Ders>(entity =>
            {
                entity.ToTable("tblDersler");
                entity.Property(d => d.DersAdi).HasMaxLength(100).IsRequired();
                entity.Property(d => d.DersKodu).HasMaxLength(20).IsRequired();
                // SQL Server için uygun tip belirlemesi
                entity.Property(d => d.DersKredi).HasColumnType("smallint");
            });

            modelBuilder.Entity<Ogretmen>(entity =>
            {
                entity.ToTable("tblOgretmenler");
                entity.Property(o => o.OgretmenAdı).HasMaxLength(50).IsRequired();
                entity.Property(o => o.OgretmenSoyAdı).HasMaxLength(50).IsRequired();
                entity.Property(o => o.OgretmenBolum).HasMaxLength(100);
            });

            modelBuilder.Entity<OgrenciDers>(entity =>
            {
                entity.HasKey(od => new { od.OgrenciId, od.DersId });
                entity.HasOne(od => od.Ogrenci).WithMany(o => o.OgrenciDersler).HasForeignKey(od => od.OgrenciId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(od => od.Ders).WithMany(d => d.OgrenciDersler).HasForeignKey(od => od.DersId).OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
