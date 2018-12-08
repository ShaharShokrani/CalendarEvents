'use strict';

window.App = window.App || {};

App.AppModel = Backbone.Model.extend({
    defaults: function() {
        return {
            "Filters": null,
            "Events": null
        };
    },
    // url: '/filters',
    // validate: function(attrs, options) {        
    //     console.log("FilterModel.validate");

    //     if (!attrs.Options || !Array.isArray(attrs.Options)) {
    //         return "Options is null or not array object";
    //     }
    //     if (!attrs.Id || !attrs.TypeId || !attrs.Name) { //TODO: render the TypeIds enum to server and make sure typeId is a valid member.
    //         return "Id/TypeId/Name are missing";
    //     }
    // }
});