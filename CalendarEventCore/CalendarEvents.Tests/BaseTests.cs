using CalendarEvents.Controllers;
using CalendarEvents.Models;
using CalendarEvents.Services;
using Microsoft.AspNetCore.Mvc;
using Autofac.Extras.Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;

namespace CalendarEvents.Tests
{
    public class BaseTest
    {
        public void AssertResultServiceException<T>(ResultService<T> resultService, Exception expectedException)
        {
            Assert.IsNotNull(resultService);
            Assert.IsFalse(resultService.Success);
            Assert.IsNull(resultService.Value);
            Assert.IsTrue(resultService.Exception == expectedException);
        }
    }

}