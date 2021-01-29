using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ChannelBot.DAL.Models
{
    public partial class postgresContext : DbContext
    {
        public postgresContext()
        {
        }

        public postgresContext(DbContextOptions<postgresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Channel> Channel { get; set; }
        public virtual DbSet<ChannelGroup> ChannelGroup { get; set; }
        public virtual DbSet<Content> Content { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<GroupSource> GroupSource { get; set; }
        public virtual DbSet<JwtOption> JwtOption { get; set; }
        public virtual DbSet<Platform> Platform { get; set; }
        public virtual DbSet<Source> Source { get; set; }
        public virtual DbSet<UserCredential> UserCredential { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=185.87.48.116;Database=postgres;Username=postgres;Password=123123AAA");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e => e.Login).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Password).IsRequired();
            });

            modelBuilder.Entity<Channel>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<ChannelGroup>(entity =>
            {
                entity.HasKey(e => e.ChanneId)
                    .HasName("ChannelGroup_pkey");

                entity.Property(e => e.ChanneId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Content>(entity =>
            {
                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.MediaUrl)
                    .IsRequired()
                    .HasColumnName("MediaURL");

                entity.HasOne(d => d.Source)
                    .WithMany(p => p.Content)
                    .HasForeignKey(d => d.SourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("c_csi_fk");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Group)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("g_cg_fk");
            });

            modelBuilder.Entity<GroupSource>(entity =>
            {
                entity.HasKey(e => new { e.GroupId, e.SourceId })
                    .HasName("GroupSources_pkey");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupSource)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("gs_gi_fk");

                entity.HasOne(d => d.Source)
                    .WithMany(p => p.GroupSource)
                    .HasForeignKey(d => d.SourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("gs_si_fk");
            });

            modelBuilder.Entity<JwtOption>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Audience).IsRequired();

                entity.Property(e => e.Issuer).IsRequired();

                entity.Property(e => e.Key).IsRequired();
            });

            modelBuilder.Entity<Platform>(entity =>
            {
                entity.Property(e => e.Url).HasColumnName("URL");
            });

            modelBuilder.Entity<Source>(entity =>
            {
                entity.Property(e => e.SourceUrl).HasColumnName("SourceURL");
            });

            modelBuilder.Entity<UserCredential>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("UserAccount_pkey");

                entity.Property(e => e.CategoryId).ValueGeneratedNever();

                entity.HasOne(d => d.Category)
                    .WithOne(p => p.UserCredential)
                    .HasForeignKey<UserCredential>(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ua_cci_fk");

                entity.HasOne(d => d.Platform)
                    .WithMany(p => p.UserCredential)
                    .HasForeignKey(d => d.PlatformId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ua_pi_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
