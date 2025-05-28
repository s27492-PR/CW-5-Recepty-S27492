using Microsoft.EntityFrameworkCore;
using Recepty.Models;

namespace Recepty.Data;

public class AppDbContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var doctors = new Doctor
        {
            IdDoctor = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "pawel.rachocki@outlook.com"
        };

        var patients = new Patient
        {
            IdPatient = 1,
            FirstName = "Jan",
            LastName = "Nowak",
            Birthdate = new DateTime(1990, 3, 15)
        };

        var medicaments = new Medicament
        {
            Idmedicament = 1,
            Name = "Lek",
            Description = "opis",
            Type = "typ testowy"
        };

        var prescriptions = new Prescription
        {
            IdPrescription = 1,
            Date = new DateTime(2025, 3, 15),
            DueDate = new DateTime(2025, 5, 15),
            IdPatient = 1,
            IdDoctor = 1
        };
        var prescriptionmedicaments = new PrescriptionMedicament
        {
            IdMedicament = 1,
            IdPrescription = 1,
            Dose = null,
            Details = "Testowo",
        };
        
        modelBuilder.Entity<Doctor>().HasData(doctors);
        modelBuilder.Entity<Patient>().HasData(patients);
        modelBuilder.Entity<Medicament>().HasData(medicaments);
        modelBuilder.Entity<Prescription>().HasData(prescriptions);
        modelBuilder.Entity<PrescriptionMedicament>().HasData(prescriptionmedicaments);
    }
}