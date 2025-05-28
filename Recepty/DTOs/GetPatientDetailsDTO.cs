using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Recepty.Models;

namespace Recepty.DTOs;

public class GetPatientDetailsDTO
{
    [Required]
    public int IdPatient { get; set; }
    
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public DateTime BirthDate { get; set; }
    
    [Required]
    public List<PrescriptionDTO> Prescriptions { get; set; }
}

public class PrescriptionDTO
{
    [Required]
    public int IdPrescription { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
    [Required]
    public DoctorDTO Doctor { get; set; }
    [Required]
    public ICollection<MedicamentDTO> Medicaments { get; set; }
}


public class DoctorDTO
{
    [Required]
    public int IdDoctor { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }
}

public class MedicamentDTO
{
    [Required]
    public int IdMedicament { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string Type { get; set; }
    [Required]
    public int? Dose { get; set; }
    [Required]
    public string Details { get; set; }
}
