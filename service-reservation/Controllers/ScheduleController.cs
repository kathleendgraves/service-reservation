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
    public class ScheduleController : Controller
    {
        private readonly IScheduleManager _scheduleManager;

        public ScheduleController(IScheduleManager scheduleManager)
        {
            _scheduleManager = scheduleManager;
        }

        [HttpPost]
        public Schedule AddSchedule([FromBody] RequestSchedule requestSchedule)
        {
            return _scheduleManager.AddSchedule(requestSchedule);
        }
    }
}
