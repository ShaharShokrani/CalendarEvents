'use strict';

window.App = window.App || {};
App.FilterModel = App.FilterModel || {}

App.FiltersCollection = Backbone.Collection.extend({    
    model: App.FilterModel,
    url: '/filters'
});