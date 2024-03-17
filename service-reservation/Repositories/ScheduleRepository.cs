using service_reservation.Models;
using service_reservation.Models.HttpRequest;
using service_reservation.Repositories.Interfaces;

namespace service_reservation.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly List<Schedule> _schedules;

        public ScheduleRepository() 
        {
            _schedules = new List<Schedule>();
        }

        public Schedule AddSchedule(RequestSchedule requestSchedule)
        {
            Schedule schedule = convertRequestToSchedule(requestSchedule);
            schedule.Id = Guid.NewGuid();
            _schedules.Add(schedule);

            return schedule;
        }

        private Schedule convertRequestToSchedule(RequestSchedule requestSchedule)
        {
            return new Schedule
            {
                ProviderName = requestSchedule.ProviderName,
                StartTime = requestSchedule.StartTime,
                EndTime = requestSchedule.EndTime,
            };
        }
    }
}
