'use strict';

window.App = window.App || {};
App.AppModel = App.AppModel || {}

App.AppCollection = Backbone.Collection.extend({    
    model: App.AppModel,
});