using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Data
{
    public class TimeIsGoldDbContext : DbContext
    {
        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Professional> Professionals { get; set; }
        public DbSet<SchedulingType> SchedulingTypes { get; set; }
        public DbSet<Scheduling> Schedulings { get; set; }

        public TimeIsGoldDbContext(DbContextOptions<TimeIsGoldDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relacionamento um-para-muitos entre Enterprise e Professional
            modelBuilder.Entity<Enterprise>()
                .HasMany(e => e.Professionals)
                .WithOne(p => p.Enterprise)
                .HasForeignKey(p => p.EnterpriseId);

            // Relacionamento um-para-muitos entre Enterprise e SchedulingType
            modelBuilder.Entity<Enterprise>()
                .HasMany(e => e.SchedulingType)
                .WithOne(st => st.Enterprise)
                .HasForeignKey(st => st.EnterpriseId);

            // Relacionamento um-para-muitos entre Plan e Enterprise
            modelBuilder.Entity<Plan>()
                .HasMany(p => p.Enterprises)
                .WithOne(e => e.Plan)
                .HasForeignKey(e => e.PlanId);

            // Marcar Address como tipo complexo (owned)
            //modelBuilder.Entity<Enterprise>()
            //    .OwnsOne(e => e.Address, address =>
            //    {
            //        address.Property(a => a.Street).HasColumnName("Address_Street");
            //        address.Property(a => a.Number).HasColumnName("Address_Number");
            //        address.Property(a => a.City).HasColumnName("Address_City");
            //        address.Property(a => a.State).HasColumnName("Address_State");
            //        address.Property(a => a.Country).HasColumnName("Address_Country");
            //    });

            // Adicionando relacionamentos para Scheduling
            modelBuilder.Entity<Scheduling>()
                .HasOne(s => s.Client)
                .WithMany(c => c.Schedulings)
                .HasForeignKey(s => s.ClientId);

            modelBuilder.Entity<Scheduling>()
                .HasOne(s => s.Professional)
                .WithMany(p => p.Schedulings)
                .HasForeignKey(s => s.ProfessionalId);

            modelBuilder.Entity<Scheduling>()
                .HasOne(s => s.SchedulingType)
                .WithMany()
                .HasForeignKey(s => s.SchedulingTypeId);

            base.OnModelCreating(modelBuilder);
        }
    }
}