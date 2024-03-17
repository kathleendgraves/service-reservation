using service_reservation.Models;
using service_reservation.Models.HttpRequest;
using service_reservation.Repositories.Interfaces;

namespace service_reservation.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly List<Appointment> _appointments;

        public AppointmentRepository()
        {
            _appointments = new List<Appointment>();
        }

        public IEnumerable<Appointment> CreateAppointmentsForSchedule(string providerName, DateTime startDate, DateTime endDate)
        {
            List<Appointment> newAppointments = new List<Appointment>();

            DateTime appointmentStartTime = startDate;
            while (appointmentStartTime < endDate)
            {
                Appointment appointment = new Appointment
                {
                    Id = Guid.NewGuid(),
                    ProviderName = providerName,
                    StartTime = appointmentStartTime,
                    EndTime = appointmentStartTime.AddMinutes(15),
                    Reserved = false,
                    Confirmed = false,
                };
                _appointments.Add(appointment);
                newAppointments.Add(appointment);

                appointmentStartTime = appointment.StartTime.AddMinutes(15);
            }

            return newAppointments;
        }

        public Appointment? ReserveAppointment(RequestReserveAppointment reserveAppointmentRequest)
        {
            Appointment? appointment = _appointments.Where(a => a.Id == reserveAppointmentRequest.AppointmentId).FirstOrDefault();
            if(appointment is not null)
            {
                if(appointment.StartTime - DateTime.Now < TimeSpan.FromHours(24))
                {
                    throw new ArgumentException("Reservations must be made at least 24 hours in advance.");
                }

                appointment.ClientName = reserveAppointmentRequest.ClientName;
                appointment.Reserved = true;
                appointment.ReservationCreatedAt = DateTime.Now;
            }

            return appointment;
        }

        public Appointment? ConfirmAppointment(Guid appointmentId)
        {
            Appointment? appointment = _appointments.Where(a => a.Id == appointmentId).FirstOrDefault();
            if (appointment is not null)
            {
                appointment.Confirmed = true;
            }

            return appointment;
        }

        public IEnumerable<Appointment> GetAvailableAppointments()
        {
            freeExpiredAppointments();

            return _appointments.Where(a => !a.Confirmed || !a.Reserved);
        }

        public IEnumerable<Appointment> GetAppointments()
        {
            return _appointments;
        }

        private void freeExpiredAppointments()
        {
            _appointments.ForEach(a =>
            {
                if(a.Reserved && a.ReservationCreatedAt.HasValue && (DateTime.Now - a.ReservationCreatedAt) > TimeSpan.FromMinutes(30))
                {
                    a.Reserved = false;
                    a.ReservationCreatedAt = null;
                }
            });
        }
    }
}
