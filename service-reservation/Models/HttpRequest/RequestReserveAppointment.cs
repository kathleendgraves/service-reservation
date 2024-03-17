using System.Text.Json.Serialization;

namespace service_reservation.Models.HttpRequest
{
    public class RequestReserveAppointment
    {
        [JsonIgnore]
        public Guid AppointmentId { get; set; }

        public string? ClientName { get; set; }
    }
}
