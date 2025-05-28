using Recepty.DTOs;

namespace Recepty.Services;

public interface IPrescriptionService
{
    Task<int> AddPrescriptionAsync(AddPrescriptionDTO addPrescriptionDTO);
}