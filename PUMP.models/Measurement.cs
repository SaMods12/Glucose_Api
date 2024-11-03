namespace PUMP.models
{
    public class Measurement
    {
        public int Id { get; set; }
        public DateTime MeasurementDate { get; set; }
        public decimal GlucoseLevel { get; set; } 
        public int PatientId { get; set; } // Relación con el paciente
        public virtual Patient Patient { get; set; } // Navegación hacia Patient
       

    }
}
