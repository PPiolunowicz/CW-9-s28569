using PrescriptionApi.DTOs;

namespace PrescriptionApi.Services;

public interface IDbService
{
    
    public Task<PatientGetDto> GetPatientDetailsAsync(int idPatient);
    public  Task<int> AddPrescriptionAsync(PrescriptionCreateDto  dto);
    
}