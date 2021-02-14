using System;
using Data.Cinema.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Data.Cinema
{
    public partial class CinemaContext : DbContext
    {
        public CinemaContext()
        {
        }

        public CinemaContext(DbContextOptions<CinemaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Show> Shows { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseNpgsql("Server=localhost;Port=5432;Database=cinema;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.UTF-8");

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("movies");

                entity.Property(e => e.Movieid).HasColumnName("movieid");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("rooms");

                entity.Property(e => e.Roomid).HasColumnName("roomid");

                entity.Property(e => e.Columns).HasColumnName("columns");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Rows).HasColumnName("rows");
            });

            modelBuilder.Entity<Show>(entity =>
            {
                entity.HasKey(e => e.Showid)
                    .HasName("showtimes_pkey");

                entity.ToTable("showtimes");

                entity.Property(e => e.Showid).HasColumnName("showid");

                entity.Property(e => e.Movieid).HasColumnName("movieid");

                entity.Property(e => e.Roomid).HasColumnName("roomid");

                entity.Property(e => e.Start).HasColumnName("start");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Showtimes)
                    .HasForeignKey(d => d.Movieid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("showtimes_movieid_fkey");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Showtimes)
                    .HasForeignKey(d => d.Roomid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("showtimes_roomid_fkey");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("tickets");

                entity.Property(e => e.Ticketid)
                    .HasColumnName("ticketid")
                    .HasDefaultValueSql("nextval('tickets_ticket_id_seq'::regclass)");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Rownum).HasColumnName("rownum");

                entity.Property(e => e.Seatnum).HasColumnName("seatnum");

                entity.Property(e => e.Showid)
                    .IsRequired()
                    .HasColumnName("showid");

                entity.HasOne(d => d.Show)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.Showid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tickets_showid_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
