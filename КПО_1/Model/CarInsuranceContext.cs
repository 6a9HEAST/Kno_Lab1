using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace КПО_1;

public partial class CarInsuranceContext : DbContext
{
    public CarInsuranceContext()
    {
    }

    public CarInsuranceContext(DbContextOptions<CarInsuranceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<InsuranceAgent> InsuranceAgents { get; set; }

    public virtual DbSet<InsuranceCase> InsuranceCases { get; set; }

    public virtual DbSet<Model> Models { get; set; }

    public virtual DbSet<Owner> Owners { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=Car_insurance;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=Yes");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contract>(entity =>
        {
            entity.ToTable("contract");

            entity.Property(e => e.ContractId)
                .ValueGeneratedNever()
                .HasColumnName("contract_id");
            entity.Property(e => e.ConslusionDate)
                .HasColumnType("date")
                .HasColumnName("conslusion_date");
            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("end_date");
            entity.Property(e => e.InsuranceAgentId).HasColumnName("insurance_agent_id");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
            entity.Property(e => e.ValueOfInsuranceCases)
                .HasColumnType("money")
                .HasColumnName("value_of_insurance_cases");
            entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

            entity.HasOne(d => d.InsuranceAgent).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.InsuranceAgentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_contract_insurance_agent");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_contract_vehicle");
        });

        modelBuilder.Entity<InsuranceAgent>(entity =>
        {
            entity.ToTable("insurance_agent");

            entity.Property(e => e.InsuranceAgentId)
                .ValueGeneratedNever()
                .HasColumnName("insurance_agent_id");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("full_name");
        });

        modelBuilder.Entity<InsuranceCase>(entity =>
        {
            entity.ToTable("insurance_case", tb => tb.HasTrigger("update_contract_total"));

            entity.Property(e => e.InsuranceCaseId)
                .ValueGeneratedNever()
                .HasColumnName("insurance_case_id");
            entity.Property(e => e.CaseType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("case_type");
            entity.Property(e => e.ContractId).HasColumnName("contract_id");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.Value).HasColumnName("value");

            entity.HasOne(d => d.Contract).WithMany(p => p.InsuranceCases)
                .HasForeignKey(d => d.ContractId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_insurance_case_contract");
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.HasKey(e => e.ModelId).HasName("PK__Models__DC39CAF4E2C67F17");

            entity.Property(e => e.ModelId).HasColumnName("model_id");
            entity.Property(e => e.Name)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("name");
        });

        modelBuilder.Entity<Owner>(entity =>
        {
            entity.ToTable("owner");

            entity.Property(e => e.OwnerId)
                .ValueGeneratedNever()
                .HasColumnName("owner_id");
            entity.Property(e => e.DrivingExperienceYears).HasColumnName("driving_experience(years)");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("full_name");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.ToTable("vehicle");

            entity.Property(e => e.VehicleId)
                .ValueGeneratedNever()
                .HasColumnName("vehicle_id");
            entity.Property(e => e.Brand)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("brand");
            entity.Property(e => e.Mileage).HasColumnName("mileage");
            entity.Property(e => e.ModelId).HasColumnName("model_id");
            entity.Property(e => e.OwnerDrivingExperience).HasColumnName("owner_driving_experience");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.StateImplementationNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("state_implementation_number");

            entity.HasOne(d => d.Model).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.ModelId)
                .HasConstraintName("FK_vehicle_Models");

            entity.HasOne(d => d.Owner).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_vehicle_owner");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
