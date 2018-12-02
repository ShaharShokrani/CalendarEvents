'use strict';

window.App = window.App || {};

App.FullCalendarView = Backbone.View.extend({
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
        events: this.events,
        eventClick: this.renderEventModal.bind(this)
      };
    },
    initialize: function() {
        this.$el = $('.js-full-calendar');
    },
    render: function(res) {
        this.events = res.collection.toJSON();
        this.$el.fullCalendar(this.options());
        return this;
    },
    renderEventModal: function(res) {
        //this.EventModalView = new App.EventModalView({model: todo});
        console.log(res);
    },
});