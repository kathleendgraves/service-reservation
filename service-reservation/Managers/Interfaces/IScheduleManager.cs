using service_reservation.Models;
using service_reservation.Models.HttpRequest;

namespace service_reservation.Managers.Interfaces
{
    public interface IScheduleManager
    {
        public Schedule AddSchedule(RequestSchedule schedule);
    }
}
