namespace PUMP.core.BL.Interfaces;

public interface IDoctor
{
    Task<bool> Create(models.Doctor doctor);
    Task<object?> Read(int? id);
    Task<bool> Update(models.Doctor doctor);
    Task<bool> Delete(int? id);
}