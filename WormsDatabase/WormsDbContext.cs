using Microsoft.EntityFrameworkCore;
using WormsBasic;

namespace WormsDatabase {
    public class WormsDbContext : DbContext {
        public DbSet<WorldState> WorldStates { get; set; }

        public WormsDbContext() {}

        public WormsDbContext(DbContextOptions<WormsDbContext> options) : base(options) {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseNpgsql(
                    "Host=localhost;Database=worms;Username=postgres;Password=1234"
                    );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<WorldState>(state => {
                state.HasKey(s => s.Id);
                state.HasIndex(s => s.Name)
                    .IsUnique();
                state.Property(s => s.Name)
                    .HasMaxLength(100)
                    .IsRequired();
                state.Ignore(s => s.GeneratedFood);
                state.Property(s => s.GeneratedFoodJson)
                    .IsUnicode(false)
                    .HasColumnName("GeneratedFood")
                    .IsRequired();
            });
        }
    }
}