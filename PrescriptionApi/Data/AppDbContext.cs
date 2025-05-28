using Microsoft.EntityFrameworkCore;
using PrescriptionApi.Models;

namespace PrescriptionApi.Data;

public class AppDbContext :DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    public DbSet<Patient> Patients { get; set; }
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var doctor = new Doctor
        {
            IdDoctor = 1,
            FirstName = "Jan",
            LastName = "Kowalski",
            Email = "jan.kowalski@clinic.com"
        };

        var patient = new Patient
        {
            IdPatient = 1,
            FirstName = "Anna",
            LastName = "Nowak",
            Birthdate = new DateTime(1990, 5, 1)
        };

        var medicament = new Medicament
        {
            IdMedicament = 1,
            Name = "Ibuprofen",
            Description = "Pain reliever",
            Type = "Tablet"
        };

        var prescription = new Prescription
        {
            IdPrescription = 1,
            Date = new DateTime(2024, 6, 1),
            DueDate = new DateTime(2024, 7, 1),
            IdPatient = 1,
            IdDoctor = 1
        };

        var prescriptionMedicament = new PrescriptionMedicament
        {
            IdMedicament = 1,
            IdPrescription = 1,
            Dose = 2,
            Details = "Take twice daily"
        };

        modelBuilder.Entity<Doctor>().HasData(doctor);
        modelBuilder.Entity<Patient>().HasData(patient);
        modelBuilder.Entity<Medicament>().HasData(medicament);
        modelBuilder.Entity<Prescription>().HasData(prescription);
        modelBuilder.Entity<PrescriptionMedicament>().HasData(prescriptionMedicament);
    }
}