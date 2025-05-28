using Microsoft.EntityFrameworkCore;
using PrescriptionApi.Data;
using PrescriptionApi.DTOs;
using PrescriptionApi.Exceptions;
using PrescriptionApi.Models;

namespace PrescriptionApi.Services;

public class DbService (AppDbContext data) : IDbService
{
    public async Task<PatientGetDto> GetPatientDetailsAsync(int idPatient)
    {
        var result = await data.Patients
            .Where(p => p.IdPatient == idPatient)
            .Select(p => new PatientGetDto
            {
                IdPatient = p.IdPatient,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Birthdate = p.Birthdate,
                Prescriptions = p.Prescriptions
                    .OrderBy(r => r.DueDate)
                    .Select(pr => new PrescriptionDto
                    {
                        IdPrescription = pr.IdPrescription,
                        Date = pr.Date,
                        DueDate = pr.DueDate,
                        Doctor = new DoctorDto
                        {
                            IdDoctor = pr.Doctor.IdDoctor,
                            FirstName = pr.Doctor.FirstName,
                            LastName = pr.Doctor.LastName,
                            Email = pr.Doctor.Email
                        },
                        Medicaments = pr.PrescriptionMedicaments.Select(pm => new MedicamentDto
                        {
                            IdMedicament = pm.Medicament.IdMedicament,
                            Name = pm.Medicament.Name,
                            Description = pm.Medicament.Description,
                            Type = pm.Medicament.Type,
                            Dose = pm.Dose,
                            Details = pm.Details
                        }).ToList()
                    }).ToList()
            })
            .FirstOrDefaultAsync();
        return result ?? throw new NotFoundException($"Patient with id: {idPatient} not found");
    }

    public Task<int> AddPrescriptionAsync(PrescriptionGetDto dto)
    {
        throw new NotImplementedException();
    }


    public async Task<int> AddPrescriptionAsync(PrescriptionCreateDto  dto)
{
    if (dto.Medicaments.Count > 10)
    {
        throw new ArgumentException("Prescription cannot contain more than 10 medicaments.");
    }

    if (dto.DueDate < dto.Date)
    {
        throw new ArgumentException("DueDate must be greater than or equal to Date.");
    }

    var doctor = await data.Doctors.FindAsync(dto.Doctor.IdDoctor);
    if (doctor == null)
    {
        throw new NotFoundException($"Doctor with id {dto.Doctor.IdDoctor} not found.");
    }

    var patient = await data.Patients.FirstOrDefaultAsync(p => p.IdPatient == dto.Patient.IdPatient);
    if (patient == null)
    {
        patient = new Patient
        {
            FirstName = dto.Patient.FirstName,
            LastName = dto.Patient.LastName,
            Birthdate = dto.Patient.Birthdate
        };
        await data.Patients.AddAsync(patient);
        await data.SaveChangesAsync(); // Save to get IdPatient
    }

    var medicaments = new List<PrescriptionMedicament>();
    foreach (var m in dto.Medicaments)
    {
        var medicament = await data.Medicaments.FindAsync(m.IdMedicament);
        if (medicament == null)
        {
            throw new NotFoundException($"Medicament with id {m.IdMedicament} not found.");
        }

        medicaments.Add(new PrescriptionMedicament
        {
            IdMedicament = m.IdMedicament,
            Dose = m.Dose,
            Details = m.Details
        });
    }

    var prescription = new Prescription
    {
        Date = dto.Date,
        DueDate = dto.DueDate,
        IdDoctor = doctor.IdDoctor,
        IdPatient = patient.IdPatient,
        PrescriptionMedicaments = medicaments
    };

    await data.Prescriptions.AddAsync(prescription);
    await data.SaveChangesAsync();

    return  prescription.IdPrescription;
}

}
