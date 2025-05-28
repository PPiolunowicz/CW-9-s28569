namespace PrescriptionApi.DTOs;

public class PatientGetDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime Birthdate { get; set; }
    public ICollection<PrescriptionDto> Prescriptions { get; set; } = new List<PrescriptionDto>();
}

public class PrescriptionDto
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public DoctorDto Doctor { get; set; } = null!;
    public ICollection<MedicamentDto> Medicaments { get; set; } = new List<MedicamentDto>();
}

// public class DoctorDto
// {
//     public int IdDoctor { get; set; }
//     public string FirstName { get; set; } = null!;
//     public string LastName { get; set; } = null!;
//     public string Email { get; set; } = null!;
// }

// public class MedicamentDto
// {
//     public int IdMedicament { get; set; }
//     public string Name { get; set; } = null!;
//     public string Description { get; set; } = null!;
//     public string Type { get; set; } = null!;
//     public int? Dose { get; set; }
//     public string? Details { get; set; }
// }