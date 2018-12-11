'use strict';

window.App = window.App || {};

App.FiltersView = Backbone.View.extend({
    el: '.js-filters',
    events: {
        "change .js-genre-id": "onGenreChanged"
    },
    initialize: function(options) {
        console.log("FiltersView.initialize");
        
        //if (!options || !options.vent || !(options.vent instanceof Backbone.Events)) { //TODO make a genric common view.
        if (!options || !options.vent) { //TODO make a genric common view.
            throw new Error("options params are missing.");
        }
        this.vent =  options.vent;

        this.listenTo(this.model, 'sync', this.renderOptions);
        this.template = $(".js-filters-template").html();

        this.render();
    },
    render: function() {
        console.log("FiltersView.render");

		this.$el.html(Mustache.render(this.template));
		return this;
    },
    renderOptions: function() {
        console.log("FiltersView.renderOptions");
        let self = this;

        this.model.models.forEach(function(model, index, list) {
            if (!model.isValid()) {
                throw new Error("FiltersView.render: collection missing options property. model:" + model);
            }
            let attributes = model.toJSON();

            switch (attributes.TypeId) {
                case 1:
                    self.$el.find('.js-genre-id').append($('<option>', { 
                        value: -1,
                        text : "Please select" 
                    }))
            
                    attributes.Options.forEach(function(item){
                        self.$el.find('.js-genre-id').append($('<option>', { 
                            value: item.Value,
                            text : item.Text 
                        }));
                    });
                    break;
                default:
                    break;
            }

        });
    },
    renderGenre: function() {
        console.log("FiltersView.renderGenre");
        let self = this;
		return this;
    },
    onGenreChanged: function(event) {
        console.log('FiltersView.onGenreChanged');
        this.vent.trigger("genre", { GenreId: parseInt(event.target.value) });
    }
});

// App.FiltersView.Genre = Backbone.View.extend({    
//     el: '.js-filter-genre-id',
//     initialize: function(options) {
//         console.log("FiltersView.Genre.initialize");

//         if (!this.model.isValid()) {
//             throw new Error("FiltersView.Genre.render model not valid, model:'" + model.toJSON() + "'");
//         }
//         this.render();

//     },
//     render: function() {

//     },
//     onChange: function(event) {
//         this.$el.trigger('FiltersView.Genre.change', event);
//     }
// });