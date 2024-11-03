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
                pat.DoctorId = patient.DoctorId;
                pat.Phone = patient.Phone;
                pat.Address = patient.Address;
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
                        item.DoctorId,
                        item.Phone,
                        item.Address
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
                        item.DoctorId,
                        item.Phone,
                        item.Address
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
                query.DoctorId = patient.DoctorId;
                query.Phone = patient.Phone;
                query.Address = patient.Address;
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