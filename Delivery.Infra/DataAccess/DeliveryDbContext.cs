using Delivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Infra.DataAccess;

public class DeliveryDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Deliveryman> Deliveryman { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Feed> Feed { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<ProductVariation> ProductVariation { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Deliveryman>(entity =>
            {
                entity.ToTable("deliveryman");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Cpf)
                    .HasColumnName("cpf")
                    .HasMaxLength(15)
                    .IsRequired();

                entity.Property(e => e.DateOfBirth)
                    .HasColumnName("date_of_birth").HasColumnType("date");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at").HasColumnType("timestamp with time zone");
                
                entity.Property(e => e.UserId)
                    .HasColumnName("user_id");
                entity.HasOne(d => d.User)
                    .WithOne()
                    .HasForeignKey<Deliveryman>(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

            }
        );
        
        modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.UserName)
                    .HasColumnName("username")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasMaxLength(50)
                    .IsRequired();
                
                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at").HasColumnType("timestamp with time zone");
                
                entity.Property(e => e.Guid)
                    .HasColumnName("guid").HasColumnType("uuid");
            }
        );
        
        modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Cod)
                    .HasColumnName("cod")
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(100)
                    .IsRequired();
                
                entity.Property(e => e.UnitMeasure)
                    .HasColumnName("unit_measure")
                    .HasMaxLength(5)
                    .IsRequired();
                
                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at").HasColumnType("timestamp with time zone");
                
                entity.Property(e => e.Guid)
                    .HasColumnName("guid").HasColumnType("uuid");
            }
        );
        
        modelBuilder.Entity<ProductVariation>(entity =>
            {
                entity.ToTable("product_variation");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Cod)
                    .HasColumnName("cod")
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.Enable)
                    .HasColumnName("enable")
                    .IsRequired();
                
                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at").HasColumnType("timestamp with time zone");
                
                entity.Property(e => e.Guid)
                    .HasColumnName("guid").HasColumnType("uuid");
                
                       
                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id");
                
                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            }
        );
        
        modelBuilder.Entity<Feed>(entity =>
            {
                entity.ToTable("feed");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Duration)
                    .HasColumnName("duration")
                    .IsRequired();

                entity.Property(e => e.Likes)
                    .HasColumnName("likes");

                entity.Property(e => e.ThumbnailUrl)
                    .HasColumnName("thumbnail_url")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(50)
                    .IsRequired();
                
                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at").HasColumnType("timestamp with time zone");
                
                entity.Property(e => e.VideoUrl)
                    .HasColumnName("video_url");
                
                entity.Property(e => e.UserId)
                    .HasColumnName("user_id");
                
                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            }
        );
        base.OnModelCreating(modelBuilder);
    }
}