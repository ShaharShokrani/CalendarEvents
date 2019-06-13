using CalendarEvents.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CalendarEvents.Tests
{
    public class TestsFacade
    {
        public static class EventsFacade
        {
            public static List<EventModel> BuildEventModelList(int count = 1)
            {
                List<EventModel> resultList = new List<EventModel>(count);

                while (count > 0)
                {
                    resultList.Add(BuildEventModelItem());
                    count--;
                }

                return resultList;
            }            
            public static EventModel BuildEventModelItem()
            {
                return new EventModel()
                {
                    End = DateTime.UtcNow.AddHours(1),
                    Id = Guid.NewGuid(),
                    IsAllDay = false,
                    Start = DateTime.UtcNow.AddMinutes(1),
                    Name = Guid.NewGuid().ToString(),
                    URL = Guid.NewGuid().ToString()
                };
            }
        }

        public static class FilterStatementFacade
        {
            public static IEnumerable<FilterStatement<TEntity>> BuildFilterStatementList<TEntity>(int count = 1) where TEntity : IBaseModel
            {
                List<FilterStatement<TEntity>> resultList = new List<FilterStatement<TEntity>>(count);

                while (count > 0)
                {
                    resultList.Add(BuildFilterStatement<TEntity>());
                    count--;
                }

                return resultList;
            }
            public static FilterStatement<TEntity> BuildFilterStatement<TEntity>() where TEntity : IBaseModel
            {
                return new FilterStatement<TEntity>()
                {
                    Operation = FilterOperation.Equal,
                    PropertyName = "Id",
                    Value = Guid.NewGuid().ToString()
                };
            }
        }
    }
}
