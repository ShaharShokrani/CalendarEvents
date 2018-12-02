'use strict';

window.App = window.App || {};

App.EventsCollection = Backbone.Collection.extend({          
    url: '/events/'          
});