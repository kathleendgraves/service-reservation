# serice-reservation
service-reservation is the API backend for a reservation service. It allows providers to create a work schedule and 
clients to see available appointments, reserve an appointment, and confirm the appointment.
## How to set up the service
* Install [.NET Core 7](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
* Clone the code
## How to run the service
### Command line
From the `service-reservation` folder, run the following command to start the project
```
dotnet run service-reservation
```
The swagger page can be accessed from `http://localhost:5113/swagger/index.html`.

Ctrl + C will stop the process.
### Visual Studio
Press the green play button near the top of the screen. This will launch a browswer open to the swagger page.
Press the red square button to stop.
## How to run the automated tests
### Command Line
From the `service-reservation-tests` folder, run the following command to start the project
```
dotnet test
```
### Visual Studio
From the "Test" menu at the top of the screen, select "Run all tests." This will open the Test Explorer window and 
display the results of the test.

## Assumptions
- A provider submits one schedule for the entire day
- The start and end times of the schedule are in 15 minute increments

## Production Considerations
### Add automated tests
Testing was mostly done manually, though I was able to add a few automated tests. To prepare the code for production, 
I would add automated tests that cover > 80% of the code. The tests would include unit tests, functional tests, and 
end to end tests. Ideally the tests would run as a required PR check.
### Add validation
The code assumes that all passed in values are valid for the sake of time (eg: the start date is before the end date).
To prepare the code for production I would add validation to all incoming values. For example, 
[FluentValidation](https://docs.fluentvalidation.net/en/latest/)
### Add error handling
The code assumes that none of the passed in information will cause an issue. To prepare the code for production, I would
add error handling for duplicate schedules and appointments, overlapping schedules, prevent clients from confirming
another client's appointment, etc.
### Add auth
The APIs are unauthorized. To prepare the code for production, I would add authentication and authorization to the project.
Only authorized users would be able to call the APIs and clients would not be able to access the provider APIs and vice versa.
### Add a database
The code stores the appointment information in memory and declares the Appointment and Schedule repository as
singletons so the information persists. In production, I would replace the current repository implementation
with one that calls a SQL database and register the repositories as scoped.
### Add user awareness
The code stores provider and client information as names. To prepare the code for production, I would determine if there is
another service that handles provider/client creation. If there is, I would update the system to expect
unique ids instead of names and call out to those services to confirm the user before storing data. If there is not,
I would consider implementing a user service outside of this one to handle users (either build or buy).
### Add logging
As written, there is no logging. To prepare the code for production I would add light logging through the solution
and determine the need for more/different logging through monitoring in production.
### Filtering for available appointments
The code currently returns all available appointments. To prepare the code for production, I would update the endpoint
to filter by date and provider. I would also talk with stakeholders to determine if any other filtering is necessary.
### Add swagger documentation
The endpoints don't have descriptions in swagger. To prepare the code for production, I would add XML comments for all
the API endpoints and configure the project to use those to populate the swagger document.
### Review API routes
The API routes are functional but would benefit from a review to ensure they are appropriately RESTful.
### Full PR review
This code has not been peer reviewed and should be, thoroughly, before deploying to production.
