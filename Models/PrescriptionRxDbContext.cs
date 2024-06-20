using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PRESCRIPTIONS_RX.Models;

public partial class PrescriptionRxDbContext : DbContext
{
    public PrescriptionRxDbContext()
    {
    }

    public PrescriptionRxDbContext(DbContextOptions<PrescriptionRxDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<ContractSupervisor> ContractSupervisors { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<DoctorsAddress> DoctorsAddresses { get; set; }

    public virtual DbSet<Drug> Drugs { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PatientAddress> PatientAddresses { get; set; }

    public virtual DbSet<PharmaceuticalCompany> PharmaceuticalCompanies { get; set; }

    public virtual DbSet<PharmaciesAddress> PharmaciesAddresses { get; set; }

    public virtual DbSet<Pharmacy> Pharmacies { get; set; }

    public virtual DbSet<PharmacyStaff> PharmacyStaffs { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<SoldDrug> SoldDrugs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=GERSHOM-CHOMBA\\SQLEXPRESS;database=PRESCRIPTION_RX_DB;Integrated Security=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.ContractId).HasName("PK__CONTRACT__3F5DFF1426DF65D1");

            entity.ToTable("CONTRACTS");

            entity.Property(e => e.ContractId).HasColumnName("CONTRACT_ID");
            entity.Property(e => e.ContractText)
                .IsUnicode(false)
                .HasColumnName("CONTRACT_TEXT");
            entity.Property(e => e.Enddate).HasColumnName("ENDDATE");
            entity.Property(e => e.PharmaceuticalName)
                .HasMaxLength(225)
                .IsUnicode(false)
                .HasColumnName("PHARMACEUTICAL_NAME");
            entity.Property(e => e.PharmacyName)
                .HasMaxLength(225)
                .IsUnicode(false)
                .HasColumnName("PHARMACY_NAME");
            entity.Property(e => e.Startdate).HasColumnName("STARTDATE");

            entity.HasOne(d => d.PharmaceuticalNameNavigation).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.PharmaceuticalName)
                .HasConstraintName("FK_CONTRACTS_COMPANY");

            entity.HasOne(d => d.PharmacyNameNavigation).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.PharmacyName)
                .HasConstraintName("FK_CONTRACTS_PHARMACY");
        });

        modelBuilder.Entity<ContractSupervisor>(entity =>
        {
            entity.HasKey(e => e.SupervisorSsn).HasName("PK__CONTRACT__1F9EDFAFB86C0445");

            entity.ToTable("CONTRACT_SUPERVISOR");

            entity.Property(e => e.SupervisorSsn)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SUPERVISOR_SSN");
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CITY");
            entity.Property(e => e.ContractId).HasColumnName("CONTRACT_ID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.Gender)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("GENDER");
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MIDDLE_NAME");
            entity.Property(e => e.PharmacyName)
                .HasMaxLength(225)
                .IsUnicode(false)
                .HasColumnName("PHARMACY_NAME");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PHONE_NUMBER");
            entity.Property(e => e.PlotNumber).HasColumnName("PLOT_NUMBER");
            entity.Property(e => e.SN)
                .ValueGeneratedOnAdd()
                .HasColumnName("S/N");
            entity.Property(e => e.Street)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("STREET");

            entity.HasOne(d => d.Contract).WithMany(p => p.ContractSupervisors)
                .HasForeignKey(d => d.ContractId)
                .HasConstraintName("FK_SUPERVISOR_CONTRACT");

            entity.HasOne(d => d.PharmacyNameNavigation).WithMany(p => p.ContractSupervisors)
                .HasForeignKey(d => d.PharmacyName)
                .HasConstraintName("FK_SUPERVISOR_PHARMACY");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorsSsn).HasName("PK__DOCTORS__350FD4DAFBAFE201");

            entity.ToTable("DOCTORS");

            entity.Property(e => e.DoctorsSsn)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DOCTORS_SSN");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MIDDLE_NAME");
            entity.Property(e => e.SN)
                .ValueGeneratedOnAdd()
                .HasColumnName("S/N");
            entity.Property(e => e.Specialty)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SPECIALTY");
            entity.Property(e => e.YearsofExperience).HasColumnName("YEARSOF_EXPERIENCE");
        });

        modelBuilder.Entity<DoctorsAddress>(entity =>
        {
            entity.HasKey(e => e.SN).HasName("PK__DOCTORS___CA1DFBCAA2634B7D");

            entity.ToTable("DOCTORS_ADDRESSES");

            entity.Property(e => e.SN).HasColumnName("S/N");
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CITY");
            entity.Property(e => e.DoctorSsn)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DOCTOR_SSN");
            entity.Property(e => e.PlotNumber).HasColumnName("PLOT_NUMBER");
            entity.Property(e => e.Street)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("STREET");

            entity.HasOne(d => d.DoctorSsnNavigation).WithMany(p => p.DoctorsAddresses)
                .HasForeignKey(d => d.DoctorSsn)
                .HasConstraintName("FK_DOCTORS_ADDRESSES_DOCTORS");
        });

        modelBuilder.Entity<Drug>(entity =>
        {
            entity.HasKey(e => e.TradeName).HasName("PK__DRUGS__68213C57B00CD555");

            entity.ToTable("DRUGS");

            entity.Property(e => e.TradeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TRADE_NAME");
            entity.Property(e => e.Formula)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FORMULA");
            entity.Property(e => e.SN)
                .ValueGeneratedOnAdd()
                .HasColumnName("S/N");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientSsn).HasName("PK__PATIENTS__6274B5BC465E8BCA");

            entity.ToTable("PATIENTS");

            entity.Property(e => e.PatientSsn)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PATIENT_SSN");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.Gender)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("GENDER");
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MIDDLE_NAME");
            entity.Property(e => e.PrimaryPhysician)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PRIMARY_PHYSICIAN");
            entity.Property(e => e.SN)
                .ValueGeneratedOnAdd()
                .HasColumnName("S/N");
        });

        modelBuilder.Entity<PatientAddress>(entity =>
        {
            entity.HasKey(e => e.SN).HasName("PK__PATIENT___CA1DFBCA582B9DE1");

            entity.ToTable("PATIENT_ADDRESSES");

            entity.Property(e => e.SN).HasColumnName("S/N");
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CITY");
            entity.Property(e => e.PatientSsn)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PATIENT_SSN");
            entity.Property(e => e.PlotNumber).HasColumnName("PLOT_NUMBER");
            entity.Property(e => e.Street)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("STREET");

            entity.HasOne(d => d.PatientSsnNavigation).WithMany(p => p.PatientAddresses)
                .HasForeignKey(d => d.PatientSsn)
                .HasConstraintName("FK_PATIENT_ADDRESSES_PATIENTS");
        });

        modelBuilder.Entity<PharmaceuticalCompany>(entity =>
        {
            entity.HasKey(e => e.CompanyName).HasName("PK__PHARMACE__14099D274C630CD0");

            entity.ToTable("PHARMACEUTICAL_COMPANIES");

            entity.Property(e => e.CompanyName)
                .HasMaxLength(225)
                .IsUnicode(false)
                .HasColumnName("COMPANY_NAME");
            entity.Property(e => e.DrugTradeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DRUG_TRADE_NAME");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PHONE_NUMBER");
            entity.Property(e => e.SN)
                .ValueGeneratedOnAdd()
                .HasColumnName("S/N");
        });

        modelBuilder.Entity<PharmaciesAddress>(entity =>
        {
            entity.HasKey(e => e.SN).HasName("PK__PHARMACI__CA1DFBCA50BC68EB");

            entity.ToTable("PHARMACIES_ADDRESSES");

            entity.Property(e => e.SN).HasColumnName("S/N");
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CITY");
            entity.Property(e => e.PharmacyName)
                .HasMaxLength(225)
                .IsUnicode(false)
                .HasColumnName("PHARMACY_NAME");
            entity.Property(e => e.PlotNumber).HasColumnName("PLOT_NUMBER");
            entity.Property(e => e.Street)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("STREET");

            entity.HasOne(d => d.PharmacyNameNavigation).WithMany(p => p.PharmaciesAddresses)
                .HasForeignKey(d => d.PharmacyName)
                .HasConstraintName("FK_PHARMACIES_ADDRESSES_PHARMACY");
        });

        modelBuilder.Entity<Pharmacy>(entity =>
        {
            entity.HasKey(e => e.PharmacyName).HasName("PK__PHARMACI__B01DCBE2CB6893F9");

            entity.ToTable("PHARMACIES");

            entity.Property(e => e.PharmacyName)
                .HasMaxLength(225)
                .IsUnicode(false)
                .HasColumnName("PHARMACY_NAME");
            entity.Property(e => e.PharmaceuticalName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PHARMACEUTICAL_NAME");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PHONE_NUMBER");
            entity.Property(e => e.SN)
                .ValueGeneratedOnAdd()
                .HasColumnName("S/N");
        });

        modelBuilder.Entity<PharmacyStaff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__PHARMACY__EEFD934EED87B91C");

            entity.ToTable("PHARMACY_STAFF");

            entity.Property(e => e.StaffId).HasColumnName("STAFF_ID");
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CITY");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EMAIL_ADDRESS");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.Gender)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("GENDER");
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MIDDLE_NAME");
            entity.Property(e => e.PlotNumber).HasColumnName("PLOT_NUMBER");
            entity.Property(e => e.StaffPassword)
                .IsUnicode(false)
                .HasColumnName("STAFF_PASSWORD");
            entity.Property(e => e.Street)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("STREET");
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => e.PresciptionId).HasName("PK__PRESCRIP__5220F40C17A76877");

            entity.ToTable("PRESCRIPTIONS");

            entity.Property(e => e.PresciptionId).HasColumnName("PRESCIPTION_ID");
            entity.Property(e => e.DrugTradeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DRUG_TRADE_NAME");
            entity.Property(e => e.PatientSsn)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PATIENT_SSN");
            entity.Property(e => e.PrescribingPhysician)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PRESCRIBING_PHYSICIAN");
            entity.Property(e => e.PrescriptionDate).HasColumnName("PRESCRIPTION_DATE");
            entity.Property(e => e.Quantity).HasColumnName("QUANTITY");

            entity.HasOne(d => d.DrugTradeNameNavigation).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.DrugTradeName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TRADE_NAME");

            entity.HasOne(d => d.PatientSsnNavigation).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.PatientSsn)
                .HasConstraintName("FK_PRESCRIPTION_PATIENT");

            entity.HasOne(d => d.PrescribingPhysicianNavigation).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.PrescribingPhysician)
                .HasConstraintName("FK_PRESCRIPTION_PATIENT_PHYSICIAN");
        });

        modelBuilder.Entity<SoldDrug>(entity =>
        {
            entity.HasKey(e => e.SalesId).HasName("PK__SOLD_DRU__BAE7944595A4B9FB");

            entity.ToTable("SOLD_DRUGS");

            entity.Property(e => e.SalesId).HasColumnName("SALES_ID");
            entity.Property(e => e.DrugPrice)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("DRUG_PRICE");
            entity.Property(e => e.DrugTradeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DRUG_TRADE_NAME");
            entity.Property(e => e.PharmacyName)
                .HasMaxLength(225)
                .IsUnicode(false)
                .HasColumnName("PHARMACY_NAME");

            entity.HasOne(d => d.DrugTradeNameNavigation).WithMany(p => p.SoldDrugs)
                .HasForeignKey(d => d.DrugTradeName)
                .HasConstraintName("FK_SOLD_DRUGS_DRUGS");

            entity.HasOne(d => d.PharmacyNameNavigation).WithMany(p => p.SoldDrugs)
                .HasForeignKey(d => d.PharmacyName)
                .HasConstraintName("FK_SOLD_DRUGS");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
