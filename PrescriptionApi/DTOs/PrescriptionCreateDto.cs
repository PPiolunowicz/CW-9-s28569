using System.ComponentModel.DataAnnotations;

namespace PrescriptionApi.DTOs;

public class PrescriptionCreateDto
{
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public PatientDto Patient { get; set; } = null!;
    public DoctorDto Doctor { get; set; } = null!;
    public List<MedicamentPrescriptionDto> Medicaments { get; set; } = new();
}