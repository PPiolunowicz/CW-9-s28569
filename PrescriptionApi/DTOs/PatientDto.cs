namespace PrescriptionApi.DTOs;

public class PatientDto
{
    public int IdPatient { get; set; }  // jeśli pacjent nowy → 0 lub brak, jeśli istniejący → podajesz ID
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime Birthdate { get; set; }
}