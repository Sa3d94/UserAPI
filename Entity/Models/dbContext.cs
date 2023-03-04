using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models;

public partial class dbContext : DbContext
{
  public dbContext()
  {
  }

  public dbContext(DbContextOptions<dbContext> options)
      : base(options)
  {
  }

  public virtual DbSet<User> Users { get; set; }


  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<User>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");

      entity.ToTable("users");

      entity.Property(e => e.Id)
              .HasMaxLength(50)
              .HasColumnName("ID");
      entity.Property(e => e.Email).HasMaxLength(45);
      entity.Property(e => e.FirstName)
              .HasMaxLength(45)
              .HasColumnName("First Name");
      entity.Property(e => e.LastName)
              .HasMaxLength(45)
              .HasColumnName("Last Name");
      entity.Property(e => e.MarketingConsent).HasColumnName("Marketing Consent");
    });

    OnModelCreatingPartial(modelBuilder);
  }

  partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
