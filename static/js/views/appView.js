'use strict';
//TODO: use require.
window.App = window.App || {};

App.AppView = Backbone.View.extend({
    el: 'body',
    model: App.AppModel,
    initialize: function(options) {
        console.log('AppView.initialize');

        // if (!options || !options.model || !options.model.isValid()) {
        //     throw new Error("options params are missing.");
        // }
        // this.model =  options.model;
        
        this.template = _.template($('.js-app-template').html());

        this.render();
    },
    render: function() {
        console.log('AppView.render');

        this.$el.append(this.template);

        // let eventsModel = new App.EventsCollection;
        // eventsModel.fetch();

        // this.fullCalendarView = new App.FullCalendarView({
        //     model: eventsModel,
        // });

        let filters = new App.FiltersCollection();
        filters.fetch();

        let filtersView = new App.FiltersView({ model: filters });
    },
    // renderFilters: function() {
    //     console.log('AppView.renderFilters');

    //     this.filtersView.render({
    //         model: this.model.get("Filters")
    //     });
    // },
    // renderFullCalendar: function() {
    //     console.log('AppView.renderFullCalendar');

    //     this.fullCalendarView.render({
    //         collection: this.model.get("Events")
    //     });
    // }
});