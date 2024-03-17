using service_reservation.Models;
using service_reservation.Models.HttpRequest;
using service_reservation.Repositories;
using service_reservation.Repositories.Interfaces;

namespace service_reservation_tests.Functional.Repositories
{
    public class AppointmentRepositoryFunctionalTests
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentRepositoryFunctionalTests()
        {
            _appointmentRepository = new AppointmentRepository();
        }

        [Fact]
        public void CreateAppointmentsForSchedule_CreatesAppointments()
        {
            // Arrange
            string providerName = "Dr Jekyll";
            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime.AddMinutes(45);

            // Act
            List<Appointment> appointments = (List<Appointment>)_appointmentRepository.CreateAppointmentsForSchedule(providerName, startTime, endTime);

            // Assert
            Assert.Equal(3, appointments.Count());

            Appointment firstAppointment = appointments[0];
            Assert.Equal(providerName, firstAppointment.ProviderName);
            Assert.Null(firstAppointment.ClientName);
            Assert.Equal(startTime, firstAppointment.StartTime);
            Assert.Equal(firstAppointment.StartTime.AddMinutes(15), firstAppointment.EndTime);
            Assert.False(firstAppointment.Reserved);
            Assert.False(firstAppointment.ReservationCreatedAt.HasValue);
            Assert.False(firstAppointment.Confirmed);

            Appointment secondAppointment = appointments[1];
            Assert.Equal(providerName, secondAppointment.ProviderName);
            Assert.Null(secondAppointment.ClientName);
            Assert.Equal(firstAppointment.EndTime, secondAppointment.StartTime);
            Assert.Equal(secondAppointment.StartTime.AddMinutes(15), secondAppointment.EndTime);
            Assert.False(secondAppointment.Reserved);
            Assert.False(secondAppointment.ReservationCreatedAt.HasValue);
            Assert.False(secondAppointment.Confirmed);

            Appointment thirdAppointment = appointments[2];
            Assert.Equal(providerName, thirdAppointment.ProviderName);
            Assert.Null(thirdAppointment.ClientName);
            Assert.Equal(secondAppointment.EndTime, thirdAppointment.StartTime);
            Assert.Equal(thirdAppointment.StartTime.AddMinutes(15), thirdAppointment.EndTime);
            Assert.False(thirdAppointment.Reserved);
            Assert.False(thirdAppointment.ReservationCreatedAt.HasValue);
            Assert.False(thirdAppointment.Confirmed);
        }

        [Fact]
        public void ReserveAppointment_ReservesAppointment()
        {
            // Arrange
            string providerName = "Dr Jekyll";
            DateTime startTime = DateTime.Now.AddDays(2);
            DateTime endTime = startTime.AddMinutes(45);
            List<Appointment> appointments = (List<Appointment>)_appointmentRepository.CreateAppointmentsForSchedule(providerName, startTime, endTime);

            RequestReserveAppointment reserveAppointmentRequest = new RequestReserveAppointment
            {
                AppointmentId = appointments.First().Id,
                ClientName = "Mr. Hyde"
            };

            // Act
            Appointment? reservedAppointment = _appointmentRepository.ReserveAppointment(reserveAppointmentRequest);

            // Assert
            Assert.NotNull(reservedAppointment);
            Assert.Equal(appointments.First().Id, reservedAppointment.Id);
            Assert.Equal(reserveAppointmentRequest.ClientName, reservedAppointment.ClientName);
            Assert.True(reservedAppointment.Reserved);
            Assert.NotNull(reservedAppointment.ReservationCreatedAt);
            Assert.Equal(DateTime.Now.Date, reservedAppointment.ReservationCreatedAt.Value.Date);
        }


        [Fact]
        public void ReserveAppointment_ReserveAppointment_Under24Hours_ThrowException()
        {
            // Arrange
            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime.AddMinutes(45);
            List<Appointment> appointments = (List<Appointment>)_appointmentRepository.CreateAppointmentsForSchedule("Dr Jekyll", startTime, endTime);

            RequestReserveAppointment reserveAppointmentRequest = new RequestReserveAppointment
            {
                AppointmentId = appointments.First().Id,
                ClientName = "Mr. Hyde"
            };

            // Act
            ArgumentException exception = Assert.Throws<ArgumentException>(() => _appointmentRepository.ReserveAppointment(reserveAppointmentRequest));

            // Assert
            Assert.Equal("Reservations must be made at least 24 hours in advance.", exception.Message);
        }

        [Fact]
        public void ConfirmAppointment_ConfirmsAppointment()
        {
            // Arrange
            DateTime startTime = DateTime.Now.AddDays(2);
            DateTime endTime = startTime.AddMinutes(45);
            List<Appointment> appointments = (List<Appointment>)_appointmentRepository.CreateAppointmentsForSchedule("Dr Jekyll", startTime, endTime);

            RequestReserveAppointment reserveAppointmentRequest = new RequestReserveAppointment
            {
                AppointmentId = appointments.First().Id,
                ClientName = "Mr. Hyde"
            };
            Appointment? reservedAppointment = _appointmentRepository.ReserveAppointment(reserveAppointmentRequest);
            Assert.NotNull(reservedAppointment);

            // Act
            Appointment? confirmedAppointment = _appointmentRepository.ConfirmAppointment(reservedAppointment.Id);

            // Assert
            Assert.NotNull(confirmedAppointment);
            Assert.Equal(appointments.First().Id, confirmedAppointment.Id);
            Assert.Equal(reserveAppointmentRequest.ClientName, confirmedAppointment.ClientName);
            Assert.True(confirmedAppointment.Reserved);
            Assert.NotNull(confirmedAppointment.ReservationCreatedAt);
            Assert.Equal(DateTime.Now.Date, confirmedAppointment.ReservationCreatedAt.Value.Date);
            Assert.True(confirmedAppointment.Confirmed);
        }

        [Fact]
        public void GetAvailableAppointments_GetsAvailableAppointments()
        {
            // Arrange
            string providerName = "Dr Jekyll";
            DateTime startTime = DateTime.Now.AddDays(2);
            DateTime endTime = startTime.AddMinutes(45);
            List<Appointment> appointments = (List<Appointment>)_appointmentRepository.CreateAppointmentsForSchedule(providerName, startTime, endTime);

            RequestReserveAppointment reserveAppointmentRequest = new RequestReserveAppointment
            {
                AppointmentId = appointments.First().Id,
                ClientName = "Mr. Hyde"
            };
            Appointment? reservedAppointment = _appointmentRepository.ReserveAppointment(reserveAppointmentRequest);
            Assert.NotNull(reservedAppointment);

            Appointment? confirmedAppointment = _appointmentRepository.ConfirmAppointment(reservedAppointment.Id);
            Assert.NotNull(confirmedAppointment);

            // Act
            List<Appointment> returnedAppointments = _appointmentRepository.GetAvailableAppointments().ToList();

            // Assert
            Assert.Equal(2, returnedAppointments.Count());
            Assert.DoesNotContain(returnedAppointments, a => a.Id == confirmedAppointment.Id);
        }
    }
}
