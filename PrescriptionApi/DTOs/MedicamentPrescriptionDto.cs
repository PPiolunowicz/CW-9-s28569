using System.ComponentModel.DataAnnotations;

namespace PrescriptionApi.DTOs;

public class MedicamentPrescriptionDto
{
    public int IdMedicament { get; set; }
    public int? Dose { get; set; }
    public string? Details { get; set; }
}