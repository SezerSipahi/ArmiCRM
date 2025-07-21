using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace deneme.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostgreSQLMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Musteriler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirmaUnvani = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    TCKN = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: true),
                    VergiNo = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Telefon = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Adres = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Ad = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Soyad = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Notlar = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    OlusturmaTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SonGuncellemeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AktifMi = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musteriler", x => x.Id);
                    table.CheckConstraint("CK_Musteri_TCKN_VergiNo", "\"TCKN\" IS NOT NULL OR \"VergiNo\" IS NOT NULL");
                });

            migrationBuilder.CreateTable(
                name: "PasswordResetTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Token = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsUsed = table.Column<bool>(type: "boolean", nullable: false),
                    UsedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordResetTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgDepolamaBilgileri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RaidTipi = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DiskBoyutu = table.Column<int>(type: "integer", nullable: true),
                    DiskSayisi = table.Column<int>(type: "integer", nullable: true),
                    YedeklemeDosyaYolu = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    YedeklemeDurumu = table.Column<int>(type: "integer", nullable: true),
                    SonGuncellemeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    OlusturmaTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MusteriId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgDepolamaBilgileri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgDepolamaBilgileri_Musteriler_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteriler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ErpBilgileri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ErpUrunAdi = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    SurumBilgisi = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    KullaniciSayisi = table.Column<int>(type: "integer", nullable: true),
                    KullanimSekli = table.Column<int>(type: "integer", nullable: true),
                    LisansTuru = table.Column<int>(type: "integer", nullable: true),
                    KurulumTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LemTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    YedeklemeDurumu = table.Column<int>(type: "integer", nullable: true),
                    Notlar = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    SonGuncellemeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    OlusturmaTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MusteriId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErpBilgileri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ErpBilgileri_Musteriler_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteriler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GuvenlikDuvariBilgileri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GuvenlikDuvariVarMi = table.Column<bool>(type: "boolean", nullable: false),
                    Model = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    GuncellemeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LemTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    YedeklemeDosyaYolu = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    YedeklemeDurumu = table.Column<int>(type: "integer", nullable: false),
                    OlusturmaTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SonGuncellemeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    MusteriId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuvenlikDuvariBilgileri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuvenlikDuvariBilgileri_Musteriler_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteriler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModemBilgileri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Marka = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Model = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    YedeklemeDosyaYolu = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    YedeklemeDurumu = table.Column<int>(type: "integer", nullable: false),
                    SonGuncellemeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    OlusturmaTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MusteriId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModemBilgileri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModemBilgileri_Musteriler_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteriler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OzelParametreler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParametreTuru = table.Column<int>(type: "integer", nullable: false),
                    ParametreAdi = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Aciklama = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Icerik = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    DosyaYolu = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    LogoResim = table.Column<byte[]>(type: "bytea", nullable: true),
                    AktifMi = table.Column<bool>(type: "boolean", nullable: false),
                    Versiyon = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    OlusturmaTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MusteriId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OzelParametreler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OzelParametreler_Musteriler_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteriler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OzelYazilimBilgileri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    YazilimIsmi = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    KullaniciSayisi = table.Column<int>(type: "integer", nullable: true),
                    KiralamaModeli = table.Column<int>(type: "integer", nullable: true),
                    EntegrasyonDurumu = table.Column<bool>(type: "boolean", nullable: false),
                    EntegrasyonAciklama = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    DestekVerenFirma = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    KurulumTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SurumBilgisi = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    LemTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    GuncellemeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LisansTuru = table.Column<int>(type: "integer", nullable: true),
                    Notlar = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    OlusturmaTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SonGuncellemeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    MusteriId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OzelYazilimBilgileri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OzelYazilimBilgileri_Musteriler_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteriler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebmailBilgileri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MailHizmetiAlinanFirma = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    GuncellemeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    MailKullaniciSayisi = table.Column<int>(type: "integer", nullable: true),
                    AktifWebSitesiVarMi = table.Column<bool>(type: "boolean", nullable: false),
                    SiteOmegaYazilimTarafindanMiYapildi = table.Column<bool>(type: "boolean", nullable: false),
                    DomainHizmetiSaglayicisi = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    HostingHizmetiSaglayicisi = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    PortalKullaniciAdi = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PortalSifre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    OlusturmaTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MusteriId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebmailBilgileri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebmailBilgileri_Musteriler_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteriler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ErpServerIpAdresleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IpAdresi = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Aciklama = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ErpBilgileriId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErpServerIpAdresleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ErpServerIpAdresleri_ErpBilgileri_ErpBilgileriId",
                        column: x => x.ErpBilgileriId,
                        principalTable: "ErpBilgileri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AcikPortlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PortNumarasi = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Protokol = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Aciklama = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    GuvenlikDuvariBilgileriId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcikPortlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcikPortlar_GuvenlikDuvariBilgileri_GuvenlikDuvariBilgileri~",
                        column: x => x.GuvenlikDuvariBilgileriId,
                        principalTable: "GuvenlikDuvariBilgileri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModemAcikPortlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PortNumarasi = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Protokol = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Aciklama = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ModemBilgileriId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModemAcikPortlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModemAcikPortlar_ModemBilgileri_ModemBilgileriId",
                        column: x => x.ModemBilgileriId,
                        principalTable: "ModemBilgileri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModemIpAdresleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IpAdresi = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    Tip = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Aciklama = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ModemBilgileriId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModemIpAdresleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModemIpAdresleri_ModemBilgileri_ModemBilgileriId",
                        column: x => x.ModemBilgileriId,
                        principalTable: "ModemBilgileri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModemKullaniciErisimBilgileri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KullaniciAdi = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Sifre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Aciklama = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ModemBilgileriId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModemKullaniciErisimBilgileri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModemKullaniciErisimBilgileri_ModemBilgileri_ModemBilgileri~",
                        column: x => x.ModemBilgileriId,
                        principalTable: "ModemBilgileri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcikPortlar_GuvenlikDuvariBilgileriId",
                table: "AcikPortlar",
                column: "GuvenlikDuvariBilgileriId");

            migrationBuilder.CreateIndex(
                name: "IX_AgDepolamaBilgileri_MusteriId",
                table: "AgDepolamaBilgileri",
                column: "MusteriId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ErpBilgileri_MusteriId",
                table: "ErpBilgileri",
                column: "MusteriId");

            migrationBuilder.CreateIndex(
                name: "IX_ErpServerIpAdresleri_ErpBilgileriId",
                table: "ErpServerIpAdresleri",
                column: "ErpBilgileriId");

            migrationBuilder.CreateIndex(
                name: "IX_GuvenlikDuvariBilgileri_MusteriId",
                table: "GuvenlikDuvariBilgileri",
                column: "MusteriId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModemAcikPortlar_ModemBilgileriId",
                table: "ModemAcikPortlar",
                column: "ModemBilgileriId");

            migrationBuilder.CreateIndex(
                name: "IX_ModemBilgileri_MusteriId",
                table: "ModemBilgileri",
                column: "MusteriId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModemIpAdresleri_ModemBilgileriId",
                table: "ModemIpAdresleri",
                column: "ModemBilgileriId");

            migrationBuilder.CreateIndex(
                name: "IX_ModemKullaniciErisimBilgileri_ModemBilgileriId",
                table: "ModemKullaniciErisimBilgileri",
                column: "ModemBilgileriId");

            migrationBuilder.CreateIndex(
                name: "IX_OzelParametreler_MusteriId",
                table: "OzelParametreler",
                column: "MusteriId");

            migrationBuilder.CreateIndex(
                name: "IX_OzelYazilimBilgileri_MusteriId",
                table: "OzelYazilimBilgileri",
                column: "MusteriId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WebmailBilgileri_MusteriId",
                table: "WebmailBilgileri",
                column: "MusteriId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcikPortlar");

            migrationBuilder.DropTable(
                name: "AgDepolamaBilgileri");

            migrationBuilder.DropTable(
                name: "ErpServerIpAdresleri");

            migrationBuilder.DropTable(
                name: "ModemAcikPortlar");

            migrationBuilder.DropTable(
                name: "ModemIpAdresleri");

            migrationBuilder.DropTable(
                name: "ModemKullaniciErisimBilgileri");

            migrationBuilder.DropTable(
                name: "OzelParametreler");

            migrationBuilder.DropTable(
                name: "OzelYazilimBilgileri");

            migrationBuilder.DropTable(
                name: "PasswordResetTokens");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WebmailBilgileri");

            migrationBuilder.DropTable(
                name: "GuvenlikDuvariBilgileri");

            migrationBuilder.DropTable(
                name: "ErpBilgileri");

            migrationBuilder.DropTable(
                name: "ModemBilgileri");

            migrationBuilder.DropTable(
                name: "Musteriler");
        }
    }
}
