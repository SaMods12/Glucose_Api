using PUMP.core.BL.Interfaces;
using PUMP.models;
using System.Diagnostics.Metrics;

namespace PUMP.core.BL.Services;

public class MeasurementServices : IMeasurement
{
    public Task<bool> Create(Measurement measurement)
    {
        bool result = false;

        using (var connection = new data.SQLServer.InitDb())
        {
            var query = (
                from item in connection.Measurement
                where item.Id == measurement.Id
                select item
            ).FirstOrDefault();
            if (query == null)
            {
                Measurement newMeasurement = new Measurement
                {
                    Id = measurement.Id,
                    GlucoseLevel = measurement.GlucoseLevel,
                    MeasurementDate = measurement.MeasurementDate,
                    PatientId = measurement.PatientId,
                };
                connection.Measurement.Add(newMeasurement);
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
                var query = (from item in connection.Measurement
                             where item.Id == id.Value
                             select new
                             {
                                 item.Id,
                                 item.GlucoseLevel,
                                 item.MeasurementDate,
                                 item.PatientId,
                             }).FirstOrDefault();
                return Task.FromResult<object?>(query);
            }

            if (id == null)
            {
                var query = (
                    from item in connection.Measurement
                    select new
                    {
                        item.Id,
                        item.GlucoseLevel,
                        item.MeasurementDate,
                        item.PatientId,
                    }).ToList();

                return Task.FromResult<object?>(query);
            }
            else
            {
                return Task.FromResult<object?>(null);
            }
        }
    }

    public Task<bool> Update(Measurement measurement)
    {
        bool result = false;
        using (var connection = new data.SQLServer.InitDb())
        {
            var query = (
                from item in connection.Measurement
                where item.Id == measurement.Id
                select item
            ).FirstOrDefault();

            if (query != null)
            {
                query.GlucoseLevel = measurement.GlucoseLevel;
                query.MeasurementDate = measurement.MeasurementDate;
                query.PatientId = measurement.PatientId;
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
                from item in connection.Measurement
                where item.Id == id.Value
                select item
            ).FirstOrDefault();

            if (query != null)
            {
                connection.Measurement.Remove(query);
                result = connection.SaveChanges() > 0;
            }
        }

        return Task.FromResult(result);
    }
}
