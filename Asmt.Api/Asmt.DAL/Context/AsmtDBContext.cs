using Asmt.DAL.Interfaces;
using Asmt.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Asmt.DAL.Context;

public class AsmtDBContext : DbContext
{
    public AsmtDBContext(DbContextOptions<AsmtDBContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Customer entity
        modelBuilder.Entity<Customer>(entity =>
        {
            SetAtomDefaults(entity);
            entity.HasMany(e => e.Orders)
                  .WithOne(e => e.Customer)
                  .HasForeignKey(e => e.CustomerId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure Order entity
        modelBuilder.Entity<Order>(entity =>
        {
            SetAtomDefaults(entity);
            
            entity.Property(e => e.PricePreTax).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Tax).HasColumnType("decimal(18, 2)");
            entity.Navigation(e => e.OrderItems).AutoInclude();
            entity.Navigation(e => e.Customer).AutoInclude();

            entity.HasMany(e => e.OrderItems)
                  .WithOne(e => e.Order)
                  .HasForeignKey(e => e.OrderId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.Ignore(e => e.Price);
        });

        // Configure OrderItem entity
        modelBuilder.Entity<OrderItem>(entity =>
        {
            SetAtomDefaults(entity);
            
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        // Configure User entity
        modelBuilder.Entity<User>(entity =>
        {
            SetAtomDefaults(entity);
            
            entity.HasIndex(e => e.Email).IsUnique();
        }); 
    }

    /// <summary>
    /// Sets the default values for the atom entity.
    /// </summary>
    /// <typeparam name="T">The type of the atom entity.</typeparam>
    /// <param name="entity">The entity to set the defaults for.</param>
    private void SetAtomDefaults<T>(EntityTypeBuilder<T> entity) where T : class, IAtom
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).ValueGeneratedOnAdd();
        entity.Property(e => e.CreateDT).HasDefaultValueSql("(GETUTCDATE())");
    }
}