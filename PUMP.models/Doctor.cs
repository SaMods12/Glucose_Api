namespace PUMP.models;

public class Doctor
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Lastname { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public virtual ICollection<Patient> Patients { get; set; } // Relaci�n con m�ltiples pacientes
}
