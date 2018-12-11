'use strict';
//TODO: use require.
window.App = window.App || {};

// App.

// App.vent.on("some:event", function(){
//     alert("some event was fired!");
// });

// App.vent.trigger("some:event");

App.AppView = Backbone.View.extend({
    el: 'body',
    initialize: function(options) {
        console.log('AppView.initialize');

        if (!options || !options.vent) {
            throw new Error("options params are missing.");
        }
        this.vent =  options.vent;
        
        this.vent.on("filterByGenreId", this.filterByGenreId.bind(this));
        this.template = _.template($('.js-app-template').html());

        this.render();
    },
    render: function() {
        console.log('AppView.render');

        this.$el.append(this.template);
        this.renderEventsView();
        this.renderFiltersView();
    },
    renderEventsView: function() {
        console.log('AppView.renderEventsView');

        let events = new App.EventsCollection;
        events.fetch();
        this.eventsView = new App.EventsView({
            model: events,
            vent: this.vent.bind(this)
        });
    },
    filterByGenreId: function(res) {
        console.log("AppView.filterByGenreId" + res);
        this.eventsView.filterByGenreId(res);
    },
    renderFiltersView: function() {
        console.log('AppView.renderFiltersView');

        let filters = new App.FiltersCollection();
        filters.fetch();
        let filtersView = new App.FiltersView({ 
            model: filters,
            vent: this.vent.bind(this)
        });        
    }
});