'use strict';

window.App = window.App || {};

App.FiltersCollection = Backbone.Collection.extend({          
    url: '/filters/'          
});