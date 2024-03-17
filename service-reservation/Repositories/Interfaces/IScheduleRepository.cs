using service_reservation.Models;
using service_reservation.Models.HttpRequest;

namespace service_reservation.Repositories.Interfaces
{
    public interface IScheduleRepository
    {
        public Schedule AddSchedule(RequestSchedule schedule);
    }
}
