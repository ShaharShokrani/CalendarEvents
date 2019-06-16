using CalendarEvents.Models;
using Autofac.Extras.Moq;
using NUnit.Framework;
using System.Collections.Generic;
using CalendarEvents.Tests;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace CalendarEvents.Services.Tests
{
    public class OrderByServiceTests : BaseTest
    {
        [SetUp]
        public void Setup()
        {
        }

        //1. Is Ok
        //2. throws exception
        //3. propertyName is not valid
        //4. Value is not convertable to propertyName
        //5. Operation is not valid.


            //TODO
        //[Test] public void BuildExpression_WhenCalled_ShouldReturnOk()
        //{
        //    //Arrange
        //    OrderByStatement<EventModel> orderByStatement = TestsFacade.OrderBytatementFacade.BuildOrderByStatement<EventModel>();
        //    OrderByService service = new OrderByService();

        //    //Act
        //    ResultService<Func<IQueryable<EventModel>, IOrderedQueryable<EventModel>>> result = service.GetOrderBy<EventModel>(orderByStatement);

        //    //Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOf<ResultService<Func<IQueryable<EventModel>, IOrderedQueryable<EventModel>>>(result);
        //    Assert.IsTrue(result.Success);
        //}

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

        [Test] public void BuildExpression_WhenValueNotValid_ShouldReturnFail()
        {
            //Arrange
            FilterStatement<EventModel> filterStatement = TestsFacade.FilterStatementFacade.BuildFilterStatement<EventModel>();
            filterStatement.Value = 1;
            filterStatement.PropertyName = "Id";
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

        [Test] public void BuildExpression_WhenOperationNotValid_ShouldReturnFail()
        {
            //Arrange
            FilterStatement<EventModel> filterStatement = TestsFacade.FilterStatementFacade.BuildFilterStatement<EventModel>();
            filterStatement.Operation = FilterOperation.Undefined;
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
        }
    }

}