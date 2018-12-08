'use strict';

window.App = window.App || {};

App.EventsCollection = Backbone.Collection.extend({          
    model: App.EventModel,
    url: '/events'
});


// applyFilters: function (filters) {
//     var filtered = this.filter(event => {
//       const eventFilters = event.get("filters");
  
//       if (!Array.isArray(eventFilters.year) ||
//       !Array.isArray(eventFilters.month) ||
//       !Array.isArray(eventFilters.day)){
//         //TODO: set a logger for 'bad' inputs.
//         return true;
//       }
  
//       if (filters.day && filters.month && filters.year) {
//         return eventFilters.year.includes(filters.year) &&
//               eventFilters.month.includes(filters.month) &&
//               eventFilters.day.includes(filters.day);
//       }
//       else if (filters.month && filters.year) {
//         return eventFilters.year.includes(filters.year) &&
//               eventFilters.month.includes(filters.month);
//       }
//       else if (filters.year) {
//         return eventFilters.year.includes(filters.year);
//       }
//       else {
//           return true;
//       }
//     });
  
//     return new EventsCollection(filtered);
//   }