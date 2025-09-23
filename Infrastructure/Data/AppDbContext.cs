using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Professional> Professionals { get; set; }
        public DbSet<SchedulingType> SchedulingTypes { get; set; }
        public DbSet<Scheduling> Schedulings { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Relacionamento muitos-para-muitos entre Client e Enterprise
        //    modelBuilder.Entity<Enterprise>()
        //        .HasMany(e => e.Clients)
        //        .WithMany(c => c.Enterprises);

        //    // Relacionamento um-para-muitos entre Enterprise e Professional
        //    modelBuilder.Entity<Enterprise>()
        //        .HasMany(e => e.Profissionais)
        //        .WithOne(p => p.Enterprise)
        //        .HasForeignKey(p => p.EnterpriseId);

        //    // Relacionamento um-para-muitos entre Enterprise e SchedulingType
        //    modelBuilder.Entity<Enterprise>()
        //        .HasMany(e => e.SchedulingType)
        //        .WithOne(st => st.Enterprise)
        //        .HasForeignKey(st => st.EnterpriseId);

        //    // Relacionamento um-para-muitos entre Plan e Enterprise
        //    modelBuilder.Entity<Plan>()
        //        .HasMany(p => p.Enterprises)
        //        .WithOne(e => e.Plan)
        //        .HasForeignKey(e => e.PlanId);

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}