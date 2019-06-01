using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalendarEvents.Models;
using System;

namespace CalendarEventsMSTests
{
    [TestClass]
    public class EventModelTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            EventModel @event = new EventModel();
            @event.Id = Guid.NewGuid();

        }
    }
}
