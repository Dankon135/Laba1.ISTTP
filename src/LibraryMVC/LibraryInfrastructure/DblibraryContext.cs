using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LibraryDomain.Model;

namespace LibraryInfrastructure;
public partial class DblibraryContext : DbContext
{
    public DblibraryContext()
    {
    }

    public DblibraryContext(DbContextOptions<DblibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Laboratory> Laboratories { get; set; }

    public virtual DbSet<Personnel> Personnel { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<ResearcherWork> ResearcherWorks { get; set; }

    public virtual DbSet<ScientificWork> ScientificWorks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-BFPPDC8\\SQLEXPRESS; Database=DBLibrary; Trusted_Connection=True; TrustServerCertificate=True; ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Laboratory>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DepartamentId).HasColumnName("Departament_ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Departament).WithMany(p => p.Laboratories)
                .HasForeignKey(d => d.DepartamentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Laboratories_Departments");
        });

        modelBuilder.Entity<Personnel>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.DepartamentId).HasColumnName("Departament_ID");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Full_Name");
            entity.Property(e => e.LaboratoryId).HasColumnName("Laboratory_ID");
            entity.Property(e => e.PositionEnd).HasColumnName("Position_End");
            entity.Property(e => e.PositionId).HasColumnName("Position_ID");
            entity.Property(e => e.PositionStart).HasColumnName("Position_Start");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Personnel)
                .HasForeignKey<Personnel>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Personnel_Departments");

            entity.HasOne(d => d.Laboratory).WithMany(p => p.Personnel)
                .HasForeignKey(d => d.LaboratoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Personnel_Laboratories");

            entity.HasOne(d => d.Position).WithMany(p => p.Personnel)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Personnel_Position");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.ToTable("Position");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ResearcherWork>(entity =>
        {
            entity.ToTable("Researcher_Work");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Contribution)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("Created_At");
            entity.Property(e => e.ResearcherId).HasColumnName("Researcher_ID");
            entity.Property(e => e.ScientificWorkId).HasColumnName("Scientific_Work_ID");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.ResearcherWork)
                .HasForeignKey<ResearcherWork>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Researcher_Work_Scientific_Works1");
        });

        modelBuilder.Entity<ScientificWork>(entity =>
        {
            entity.ToTable("Scientific_Works");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Client)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ClientAddress)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Client_Address");
            entity.Property(e => e.Field)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PersonnelId).HasColumnName("Personnel_ID");
            entity.Property(e => e.ResearcherId).HasColumnName("Researcher_ID");
            entity.Property(e => e.Subordination)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Personnel).WithMany(p => p.ScientificWorks)
                .HasForeignKey(d => d.PersonnelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Scientific_Works_Personnel");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
