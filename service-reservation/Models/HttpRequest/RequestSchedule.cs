namespace service_reservation.Models.HttpRequest
{
    public class RequestSchedule
    {
        public string ProviderName { get; set; } = string.Empty;

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
