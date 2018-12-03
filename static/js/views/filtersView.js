'use strict';

window.App = window.App || {};

App.FiltersView = Backbone.View.extend({    
    initialize: function() {
        this.template = _.template($('.js-filters-template').html());
    },
    render: function() {
    }
});