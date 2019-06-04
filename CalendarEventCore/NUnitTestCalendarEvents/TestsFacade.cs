using CalendarEvents.Models;
using System;
using System.Collections.Generic;

namespace NUnitTestCalendarEvents
{
    public class TestsFacade
    {
        public static class EventsFacade
        {
            public static List<EventModel> BuildEventModelList()
            {
                return new List<EventModel>()
                {
                    BuildEventModel()
                };
            }
            public static EventModel BuildEventModel()
            {
                return new EventModel()
                {
                    End = DateTime.UtcNow.AddHours(1),
                    Id = Guid.NewGuid(),
                    IsAllDay = false,
                    Start = DateTime.UtcNow.AddMinutes(1),
                    Title = Guid.NewGuid().ToString(),
                    URL = Guid.NewGuid().ToString()
                };
            }
        }
    }
}
