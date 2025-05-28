using System.ComponentModel.DataAnnotations;

namespace Recepty.DTOs;

public class AddPrescriptionDTO
{
    [Required]
    public AddPatientDto Patient { get; set; } = null!;

    [Required]
    [MaxLength(10, ErrorMessage = "Recepta może mieć maksymalnie 10 leków.")]
    public ICollection<AddPrescriptionMedicamentDto> Medicaments { get; set; } = null!;

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public DateTime DueDate { get; set; }

    [Required]
    public int IdDoctor { get; set; }
}

public class AddPrescriptionMedicamentDto
{
    [Required]
    public int IdMedicament { get; set; }

    [Required]
    // Dose może być null, więc nie dajemy Required
    public int? Dose { get; set; }

    [Required, MaxLength(100)]
    public string Details { get; set; } = null!;
}

public class AddPatientDto
{
    [Required]
    public int? IdPatient { get; set; }
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    public DateTime Birthdate { get; set; }
}

