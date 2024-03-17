using service_reservation.Models;
using service_reservation.Models.HttpRequest;
using service_reservation.Repositories;
using service_reservation.Repositories.Interfaces;

namespace service_reservation_tests.Functional.Repositories
{
    public class ScheduleRepositoryFunctionalTests
    {
        private readonly IScheduleRepository _scheduleRepository;

        public ScheduleRepositoryFunctionalTests()
        {
            _scheduleRepository = new ScheduleRepository();
        }

        [Fact]
        public void AddSchedule_AddsSchedule()
        {
            // Arrange
            DateTime startTime = DateTime.Now;
            RequestSchedule requestSchedule = new RequestSchedule
            {
                ProviderName = "Dr Jekyll",
                StartTime = startTime,
                EndTime = startTime.AddMinutes(15)
            };

            // Act
            Schedule addedSchedule = _scheduleRepository.AddSchedule(requestSchedule);

            // Assert
            Assert.NotEqual(Guid.Empty, addedSchedule.Id);
            Assert.Equal(requestSchedule.ProviderName, addedSchedule.ProviderName);
            Assert.Equal(requestSchedule.StartTime, addedSchedule.StartTime);
            Assert.Equal(requestSchedule.EndTime, addedSchedule.EndTime);
        }
    }
}
