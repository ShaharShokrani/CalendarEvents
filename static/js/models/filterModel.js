'use strict';

window.App = window.App || {};

App.FilterModel = Backbone.Model.extend({
    defaults: function() {
        return {
            "Id": null,
            "TypeId": null,
            "Name": null,
            "Options": []
        };
    },
    validate: function(attrs, options) {
        console.log("FilterModel.validate");
        
        if (!attrs.Options || Array.isArray(attrs.Options)) {
            return "Options not valid";
        }
    }
});