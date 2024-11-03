namespace PUMP.core.BL.Interfaces;

public interface IMeasurement
{
    Task<bool> Create(models.Measurement measurement);
    Task<object?> Read(int? id);
    Task<bool> Update(models.Measurement measurement);
    Task<bool> Delete(int? id);
}