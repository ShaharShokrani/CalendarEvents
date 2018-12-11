'use strict';

window.App = window.App || {};

App.EventsCollection = Backbone.Collection.extend({          
    model: App.EventModel,
    url: '/events',
    initialize: function (models) {
        this.filtered = new Backbone.Collection();
    },
    filterByGenreId: function (params) {
        if (params !== -1)
        {
            var filteredEvents = this.filter(function(item){
                let filters = item.get("filters");
                return filters.genreId.includes(params);
            });
            this.filtered.reset(filteredEvents);
        }
        else
        {
            this.filtered.reset(this.models); 
        }
    },
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