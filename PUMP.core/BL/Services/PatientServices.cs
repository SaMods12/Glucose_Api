using PUMP.core.BL.Interfaces;
using PUMP.models;

namespace PUMP.core.BL.Services;

public class PatientServices : IPatient
{
    public Task<bool> Create(Patient patient)
    {
        bool result = false;

        using (var connection = new data.SQLServer.InitDb())
        {
            var query = (
                from item in connection.Patient
                where item.Id == patient.Id
                select item
            ).FirstOrDefault();
            if (query == null)
            {
                Patient pat = new Patient();
                pat.Id = patient.Id;
                pat.Name = patient.Name;
                pat.Lastname = patient.Lastname;
                pat.Age = patient.Age;
                pat.Mail = patient.Mail;
                pat.Password = patient.Password;
                pat.Doctor = patient.Doctor;
                pat.Appointment = patient.Appointment;
                pat.Measurement = patient.Measurement;
                pat.Phone = patient.Phone;
                pat.Addreess = patient.Addreess;
                connection.Patient.Add(pat);
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
                var query = (from item in connection.Patient
                    where item.Id == id.Value
                    select new
                    {
                        item.Id,
                        item.Name,
                        item.Lastname,
                        item.Age,
                        item.Mail,
                        item.Password,
                        item.Doctor,
                        item.Appointment,
                        item.Measurement,
                        item.Phone,
                        item.Addreess
                    }).FirstOrDefault();
                return Task.FromResult<object?>(query);
            }

            if (id == null)
            {
                var query = (
                    from item in connection.Patient
                    select new
                    {
                        item.Id,
                        item.Name,
                        item.Lastname,
                        item.Age,
                        item.Mail,
                        item.Password,
                        item.Doctor,
                        item.Appointment,
                        item.Measurement,
                        item.Phone,
                        item.Addreess
                    }).ToList();
                
                return Task.FromResult<object?>(query);
            }
            else
            {
                return Task.FromResult<object?>(null);
            }
        }
    }

    public Task<bool> Update(Patient patient)
    {
        bool result = false;
        using (var connection = new data.SQLServer.InitDb())
        {
            var query = (
                from item in connection.Patient
                where item.Id == patient.Id
                select item
            ).FirstOrDefault();

            if (query != null)
            {
                query.Id = patient.Id;
                query.Name = patient.Name;
                query.Lastname = patient.Lastname;
                query.Age = patient.Age;
                query.Mail = patient.Mail;
                query.Password = patient.Password;
                query.Doctor = patient.Doctor;
                query.Appointment = patient.Appointment;
                query.Measurement = patient.Measurement;
                query.Phone = patient.Phone;
                query.Addreess = patient.Addreess;
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
                from item in connection.Patient
                where item.Id == id.Value
                select item
            ).FirstOrDefault();

            if (query != null)
            {
                connection.Patient.Remove(query);
                result = connection.SaveChanges() > 0;
            }
        }

        return Task.FromResult(result);    }
}