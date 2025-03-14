using Google.Apis.Calendar.v3.Data;
using PayrollTask.Models.Domain;

namespace PayrollTask.Models.Calendar;

public interface IGoogleCalendarHelper
{
    Task<Event> ScheduleReviewCall(Employee employee, Employee admin, DateTime dateTime);
}
