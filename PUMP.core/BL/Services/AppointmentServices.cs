using PUMP.core.BL.Interfaces;
using PUMP.models;

namespace PUMP.core.BL.Services;

public class AppointmentServices : IAppointment
{
    public Task<bool> Create(Appointment appointment)
    {
        bool result = false;

        using (var connection = new data.SQLServer.InitDb())
        {
            var query = (
                from item in connection.Appointment
                where item.Id == appointment.Id
                select item
            ).FirstOrDefault();
            if (query == null)
            {
                Appointment newAppointment = new Appointment
                {
                    Id = appointment.Id,
                    AppointmentDate = appointment.AppointmentDate,
                    PatientId = appointment.PatientId,
                    DoctorId = appointment.DoctorId
                };
                connection.Appointment.Add(newAppointment);
                result = connection.SaveChanges() > 0;
            }
        }

        return Task.FromResult(result);
    }

    public Task<object?> Read(int? id)
    {
        using (var connection = new data.SQLServer.InitDb())
        {
            if (id.HasValue)
            {
                var query = (from item in connection.Appointment
                             where item.Id == id.Value
                             select new
                             {
                                 item.Id,
                                 item.AppointmentDate,
                                 item.PatientId,
                                 item.DoctorId
                             }).FirstOrDefault();
                return Task.FromResult<object?>(query);
            }

            if (id == null)
            {
                var query = (
                    from item in connection.Appointment
                    select new
                    {
                        item.Id,
                        item.AppointmentDate,
                        item.PatientId,
                        item.DoctorId
                    }).ToList();

                return Task.FromResult<object?>(query);
            }
            else
            {
                return Task.FromResult<object?>(null);
            }
        }
    }

    public Task<bool> Update(Appointment appointment)
    {
        bool result = false;
        using (var connection = new data.SQLServer.InitDb())
        {
            var query = (
                from item in connection.Appointment
                where item.Id == appointment.Id
                select item
            ).FirstOrDefault();

            if (query != null)
            {
                query.AppointmentDate = appointment.AppointmentDate;
                query.PatientId = appointment.PatientId;
                query.DoctorId = appointment.DoctorId;
                result = connection.SaveChanges() > 0;
            }
        }

        return Task.FromResult(result);
    }

    public Task<bool> Delete(int? id)
    {
        bool result = false;
        using (var connection = new data.SQLServer.InitDb())
        {
            var query = (
                from item in connection.Appointment
                where item.Id == id.Value
                select item
            ).FirstOrDefault();

            if (query != null)
            {
                connection.Appointment.Remove(query);
                result = connection.SaveChanges() > 0;
            }
        }

        return Task.FromResult(result);
    }
}
