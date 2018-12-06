'use strict';

window.App = window.App || {};

App.AppModel = Backbone.Model.extend({
    defaults: function() {
        return {
            id: null, //String/Integer
        };
    }
});