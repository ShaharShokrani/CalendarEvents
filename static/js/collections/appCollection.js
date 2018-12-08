'use strict';

window.App = window.App || {};

App.AppCollection = Backbone.Collection.extend({    
    model: App.AppModel,
});