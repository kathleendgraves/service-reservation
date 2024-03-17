namespace service_reservation.Models
{
    public class Schedule
    {
        public Guid Id { get; set; }

        public string ProviderName { get; set; } = string.Empty;

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public IEnumerable<Appointment> Appointments { get; set; } = Enumerable.Empty<Appointment>();
    }
}
