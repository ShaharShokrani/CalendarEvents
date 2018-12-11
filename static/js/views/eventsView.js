'use strict';

window.App = window.App || {};

App.EventsView = Backbone.View.extend({
    el: '.js-full-calendar',
    
    options: function () {
      return {
        locale: 'he', //TODO: make this options come as params from server side.
        isRTL: true,
        header: { center: 'month,agendaWeek' }, // buttons for switching between views
        views: {
            month: { // name of view
                titleFormat: 'YYYY, MM, DD' //TODO: make it behave according to the user.
            // other view-specific options here
            }
        },
        events: this.model.toJSON(),
        eventClick: this.renderEventModal.bind(this)
      };
    },
    initialize: function() {
        console.log('EventsView.initialize');

        this.listenTo(this.model, 'sync', this.render.bind(this));
        this.listenTo(this.model.filtered, 'reset', this.rerenderEvents.bind(this));
    },
    render: function(res) {
        console.log('EventsView.render');
        console.log(this.model.toJSON());
        
        this.$el.fullCalendar(this.options());
        return this;
    },
    rerenderEvents: function() {
        console.log('EventsView.rerenderEvents');

        this.$el.fullCalendar('removeEvents');
        this.$el.fullCalendar('addEventSource', this.model.filtered.toJSON());
        this.$el.fullCalendar('rerenderEvents');
    },
    filter: function(res) {
        console.log('EventsView.filter');

        this.model.filterByGenreId(res.GenreId); //TODO: should be renamed to collection instead of model.
    },
    renderEventModal: function(res) {
        console.log('EventsView.renderEventModal');
        //this.EventModalView = new App.EventModalView({model: todo});
        console.log(res);
        
    },
});