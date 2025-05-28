using Microsoft.EntityFrameworkCore;
using Recepty.Data;
using Recepty.DTOs;
using Recepty.Exceptions;
using Recepty.Models;

namespace Recepty.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly AppDbContext _dbContext;

    public PrescriptionService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> AddPrescriptionAsync(AddPrescriptionDTO addPrescriptionDTO)
    {
        // Walidacja daty
        if (addPrescriptionDTO.DueDate < addPrescriptionDTO.Date)
        {
            throw new BadRequestException("Data ważności recepty nie może być wcześniejsza niż data wystawienia.");
        }

        // Walidacja leków
        if (addPrescriptionDTO.Medicaments.Count > 10)
        {
            throw new BadRequestException("Recepta może zawierać maksymalnie 10 leków.");
        }

        // Sprawdzenie czy lekarz istnieje
        var doctorExists = await _dbContext.Doctors.AnyAsync(d => d.IdDoctor == addPrescriptionDTO.IdDoctor);
        if (!doctorExists)
        {
            throw new NotFoundException($"Lekarz o ID {addPrescriptionDTO.IdDoctor} nie został znaleziony.");
        }

        // Sprawdzenie czy wszystkie leki istnieją
        var medicamentIds = addPrescriptionDTO.Medicaments.Select(m => m.IdMedicament).ToList();
        var existingMedicaments = await _dbContext.Medicaments
            .Where(m => medicamentIds.Contains(m.Idmedicament))
            .Select(m => m.Idmedicament)
            .ToListAsync();

        if (existingMedicaments.Count != medicamentIds.Count)
        {
            var missingMedicaments = medicamentIds.Except(existingMedicaments);
            throw new NotFoundException($"Leki o ID {string.Join(", ", missingMedicaments)} nie zostały znalezione.");
        }

        // Rozpoczęcie transakcji
        using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            // Sprawdzenie czy pacjent istnieje
            Patient patient;
            if (addPrescriptionDTO.Patient.IdPatient.HasValue)
            {
                // Pacjent istnieje - sprawdzamy czy jest w bazie
                patient = await _dbContext.Patients.FirstOrDefaultAsync(p => p.IdPatient == addPrescriptionDTO.Patient.IdPatient.Value);
                
                if (patient == null)
                {
                    // Pacjent nie istnieje - tworzymy nowego z podanym ID
                    patient = new Patient
                    {
                        IdPatient = addPrescriptionDTO.Patient.IdPatient.Value,
                        FirstName = addPrescriptionDTO.Patient.FirstName,
                        LastName = addPrescriptionDTO.Patient.LastName,
                        Birthdate = addPrescriptionDTO.Patient.Birthdate
                    };
                    _dbContext.Patients.Add(patient);
                    await _dbContext.SaveChangesAsync();
                }
            }
            else
            {
                // Tworzenie nowego pacjenta
                patient = new Patient
                {
                    FirstName = addPrescriptionDTO.Patient.FirstName,
                    LastName = addPrescriptionDTO.Patient.LastName,
                    Birthdate = addPrescriptionDTO.Patient.Birthdate
                };
                _dbContext.Patients.Add(patient);
                await _dbContext.SaveChangesAsync();
            }

            // Tworzenie nowej recepty
            var prescription = new Prescription
            {
                Date = addPrescriptionDTO.Date,
                DueDate = addPrescriptionDTO.DueDate,
                IdPatient = patient.IdPatient,
                IdDoctor = addPrescriptionDTO.IdDoctor,
            };
            _dbContext.Prescriptions.Add(prescription);
            await _dbContext.SaveChangesAsync();

            // Dodawanie do  receptty
            foreach (var medicamentDto in addPrescriptionDTO.Medicaments)
            {
                var prescriptionMedicament = new PrescriptionMedicament
                {
                    IdPrescription = prescription.IdPrescription,
                    IdMedicament = medicamentDto.IdMedicament,
                    Dose = medicamentDto.Dose,
                    Details = medicamentDto.Details
                };
                _dbContext.PrescriptionMedicaments.Add(prescriptionMedicament);
            }
            await _dbContext.SaveChangesAsync();

            await transaction.CommitAsync();
            return prescription.IdPrescription;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}