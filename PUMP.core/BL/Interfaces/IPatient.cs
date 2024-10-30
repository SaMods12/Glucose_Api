namespace PUMP.core.BL.Interfaces;

public interface IPatient
{
    // Create new patients
    Task<bool> Create(models.Patient patient);
    
    // Read all patients
    Task<object?> Read(int? id);
    
    // Update patients
    Task<bool> Update(models.Patient patient);
    
    // Delete patients
    Task<bool> Delete(int? id);
    
}