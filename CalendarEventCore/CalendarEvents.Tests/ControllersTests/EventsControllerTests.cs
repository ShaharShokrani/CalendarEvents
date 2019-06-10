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
    public class EventsControllerTests
    {
        private AutoMock _mock = null;

        [SetUp] public void Setup()
        {
            _mock = AutoMock.GetLoose();
        }

        //TODO: add filter and orderBy and include.
        #region Get
        [Test] public void Get_WhenCalled_ShouldReturnOk()
        {
            //Arrange
            IEnumerable<EventModel> expectedList = TestsFacade.EventsFacade.BuildEventModelList(); ;

            ResultService<IEnumerable<EventModel>> expected = ResultService.Ok(expectedList); ;

            _mock.Mock<IGenericService<EventModel>>()
                .Setup(items => items.Get(null, null, ""))
                .Returns(() => expected);

            var controller = _mock.Create<EventsController>();

            //Act
            ActionResult<IEnumerable<EventModel>> result = controller.Get();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult.Value);

            List<EventModel> okResultList = okResult.Value as List<EventModel>;

            Assert.IsNotNull(okResultList);
            Assert.AreEqual(okResultList.Count, (expectedList as List<EventModel>).Count);
            Assert.AreEqual(okResultList[0].GetHashCode(), (expectedList as List<EventModel>)[0].GetHashCode());
        }
        [Test] public void Get_WhenServiceHasError_ShouldReturnStatusCode500()
        {
            //Arrange
            ResultService<IEnumerable<EventModel>> expectedResultService = ResultService.Fail<IEnumerable<EventModel>>(ErrorCode.Unknown);

            _mock.Mock<IGenericService<EventModel>>()
                .Setup(items => items.Get(null, null, ""))
                .Returns(() => expectedResultService);

            var controller = _mock.Create<EventsController>();

            //Act
            ActionResult<IEnumerable<EventModel>> result = controller.Get();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result.Result);

            ObjectResult objectResult = result.Result as ObjectResult;
            AssertStatusCode500(objectResult, expectedResultService.ErrorCode);
        }
        #endregion

        #region GetById
        [Test] public void Put_RequestNotValid_ShouldReturnBadRequest()
        {
            //Arrange
            EventModel expectedItem = TestsFacade.EventsFacade.BuildEventModelItem();

            var controller = _mock.Create<EventsController>();

            //Act
            ActionResult<EventModel> actionResult = controller.Get(new Guid());

            //Assert
            AssertBadRequestResult(actionResult);
        }
        [Test] public void GetById_WhenCalled_ShouldReturnOk()
        {
            //Arrange
            EventModel expectedItem = TestsFacade.EventsFacade.BuildEventModelItem();

            ResultService<EventModel> expectedResultService = ResultService.Ok(expectedItem);

            _mock.Mock<IGenericService<EventModel>>()
                .Setup(items => items.GetById(expectedItem.Id))
                .Returns(() => expectedResultService);

            var controller = _mock.Create<EventsController>();

            //Act
            ActionResult<EventModel> actionResult = controller.Get(expectedItem.Id);

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);

            var okResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okResult.Value);

            EventModel okResultItem = okResult.Value as EventModel;

            Assert.IsNotNull(okResultItem);
            Assert.AreEqual(okResultItem.GetHashCode(), expectedItem.GetHashCode());
        }
        [Test] public void GetById_WhenServiceHasError_ShouldReturnStatusCode500()
        {
            //Arrange
            ResultService<EventModel> expectedResultService = ResultService.Fail<EventModel>(ErrorCode.Unknown);
            Guid id = Guid.NewGuid();

            _mock.Mock<IGenericService<EventModel>>()
                .Setup(items => items.GetById(id))
                .Returns(() => expectedResultService);

            var controller = _mock.Create<EventsController>();

            //Act
            ActionResult<EventModel> actionResult = controller.Get(id);

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<ObjectResult>(actionResult.Result);

            var objectResult = actionResult.Result as ObjectResult;
            AssertStatusCode500(objectResult, expectedResultService.ErrorCode);
        }
        #endregion

        #region Post
        [Test] public void Post_RequestNotValid_ShouldReturnBadRequest()
        {
            //Arrange
            EventModel expectedItem = TestsFacade.EventsFacade.BuildEventModelItem();
            expectedItem.Name = null;

            var controller = _mock.Create<EventsController>();

            //Act
            controller.ModelState.AddModelError("Title", "Required");
            ActionResult<EventModel> actionResult = controller.Post(expectedItem);

            //Assert
            AssertBadRequestResult(actionResult);
        }
        [Test] public void Post_WhenCalled_ShouldReturnPost()
        {
            //Arrange
            EventModel expectedItem = TestsFacade.EventsFacade.BuildEventModelItem();

            ResultService<EventModel> expectedResultService = ResultService.Ok(expectedItem);

            _mock.Mock<IGenericService<EventModel>>()
                .Setup(items => items.Insert(expectedItem))
                .Returns(() => expectedResultService);

            var controller = _mock.Create<EventsController>();

            //Act
            ActionResult<EventModel> actionResult = controller.Post(expectedItem);

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<CreatedAtActionResult>(actionResult.Result);

            var createdAtActionResult = actionResult.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult.Value);

            EventModel createdAtActionResultItem = createdAtActionResult.Value as EventModel;
            RouteValueDictionary createdAtActionResultRouteValues = createdAtActionResult.RouteValues;

            Assert.IsNotNull(createdAtActionResultItem);
            Assert.AreEqual(createdAtActionResult.ActionName, "Post");
            Assert.AreEqual(createdAtActionResultRouteValues["Id"], createdAtActionResultItem.Id);
            Assert.AreEqual(createdAtActionResultItem.GetHashCode(), expectedItem.GetHashCode());
        }
        [Test] public void Post_WhenServiceHasError_ShouldReturnStatusCode500()
        {
            //Arrange
            ResultService<EventModel> expectedResultService = ResultService.Fail<EventModel>(ErrorCode.Unknown);
            EventModel eventModel = TestsFacade.EventsFacade.BuildEventModelItem();

            _mock.Mock<IGenericService<EventModel>>()
                .Setup(items => items.Insert(eventModel))
                .Returns(() => expectedResultService);

            var controller = _mock.Create<EventsController>();

            //Act
            ActionResult<EventModel> actionResult = controller.Post(eventModel);

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<ObjectResult>(actionResult.Result);

            var objectResult = actionResult.Result as ObjectResult;
            AssertStatusCode500(objectResult, expectedResultService.ErrorCode);
        }
        #endregion

        #region Put
        [Test] public void Put_RequestStateNotValid_ShouldReturnBadRequest()
        {
            //Arrange
            EventModel expectedItem = TestsFacade.EventsFacade.BuildEventModelItem();
            expectedItem.Name = null;

            var controller = _mock.Create<EventsController>();

            //Act
            controller.ModelState.AddModelError("Title", "Required");
            ActionResult<EventModel> actionResult = controller.Put(expectedItem);

            //Assert
            AssertBadRequestResult(actionResult);
        }
        [Test] public void Put_RequestIdNotValid_ShouldReturnBadRequest()
        {
            //Arrange
            EventModel expectedItem = TestsFacade.EventsFacade.BuildEventModelItem();
            expectedItem.Id = new Guid();

            var controller = _mock.Create<EventsController>();

            //Act
            ActionResult<EventModel> actionResult = controller.Put(expectedItem);

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<BadRequestResult>(actionResult.Result);
        }
        [Test] public void Put_WhenCalled_ShouldReturnPut()
        {
            //Arrange
            EventModel expectedItem = TestsFacade.EventsFacade.BuildEventModelItem();

            ResultService<EventModel> expectedResultService = ResultService.Ok(expectedItem);

            _mock.Mock<IGenericService<EventModel>>()
                .Setup(items => items.Update(expectedItem))
                .Returns(() => expectedResultService);

            var controller = _mock.Create<EventsController>();

            //Act
            ActionResult actionResult = controller.Put(expectedItem);

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<OkResult>(actionResult);
        }
        [Test] public void Put_WhenServiceHasError_ShouldReturnStatusCode500()
        {
            //Arrange
            EventModel eventModel = TestsFacade.EventsFacade.BuildEventModelItem();
            ResultService<EventModel> expectedResultService = ResultService.Fail<EventModel>(ErrorCode.Unknown);            

            _mock.Mock<IGenericService<EventModel>>()
                .Setup(items => items.Update(eventModel))
                .Returns(() => expectedResultService);

            var controller = _mock.Create<EventsController>();

            //Act
            ActionResult<EventModel> actionResult = controller.Put(eventModel);

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<ObjectResult>(actionResult.Result);

            var objectResult = actionResult.Result as ObjectResult;
            AssertStatusCode500(objectResult, expectedResultService.ErrorCode);
        }
        #endregion

        #region Delete
        [Test] public void Delete_RequestNotValid_ShouldReturnBadRequest()
        {
            //Arrange
            EventModel expectedItem = TestsFacade.EventsFacade.BuildEventModelItem();

            var controller = _mock.Create<EventsController>();

            //Act
            ActionResult<EventModel> actionResult = controller.Delete(new Guid());

            //Assert
            AssertBadRequestResult(actionResult);
        }
        [Test] public void Delete_WhenCalled_ShouldReturnOk()
        {
            //Arrange
            ResultService expectedResultService = ResultService.Ok();
            Guid id = Guid.NewGuid();

            _mock.Mock<IGenericService<EventModel>>()
                .Setup(items => items.Delete(id))
                .Returns(() => expectedResultService);

            var controller = _mock.Create<EventsController>();

            //Act
            ActionResult actionResult = controller.Delete(id);

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<OkResult>(actionResult);
        }
        [Test] public void Delete_WhenServiceHasError_ShouldReturnStatusCode500()
        {
            //Arrange
            ResultService expectedResultService = ResultService.Fail(ErrorCode.Unknown);
            Guid id = Guid.NewGuid();

            _mock.Mock<IGenericService<EventModel>>()
                .Setup(items => items.Delete(id))
                .Returns(() => expectedResultService);

            var controller = _mock.Create<EventsController>();

            //Act
            ActionResult actionResult = controller.Delete(id);

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<ObjectResult>(actionResult);

            var objectResult = actionResult as ObjectResult;
            AssertStatusCode500(objectResult, expectedResultService.ErrorCode);
        }
        #endregion

        [TearDown] public void CleanUp()
        {
            if (_mock != null)
                _mock.Dispose();
        }

        private void AssertBadRequestResult(ActionResult<EventModel> actionResult)
        {
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<BadRequestResult>(actionResult.Result);
        }
        private void AssertStatusCode500(ObjectResult objectResult, ErrorCode errorCode)
        {
            Assert.IsNotNull(objectResult.Value);
            Assert.IsNotNull(objectResult.StatusCode);
            Assert.AreEqual(objectResult.StatusCode, 500);
            Assert.AreEqual((ErrorCode)objectResult.Value, errorCode);
        }

    }

}