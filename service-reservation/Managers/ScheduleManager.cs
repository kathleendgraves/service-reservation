using service_reservation.Managers.Interfaces;
using service_reservation.Models;
using service_reservation.Models.HttpRequest;
using service_reservation.Repositories.Interfaces;

namespace service_reservation.Managers
{
    public class ScheduleManager : IScheduleManager
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IAppointmentRepository _appointmentRepository;

        public ScheduleManager(IScheduleRepository scheduleRepository, IAppointmentRepository appointmentRepository)
        {
            _scheduleRepository = scheduleRepository;
            _appointmentRepository = appointmentRepository;
        }

        public Schedule AddSchedule(RequestSchedule requestSchedule)
        {
            Schedule schedule = _scheduleRepository.AddSchedule(requestSchedule);
            schedule.Appointments = _appointmentRepository.CreateAppointmentsForSchedule(schedule.ProviderName, schedule.StartTime, schedule.EndTime);

            return schedule;
        }
    }
}
