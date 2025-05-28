using PrescriptionApi.Models;

namespace PrescriptionApi.DTOs;

public class PrescriptionGetDto
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }

    public DoctorDto Doctor { get; set; } = null!;
    public List<MedicamentDto> Medicaments { get; set; } = new();
}
