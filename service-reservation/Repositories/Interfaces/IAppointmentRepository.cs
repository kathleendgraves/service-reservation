using service_reservation.Models;
using service_reservation.Models.HttpRequest;

namespace service_reservation.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {
        public IEnumerable<Appointment> CreateAppointmentsForSchedule(string providerName, DateTime startDate, DateTime endDate);

        public Appointment? ReserveAppointment(RequestReserveAppointment reserveAppointmentRequest);

        public Appointment? ConfirmAppointment(Guid appointmentId);

        public IEnumerable<Appointment> GetAvailableAppointments();

        public IEnumerable<Appointment> GetAppointments();
    }
}
