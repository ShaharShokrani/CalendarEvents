'use strict';

window.App = window.App || {};

App.EventModalView = Backbone.View.extend({
    el:'body',
    initialize: function() {
        this.template = _.template($('.js-event-modal-template').html());
        
    },
    render: function() {
        this.$el.append(this.template);
//this.$el.html(this.template(this.model.toJSON()));
        return this;
    },
    clear: function() {
        this.model.destroy();
    }
});   