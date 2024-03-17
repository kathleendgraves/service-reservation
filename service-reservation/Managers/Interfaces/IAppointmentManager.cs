using service_reservation.Models;
using service_reservation.Models.HttpRequest;

namespace service_reservation.Managers.Interfaces
{
    public interface IAppointmentManager
    {
        public Appointment? ReserveAppointment(RequestReserveAppointment reserveAppointmentRequest);

        public Appointment? ConfirmAppointment(Guid appointmentId);

        public IEnumerable<Appointment> GetAvailableAppointments();

        public IEnumerable<Appointment> GetAppointments();
    }
}
