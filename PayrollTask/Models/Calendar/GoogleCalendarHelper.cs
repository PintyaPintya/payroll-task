using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Util.Store;
using PayrollTask.Models.Domain;

namespace PayrollTask.Models.Calendar;

public class GoogleCalendarHelper
{
    static string[] Scopes = { CalendarService.Scope.Calendar };
    static string ApplicationName = "payroll-task";

    public async Task<Event> ScheduleReviewCall(Employee employee, Employee admin, DateTime dateTime)
    {
        UserCredential userCredential;
        using(var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
        {
            string credPath = "token.json";
            userCredential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                new FileDataStore(credPath, true));
        }

        var newEvent = new Event()
        {
            Summary = "Payroll task review call",
            Location = "Virtual Meeting",
            Description = $"Payroll task review call of {employee.Name} with {admin.Name}",
            Start = new EventDateTime()
            {
                DateTime = DateTime.Parse(dateTime),
                TimeZone = "India Standard Time"
            },
            End = new EventDateTime()
            {
                DateTime = DateTime.Parse(dateTime),
                TimeZone = "India Standard Time",
            },
            Attendees = new List<EventAttendee>()
            {
                new EventAttendee {Email = admin.Email},
                new EventAttendee {Email = employee.Email}
            },
            Reminders = new Event.RemindersData()
            {
                UseDefault = false,
                Overrides = new List<EventReminder>()
                {
                    new EventReminder() { Method = "email", Minutes = 24 * 60 }
                }
            }
        };

        var insertRequest = service.Events.Insert(newEvent, "primary");
        Event createdEvent = await insertRequest.ExecuteAsync();

        return createdEvent;
    }
}
