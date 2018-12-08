'use strict';

window.App = window.App || {};

App.FullCalendarView = Backbone.View.extend({
    el: '.js-full-calendar',
    model: App.EventsCollection,
    
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
        events: this.model,
        eventClick: this.renderEventModal.bind(this)
      };
    },
    initialize: function() {
        console.log('FullCalendarView.initialize');

        this.listenTo(this.model, 'sync', this.render);
    },
    render: function(res) {
        console.log('FullCalendarView.render');
        
        //this.model.fetch();
        this.$el.fullCalendar(this.options());
        return this;
    },
    renderEventModal: function(res) {
        console.log('FullCalendarView.renderEventModal');
        //this.EventModalView = new App.EventModalView({model: todo});
        console.log(res);
    },
});