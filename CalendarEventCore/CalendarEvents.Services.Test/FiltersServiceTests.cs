using CalendarEvents.Models;
using Autofac.Extras.Moq;
using NUnit.Framework;
using System.Collections.Generic;
using CalendarEvents.Tests;
using System.Linq.Expressions;
using System;

namespace CalendarEvents.Services.Tests
{
    public class FiltersServiceTests : BaseTest
    {
        private AutoMock _mock = null;

        [SetUp]
        public void Setup()
        {
            _mock = AutoMock.GetLoose();
        }

        //1. filterStatements is null.
        //2. Is Ok
        //3. throws exception
        //4. propertyName is not valid

        [Test] public void BuildExpression_WhenCalled_ShouldReturnOk()
        {
            //Arrange
            IEnumerable<FilterStatement<EventModel>> filterStatements = TestsFacade.FilterStatementFacade.BuildFilterStatementList<EventModel>();
            FiltersService<EventModel> service = new FiltersService<EventModel>(filterStatements);

            //Act
            ResultService<Expression<Func<EventModel, bool>>> result = service.BuildExpression();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ResultService<Expression<Func<EventModel, bool>>>>(result);
            Assert.IsTrue(result.Success);
        }

        [Test] public void BuildExpression_WhenPropertyNotValid_ShouldReturnFail()
        {
            //Arrange
            FilterStatement<EventModel> filterStatement = TestsFacade.FilterStatementFacade.BuildFilterStatement<EventModel>();
            filterStatement.PropertyName = Guid.NewGuid().ToString();
            IEnumerable<FilterStatement<EventModel>> filterStatements = new List<FilterStatement<EventModel>>()
            {
                filterStatement
            };

            FiltersService<EventModel> service = new FiltersService<EventModel>(filterStatements);

            //Act
            ResultService<Expression<Func<EventModel, bool>>> result = service.BuildExpression();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Value);
            Assert.IsTrue(result.ErrorCode == ErrorCode.EntityNotValid);
        }

        [TearDown] public void CleanUp()
        {
            if (_mock != null)
                _mock.Dispose();
        }
    }

}