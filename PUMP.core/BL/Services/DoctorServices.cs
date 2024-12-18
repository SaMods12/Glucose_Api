using PUMP.core.BL.Interfaces;
using PUMP.models;

namespace PUMP.core.BL.Services;

public class DoctorServices : IDoctor
{
    public Task<bool> Create(Doctor doctor)
    {
        bool result = false;

        using (var connection = new data.SQLServer.InitDb())
        {
            var query = (
                from item in connection.Doctor
                where item.Id == doctor.Id
                select item
            ).FirstOrDefault();
            if (query == null)
            {
                Doctor doc = new Doctor();
                doc.Id = doctor.Id;
                doc.Name = doctor.Name;
                doc.Lastname = doctor.Lastname;
                //doc.PatientId = doctor.PatientId;//
                doc.Phone = doctor.Phone;
                doc.Address = doctor.Address;
                connection.Doctor.Add(doc);
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
                var query = (from item in connection.Doctor
                             where item.Id == id.Value
                             select new
                             {
                                 item.Id,
                                 item.Name,
                                 item.Lastname,
                                // item.PatientId,//
                                 item.Phone,
                                 item.Address
                             }).FirstOrDefault();
                return Task.FromResult<object?>(query);
            }

            if (id == null)
            {
                var query = (
                    from item in connection.Doctor
                    select new
                    {
                        item.Id,
                        item.Name,
                        item.Lastname,
                      //  item.PatientId,//
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

    public Task<bool> Update(Doctor doctor)
    {
        bool result = false;
        using (var connection = new data.SQLServer.InitDb())
        {
            var query = (
                from item in connection.Doctor
                where item.Id == doctor.Id
                select item
            ).FirstOrDefault();

            if (query != null)
            {
                query.Id = doctor.Id;
                query.Name = doctor.Name;
                query.Lastname = doctor.Lastname;
              //  query.PatientId = doctor.PatientId;//
                query.Phone = doctor.Phone;
                query.Address = doctor.Address;
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
                from item in connection.Doctor
                where item.Id == id.Value
                select item
            ).FirstOrDefault();

            if (query != null)
            {
                connection.Doctor.Remove(query);
                result = connection.SaveChanges() > 0;
            }
        }

        return Task.FromResult(result);
    }
}