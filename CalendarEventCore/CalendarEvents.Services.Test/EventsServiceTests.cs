using CalendarEvents.Models;
using Autofac.Extras.Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using CalendarEvents.Tests;
using CalendarEvents.DataAccess;
using System.Linq;

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

        #region Insert
        [Test] public void Insert_WhenCalled_ShouldNewItem()
        {
            //Arrange
            EventModel expectedItem = TestsFacade.EventsFacade.BuildEventModelItem();

            var repositoryMock = _mock.Mock<IGenericRepository<EventModel>>();
            repositoryMock
                .Setup(items => items.Insert(expectedItem));

            GenericService<EventModel> service = _mock.Create<GenericService<EventModel>>();

            //Act
            ResultService result = service.Insert(expectedItem);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ResultService>(result);
            Assert.IsTrue(result.Success);
        }
        [Test] public void Add_WhenRepositoryThrowException_ShouldReturnError()
        {
            //Arrange
            EventModel expectedItem = TestsFacade.EventsFacade.BuildEventModelItem();
            Exception expectedException = new Exception(Guid.NewGuid().ToString());

            var repositoryMock = _mock.Mock<IGenericRepository<EventModel>>();
            repositoryMock
                .Setup(items => items.Insert(expectedItem))
                .Throws(expectedException);

            GenericService<EventModel> service = _mock.Create<GenericService<EventModel>>();

            //Act
            ResultService result = service.Insert(expectedItem);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Exception == expectedException);
        }
        #endregion  

        #region Get
        //TODO: add sort and filter.
        [Test] public void Get_WhenCalled_ShouldReturnList()
        {
            //Arrange
            IEnumerable<EventModel> expectedList = TestsFacade.EventsFacade.BuildEventModelList();

            var repositoryMock = _mock.Mock<IGenericRepository<EventModel>>();
            repositoryMock
                .Setup(items => items.Get(null, null, ""))
                .Returns(() => expectedList);

            GenericService<EventModel> service = _mock.Create<GenericService<EventModel>>();

            //Act
            ResultService<IEnumerable<EventModel>> result = service.Get(null, null, "");

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
        [Test] public void Get_WhenCalledWithFilter_ShouldReturnList()
        {
            //Arrange
            IEnumerable<EventModel> expectedList = TestsFacade.EventsFacade.BuildEventModelList();
            EventModel filteredItem = expectedList.ToList()[0];

            var repositoryMock = _mock.Mock<IGenericRepository<EventModel>>();
            repositoryMock
                .Setup(items => items.Get(a => a.Id == filteredItem.Id, null, ""))
                .Returns(() => expectedList);

            GenericService<EventModel> service = _mock.Create<GenericService<EventModel>>();

            //Act
            ResultService<IEnumerable<EventModel>> result = service.Get(a => a.Id == filteredItem.Id, null, "");

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ResultService<IEnumerable<EventModel>>>(result);
            Assert.IsTrue(result.Success);
            Assert.IsInstanceOf<IEnumerable<EventModel>>(result.Value);

            List<EventModel> resultList = result.Value as List<EventModel>;

            Assert.IsNotNull(resultList);
            Assert.IsTrue(resultList.Count() == 1);
            Assert.AreEqual(filteredItem, resultList.ToList()[0]);
        }
        [Test] public void Get_WhenCalledWithOrder_ShouldReturnList()
        {
            //Arrange
            IEnumerable<EventModel> expectedList = TestsFacade.EventsFacade.BuildEventModelList(10);

            var repositoryMock = _mock.Mock<IGenericRepository<EventModel>>();
            repositoryMock
                .Setup(items => items.Get(null, query => query.OrderBy(a => a.Name), ""))
                .Returns(() => expectedList);

            GenericService<EventModel> service = _mock.Create<GenericService<EventModel>>();

            //Act
            ResultService<IEnumerable<EventModel>> result = service.Get(null, query=>query.OrderBy(a=>a.Name), "");

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ResultService<IEnumerable<EventModel>>>(result);
            Assert.IsTrue(result.Success);
            Assert.IsInstanceOf<IEnumerable<EventModel>>(result.Value);

            List<EventModel> resultList = result.Value as List<EventModel>;

            Assert.IsNotNull(resultList);
            Assert.AreEqual(resultList.Count, (expectedList as List<EventModel>).Count);
            //TODO: Complete this:
            //Assert.AreEqual(resultList, (expectedListOrderBy(a => a.Name));
        }
        [Test] public void Get_WhenRepositoryReturnsNull_ShouldReturnNotFound()
        {
            //Arrange
            var repositoryMock = _mock.Mock<IGenericRepository<EventModel>>();
            repositoryMock
                .Setup(items => items.Get(null, null, ""))
                .Returns(() => null);

            GenericService<EventModel> service = _mock.Create<GenericService<EventModel>>();

            //Act
            ResultService<IEnumerable<EventModel>> result = service.Get();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Value);
            Assert.IsTrue(result.ErrorCode == ErrorCode.NotFound);
        }
        [Test] public void Get_WhenRepositoryThrowException_ShouldReturnError()
        {
            //Arrange
            Exception expectedException = new Exception(Guid.NewGuid().ToString());

            var repositoryMock = _mock.Mock<IGenericRepository<EventModel>>();
            repositoryMock
                .Setup(items => items.Get(null, null, ""))
                .Throws(expectedException);

            GenericService<EventModel> service = _mock.Create<GenericService<EventModel>>();

            //Act
            ResultService<IEnumerable<EventModel>> result = service.Get();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Value);
            Assert.IsTrue(result.Exception == expectedException);
        }
        #endregion

        #region GetById
        [Test] public void GetById_WhenCalled_ShouldReturnItem()
        {
            //Arrange
            EventModel expectedItem = TestsFacade.EventsFacade.BuildEventModelItem();

            var repositoryMock = _mock.Mock<IGenericRepository<EventModel>>();
            repositoryMock
                .Setup(items => items.GetById(expectedItem.Id))
                .Returns(() => expectedItem);

            GenericService<EventModel> service = _mock.Create<GenericService<EventModel>>();

            //Act
            ResultService<EventModel> result = service.GetById(expectedItem.Id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ResultService<EventModel>>(result);
            Assert.IsTrue(result.Success);
            Assert.IsInstanceOf<EventModel>(result.Value);

            EventModel resultItem = result.Value as EventModel;

            Assert.IsNotNull(resultItem);
            Assert.AreEqual(resultItem.GetHashCode(), expectedItem.GetHashCode());
        }
        [Test] public void GetById_WhenRepositoryReturnsNull_ShouldReturnNotFound()
        {
            //Arrange
            Guid id = new Guid();

            var repositoryMock = _mock.Mock<IGenericRepository<EventModel>>();
            repositoryMock
                .Setup(items => items.GetById(id))
                .Returns(() => null);

            GenericService<EventModel> service = _mock.Create<GenericService<EventModel>>();

            //Act
            ResultService<EventModel> result = service.GetById(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Value);
            Assert.IsTrue(result.ErrorCode == ErrorCode.NotFound);
        }
        [Test] public void GetById_WhenRepositoryThrowException_ShouldReturnError()
        {
            //Arrange
            Exception expectedException = new Exception(Guid.NewGuid().ToString());
            Guid id = new Guid();

            var repositoryMock = _mock.Mock<IGenericRepository<EventModel>>();
            repositoryMock
                .Setup(items => items.GetById(id))
                .Throws(expectedException);

            GenericService<EventModel> service = _mock.Create<GenericService<EventModel>>();

            //Act
            ResultService<EventModel> result = service.GetById(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Value);
            Assert.IsTrue(result.Exception == expectedException);
        }
        #endregion

        #region Delete
        [Test] public void Delete_WhenCalled_ShouldReturnOk()
        {
            //Arrange
            EventModel expectedItem = TestsFacade.EventsFacade.BuildEventModelItem();

            var repositoryMock = _mock.Mock<IGenericRepository<EventModel>>();
            repositoryMock
                .Setup(items => items.GetById(expectedItem.Id))
                .Returns(() => expectedItem);

            repositoryMock
                .Setup(items => items.Remove(expectedItem));

            GenericService<EventModel> service = _mock.Create<GenericService<EventModel>>();

            //Act
            ResultService result = service.Delete(expectedItem.Id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ResultService>(result);
            Assert.IsTrue(result.Success);
        }
        [Test] public void Delete_WhenRepositoryReturnsNull_ShouldReturnNotFound()
        {
            //Arrange
            Guid id = new Guid();
            var repositoryMock = _mock.Mock<IGenericRepository<EventModel>>();
            repositoryMock
                .Setup(items => items.GetById(id))
                .Returns(() => null);

            GenericService<EventModel> service = _mock.Create<GenericService<EventModel>>();

            //Act
            ResultService result = service.Delete(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.ErrorCode == ErrorCode.NotFound);
        }
        [Test] public void Delete_WhenRepositoryThrowException_ShouldReturnError()
        {
            //Arrange
            EventModel expectedItem = TestsFacade.EventsFacade.BuildEventModelItem();
            Exception expectedException = new Exception(Guid.NewGuid().ToString());
            Guid id = new Guid();

            var repositoryMock = _mock.Mock<IGenericRepository<EventModel>>();
            repositoryMock
                .Setup(items => items.GetById(id))
                .Throws(expectedException);

            GenericService<EventModel> service = _mock.Create<GenericService<EventModel>>();

            //Act
            ResultService result = service.Delete(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Exception == expectedException);
        }
        #endregion

        #region Update
        [Test] public void Update_WhenCalled_ShouldReturnItem()
        {
            //Arrange
            EventModel expectedItem = TestsFacade.EventsFacade.BuildEventModelItem();

            var repositoryMock = _mock.Mock<IGenericRepository<EventModel>>();
            repositoryMock
                .Setup(items => items.GetById(expectedItem.Id))
                .Returns(expectedItem);

            GenericService<EventModel> service = _mock.Create<GenericService<EventModel>>();

            //Act
            ResultService result = service.Update(expectedItem);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ResultService>(result);
            Assert.IsTrue(result.Success);
        }
        [Test] public void Update_WhenRepositoryReturnsNull_ShouldReturnNotFound()
        {
            //Arrange
            EventModel expectedItem = TestsFacade.EventsFacade.BuildEventModelItem();

            _mock.Mock<IGenericRepository<EventModel>>()
                .Setup(items => items.Update(expectedItem));

            GenericService<EventModel> service = _mock.Create<GenericService<EventModel>>();

            //Act
            ResultService result = service.Update(expectedItem);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.ErrorCode == ErrorCode.NotFound);
        }
        [Test] public void Update_WhenRepositoryThrowException_ShouldReturnError()
        {
            //Arrange
            Exception expectedException = new Exception(Guid.NewGuid().ToString());
            EventModel expectedItem = TestsFacade.EventsFacade.BuildEventModelItem();

            var repositoryMock = _mock.Mock<IGenericRepository<EventModel>>();
            repositoryMock
                .Setup(items => items.GetById(expectedItem.Id))
                .Returns(expectedItem);
            repositoryMock
                .Setup(items => items.Update(expectedItem))
                .Throws(expectedException);

            GenericService<EventModel> service = _mock.Create<GenericService<EventModel>>();

            //Act
            ResultService result = service.Update(expectedItem);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Exception == expectedException, "Not the same Expection");
        }
        #endregion

        [TearDown] public void CleanUp()
        {
            if (_mock != null)
                _mock.Dispose();
        }
    }

}