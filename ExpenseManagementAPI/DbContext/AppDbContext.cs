using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Expense-Category Relationship
        modelBuilder.Entity<Expense>()
            .HasOne(e => e.Category)
            .WithMany(c => c.Expenses)
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);  // Prevents category deletion if used in expenses

        // Seed Categories
        modelBuilder.Entity<Category>().HasData(
            new Category { ID = 1, Name = "Food" },
            new Category { ID = 2, Name = "Transport" },
            new Category { ID = 3, Name = "Entertainment" },
            new Category { ID = 4, Name = "Shopping" },
            new Category { ID = 5, Name = "Health" },
            new Category { ID = 6, Name = "Other" }
        );
    }
}
