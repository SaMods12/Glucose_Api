namespace PUMP.models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; } // Fecha y hora de la cita
        public int PatientId { get; set; } // Relación con el paciente
        public int DoctorId { get; set; } // Relación con el doctor
        public virtual Patient Patient { get; set; } // Navegación hacia Patient
        public virtual Doctor Doctor { get; set; } // Navegación hacia Doctor
    }
}
