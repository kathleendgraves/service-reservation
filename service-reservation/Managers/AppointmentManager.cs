using service_reservation.Managers.Interfaces;
using service_reservation.Models;
using service_reservation.Models.HttpRequest;
using service_reservation.Repositories.Interfaces;

namespace service_reservation.Managers
{
    public class AppointmentManager : IAppointmentManager
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentManager(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public Appointment? ReserveAppointment(RequestReserveAppointment reserveAppointmentRequest)
        {
            return _appointmentRepository.ReserveAppointment(reserveAppointmentRequest);
        }

        public Appointment? ConfirmAppointment(Guid appointmentId)
        {
            return _appointmentRepository.ConfirmAppointment(appointmentId);
        }

        public IEnumerable<Appointment> GetAvailableAppointments()
        {
            return _appointmentRepository.GetAvailableAppointments();
        }

        public IEnumerable<Appointment> GetAppointments()
        {
            return _appointmentRepository.GetAppointments();
        }
    }
}
