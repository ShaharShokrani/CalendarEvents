'use strict';

window.App = window.App || {};

App.EventModel = Backbone.Model.extend({
    defaults: function() {
        return {
            id: null, //String/Integer
            title: '', //String (*)
            allDay: null, //true or false
            start: moment().toISOString(), //Moment object (*)
            end: null, //Moment object
            url: null, //String
            className: null, //String/Array
            editable: null, //true or false
            startEditable: null, //true or false
            durationEditable: null, //true or false
            resourceEditable: null, //true or false
            rendering: null, //'background' or 'inverse-background'
            overlap: null, //true or false
            constraint: null, //event ID, "businessHours", object
            source: null, //Event Source Object
            color: null,
            backgroundColor: null,
            borderColor: null,
            textColor: null,
            filters: {
                genreId: [],
                subGenreId: [],
                placeName: null,
                price: [],
                city: null,
                country: null,
                state: null
            }
        };
    }
});