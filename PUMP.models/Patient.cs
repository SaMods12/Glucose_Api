namespace PUMP.models;

public class Patient
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Lastname { get; set; }
    public int? Age { get; set; }
    public string? Mail { get; set; }
    public string? Password { get; set; }
    public int DoctorId { get; set; } // Relación con el doctor
    public virtual Doctor Doctor { get; set; } // Navegación hacia Doctor
    public string? Phone { get; set; }
    public string? Address { get; set; }
}
