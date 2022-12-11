
using TerminalDashboard.DbModel;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public partial class TerminalContext : DbContext
    {
        public TerminalContext()
        {
        }

        public TerminalContext(DbContextOptions<TerminalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Airplane> Airplanes { get; set; } = null!;
        public virtual DbSet<Airport> Airports { get; set; } = null!;
        public virtual DbSet<Firm> Firms { get; set; } = null!;
        public virtual DbSet<Flight> Flights { get; set; } = null!;
        public virtual DbSet<Suitcase> Suitcases { get; set; } = null!;
        public virtual DbSet<Passenger> Passengers { get; set; } = null!;
        public virtual DbSet<Name> Name { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=.;database=Terminal;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Passenger>(entity =>
            {
                entity.Property(e => e.ID)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.HasKey(i => i.ID);

                entity.Property(e => e.FirstName).HasMaxLength(200);

                entity.Property(e => e.LastName).HasMaxLength(200);

                entity.Property(e => e.FlightId).HasMaxLength(10);

                entity.HasOne(d => d.Flight)
                    .WithMany(p => p.Passengers)
                    .HasForeignKey(d => d.FlightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Passengers_Flights");
            });

            modelBuilder.Entity<Suitcase>(entity =>
            {
                entity.HasKey(i => i.ID);

                entity.Property(e => e.OwnerId).HasMaxLength(200);

                entity.Property(e => e.Color).HasMaxLength(200);

                entity.HasOne(d => d.Passenger)
                    .WithMany(p => p.Suitcases)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Suitcases_Passengers");
            });

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.HasKey(i => i.ID);

                entity.Property(e => e.DepartureTime).HasColumnType("datetime");

                entity.Property(e => e.LandingTime).HasColumnType("datetime");

                entity.Property(e => e.AirplaneID).HasMaxLength(200);

                entity.HasOne(d => d.Airplane)
                    .WithMany(p => p.Flights)
                    .HasForeignKey(d => d.AirplaneID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flights_Airplanes");
            });

            modelBuilder.Entity<Firm>(entity =>
            {
                entity.HasKey(i => i.ID);

                entity.Property(e => e.ID).HasMaxLength(200);
                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<Airplane>(entity =>
            {
                entity.HasKey(i => i.ID);

                entity.Property(e => e.FirmID).HasMaxLength(200);

                entity.Property(e => e.TotalSeats);

                entity.HasOne(d => d.Firm)
                    .WithMany(p => p.Airplanes)
                    .HasForeignKey(d => d.FirmID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Airplanes_Firms");
            });
            modelBuilder.Entity<Airport>(entity =>
            {
                entity.HasKey(i => i.Ident);
            });
            modelBuilder.Entity<Name>(entity =>
            {
                entity.HasKey(i => i.Id);
                entity.Property(e => e.FirstName).HasMaxLength(200);
                entity.Property(e => e.LastName).HasMaxLength(200);
            });
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
