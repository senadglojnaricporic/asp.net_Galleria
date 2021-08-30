using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace Galleria.Data
{
    public partial class GalleriaContext : DbContext
    {
        public IConfiguration Configuration { get; }
        public GalleriaContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public GalleriaContext(DbContextOptions<GalleriaContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(Configuration["Galleria:PostgresConnectionString"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "English_United States.1252");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId)
                    .HasColumnName("category_id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, null, 10L, null, null);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.Property(e => e.ColorId)
                    .HasColumnName("color_id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, null, 10L, null, null);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.Property(e => e.GradeId)
                    .HasColumnName("grade_id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, null, 10L, null, null);

                entity.Property(e => e.GradeNum).HasColumnName("grade_num");
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.Property(e => e.PhotoId)
                    .HasColumnName("photo_id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, null, 10000L, null, 10L);

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.ColorId).HasColumnName("color_id");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("display_name");

                entity.Property(e => e.FileUrl)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("file_url");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("timestamp");

                entity.Property(e => e.UserId)
                    .HasMaxLength(45)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Photos)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("Photos_category_id_fkey");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.Photos)
                    .HasForeignKey(d => d.ColorId)
                    .HasConstraintName("Photos_color_id_fkey");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.PhotoId })
                    .HasName("Reviews_pkey");

                entity.Property(e => e.UserId)
                    .HasMaxLength(45)
                    .HasColumnName("user_id");

                entity.Property(e => e.PhotoId).HasColumnName("photo_id");

                entity.Property(e => e.Comment)
                    .HasMaxLength(100)
                    .HasColumnName("comment");

                entity.Property(e => e.GradeId).HasColumnName("grade_id");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("timestamp");

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.GradeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Reviews_grade_id_fkey");

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.PhotoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Reviews_photo_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
