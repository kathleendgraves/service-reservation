using Microsoft.AspNetCore.Mvc;
using service_reservation.Managers.Interfaces;
using service_reservation.Models;
using service_reservation.Models.HttpRequest;

namespace service_reservation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentManager _appointmentManager;

        public AppointmentController(IAppointmentManager appointmentManager)
        {
            _appointmentManager = appointmentManager;
        }

        [HttpPost("{appointmentId}/reserve")]
        public Appointment? ReserveAppontment([FromRoute] Guid appointmentId, [FromBody] RequestReserveAppointment reserveAppointmentRequest)
        {
            reserveAppointmentRequest.AppointmentId = appointmentId;
            return _appointmentManager.ReserveAppointment(reserveAppointmentRequest);
        }

        [HttpPost("{appointmentId}/confirm")]
        public Appointment? ConfirmAppointment([FromRoute] Guid appointmentId)
        {
            return _appointmentManager.ConfirmAppointment(appointmentId);
        }

        [HttpGet("available")]
        public IEnumerable<Appointment> GetAvailableAppointments()
        {
            return _appointmentManager.GetAvailableAppointments();
        }

        [HttpGet]
        public IEnumerable<Appointment> GetAppointments()
        {
            return _appointmentManager.GetAppointments();
        }
    }
}
