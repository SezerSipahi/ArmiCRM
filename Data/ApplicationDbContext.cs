using Microsoft.EntityFrameworkCore;
using deneme.Models;

namespace deneme.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; } = null!;
        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<ErpBilgileri> ErpBilgileri { get; set; }
        public DbSet<ErpServerIpAdresi> ErpServerIpAdresleri { get; set; }
        public DbSet<OzelYazilimBilgileri> OzelYazilimBilgileri { get; set; }
        public DbSet<AgDepolamaBilgileri> AgDepolamaBilgileri { get; set; }
        public DbSet<GuvenlikDuvariBilgileri> GuvenlikDuvariBilgileri { get; set; }
        public DbSet<AcikPort> AcikPortlar { get; set; }
        public DbSet<ModemBilgileri> ModemBilgileri { get; set; }
        public DbSet<ModemIpAdresi> ModemIpAdresleri { get; set; }
        public DbSet<ModemAcikPort> ModemAcikPortlar { get; set; }
        public DbSet<ModemKullaniciErisimBilgileri> ModemKullaniciErisimBilgileri { get; set; }
        public DbSet<WebmailBilgileri> WebmailBilgileri { get; set; }
        public DbSet<OzelParametre> OzelParametreler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User tablosu için konfigürasyon
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // Müşteri ile ilişkiler
            modelBuilder.Entity<Musteri>()
                .HasMany(m => m.ErpBilgileri)
                .WithOne(e => e.Musteri)
                .HasForeignKey(e => e.MusteriId);

            modelBuilder.Entity<Musteri>()
                .HasMany(m => m.OzelYazilimBilgileri)
                .WithOne(o => o.Musteri)
                .HasForeignKey(o => o.MusteriId);

            modelBuilder.Entity<Musteri>()
                .HasOne(m => m.AgDepolamaBilgileri)
                .WithOne(a => a.Musteri)
                .HasForeignKey<AgDepolamaBilgileri>(a => a.MusteriId);

            modelBuilder.Entity<Musteri>()
                .HasOne(m => m.GuvenlikDuvariBilgileri)
                .WithOne(g => g.Musteri)
                .HasForeignKey<GuvenlikDuvariBilgileri>(g => g.MusteriId);

            // GuvenlikDuvariBilgileri ile AcikPort ilişkisi
            modelBuilder.Entity<GuvenlikDuvariBilgileri>()
                .HasMany(g => g.AcikPortlar)
                .WithOne(a => a.GuvenlikDuvariBilgileri)
                .HasForeignKey(a => a.GuvenlikDuvariBilgileriId);

            modelBuilder.Entity<Musteri>()
                .HasOne(m => m.ModemBilgileri)
                .WithOne(m => m.Musteri)
                .HasForeignKey<ModemBilgileri>(m => m.MusteriId);

            // ModemBilgileri ile ilişkiler
            modelBuilder.Entity<ModemBilgileri>()
                .HasMany(m => m.IpAdresleri)
                .WithOne(i => i.ModemBilgileri)
                .HasForeignKey(i => i.ModemBilgileriId);

            modelBuilder.Entity<ModemBilgileri>()
                .HasMany(m => m.AcikPortlar)
                .WithOne(a => a.ModemBilgileri)
                .HasForeignKey(a => a.ModemBilgileriId);

            modelBuilder.Entity<ModemBilgileri>()
                .HasMany(m => m.KullaniciErisimBilgileri)
                .WithOne(k => k.ModemBilgileri)
                .HasForeignKey(k => k.ModemBilgileriId);

            modelBuilder.Entity<Musteri>()
                .HasOne(m => m.WebmailBilgileri)
                .WithOne(w => w.Musteri)
                .HasForeignKey<WebmailBilgileri>(w => w.MusteriId);

            modelBuilder.Entity<Musteri>()
                .HasMany(m => m.OzelParametreler)
                .WithOne(o => o.Musteri)
                .HasForeignKey(o => o.MusteriId);

            // TCKN veya VergiNo zorunluluğu için check constraint (PostgreSQL syntax)
            modelBuilder.Entity<Musteri>()
                .ToTable(t => t.HasCheckConstraint("CK_Musteri_TCKN_VergiNo", 
                    "\"TCKN\" IS NOT NULL OR \"VergiNo\" IS NOT NULL"));
        }
    }
} 