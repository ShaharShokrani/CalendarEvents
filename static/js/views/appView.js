'use strict';

window.App = window.App || {};

App.AppView = Backbone.View.extend({
    el: 'body', 
    initialize: function() {
        this.template = _.template($('.js-app-template').html());

        //Click listeners
        this.listenTo(this.collection, 'add', this.renderFullCalendar); //TODO: check if needed
        this.listenTo(this.collection, 'sync', this.renderFullCalendar);
        this.collection.fetch({
            data:{}
        });

        this.render();
    },
    render: function() {
        this.$el.append(this.template);

        this.fullCalendarView = new App.FullCalendarView({
            el: '.js-full-calendar'
        });
        this.filtersView = new App.FiltersView({
            el: '.js-filters'
        });
        this.renderFilters();
    },
    renderFilters: function() {
        
    },
    renderFullCalendar: function() {
        this.fullCalendarView.render({collection: this.collection});
    }
});