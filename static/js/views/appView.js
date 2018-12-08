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

        let events = new App.EventsCollection;
        events.fetch();

        this.eventsView = new App.EventsView({
            model: events,
        });

        let filters = new App.FiltersCollection();
        filters.fetch();

        let filtersView = new App.FiltersView({ model: filters });
    }
});