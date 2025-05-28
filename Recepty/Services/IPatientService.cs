using Recepty.DTOs;

namespace Recepty.Services;

public interface IPatientService
{
    Task<GetPatientDetailsDTO> GetPatientDetailsAsync(int idPatient);
}