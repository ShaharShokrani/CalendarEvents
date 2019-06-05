using CalendarEvents.Controllers;
using CalendarEvents.Models;
using CalendarEvents.Services;
using Microsoft.AspNetCore.Mvc;
using Autofac.Extras.Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;
using CalendarEvents.Tests;
using CalendarEvents.DataAccess;

namespace CalendarEvents.Services.Tests
{
    public class EventsServiceTests
    {
        private AutoMock _mock = null;

        [SetUp]
        public void Setup()
        {
            _mock = AutoMock.GetLoose();
        }

        #region GetAllItems
        [Test] public void GetAllItems_WhenCalled_ShouldReturnList()
        {
            //Arrange
            IEnumerable<EventModel> expectedList = TestsFacade.EventsFacade.BuildEventModelList();            

            _mock.Mock<IRepository<EventModel>>()
                .Setup(items => items.GetAllItems())
                .Returns(() => expectedList);

            GenericService<EventModel> service = _mock.Create<GenericService<EventModel>>();

            //Act
            ResultService<IEnumerable<EventModel>> result = service.GetAllItems();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ResultService<IEnumerable<EventModel>>>(result);
            Assert.IsTrue(result.Success);
            Assert.IsInstanceOf<IEnumerable<EventModel>>(result.Value);

            List<EventModel> resultList = result.Value as List<EventModel>;

            Assert.IsNotNull(resultList);
            Assert.AreEqual(resultList.Count, (expectedList as List<EventModel>).Count);
            Assert.AreEqual(resultList[0].GetHashCode(), (expectedList as List<EventModel>)[0].GetHashCode());
        }

        //[Test]
        //public void Get_WhenServiceHasError_ShouldReturnStatusCode500()
        //{
        //    //Arrange
        //    ResultService<IEnumerable<EventModel>> expectedResultService = ResultService.Fail<IEnumerable<EventModel>>(ErrorCode.Unknown);

        //    _mock.Mock<IEventsService>()
        //        .Setup(items => items.GetAllItems())
        //        .Returns(() => expectedResultService);

        //    var controller = _mock.Create<EventsController>();

        //    //Act
        //    ActionResult<IEnumerable<EventModel>> result = controller.Get();

        //    //Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOf<ObjectResult>(result.Result);

        //    ObjectResult objectResult = result.Result as ObjectResult;
        //    AssertStatusCode500(objectResult, expectedResultService.ErrorCode);
        //}
        #endregion

        [TearDown]
        public void CleanUp()
        {
            if (_mock != null)
                _mock.Dispose();
        }
    }

}