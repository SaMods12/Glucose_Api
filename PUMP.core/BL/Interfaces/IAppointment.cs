namespace PUMP.core.BL.Interfaces;

public interface IAppointment
{
    Task<bool> Create(models.Appointment appointment);
    Task<object?> Read(int? id);
    Task<bool> Update(models.Appointment appointment);
    Task<bool> Delete(int? id);
}