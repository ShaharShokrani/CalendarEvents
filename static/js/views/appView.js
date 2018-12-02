'use strict';

window.App = window.App || {};

App.AppView = Backbone.View.extend({
    initialize: function() {
        this.$el = $('.js-events-app');
        //TODO: add validation for collection.

        this.fullCalendarView = new App.FullCalendarView();
        //this.filtersView = new FiltersView();

        //Click listeners
        this.listenTo(this.collection, 'add', this.addEvent);
        this.listenTo(this.collection, 'sync', this.render);
        this.collection.fetch();
    },
    render: function() {
        this.fullCalendarView.render({collection: this.collection});
    }
});