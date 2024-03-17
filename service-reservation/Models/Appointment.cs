namespace service_reservation.Models
{
    public class Appointment
    {
        public Guid Id { get; set; }

        public string ProviderName { get; set; }

        public string? ClientName { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public bool Reserved { get; set; }

        public DateTime? ReservationCreatedAt { get; set; }

        public bool Confirmed { get; set; }
    }
}
