'use strict';

window.App = window.App || {};

App.FiltersCollection = Backbone.Collection.extend({    
    model: App.FilterModel,
    url: '/filters'
});