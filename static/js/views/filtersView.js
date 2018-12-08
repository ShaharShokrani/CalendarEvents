'use strict';

window.App = window.App || {};

App.FiltersView = Backbone.View.extend({
    el: '.js-filters',

    initialize: function(options) {
        console.log("FiltersView.initialize");
        
        this.listenTo(this.model, 'sync', this.renderOptions);
        this.template = $(".js-filters-template").html();

        this.render();
    },
    render: function() {
        console.log("FiltersView.render");

        //this.model.fetch();
		this.$el.html(Mustache.render(this.template));
		return this;
    },
    renderOptions: function() {
        console.log("FiltersView.renderOptions");

        this.model.models.forEach(function(model, index, list) {
            if (!model.isValid()) {
                throw new Error("FiltersView.render: collection missing options property. model:" + model);
            }
            let attributes = model.toJSON();

            switch (attributes.TypeId) {
                case 1:
                    let genreView = new App.FiltersView.Genre({
                        model: model
                    });
                    break;
                default:
                    break;
            }

        });
    },
});

App.FiltersView.Genre = Backbone.View.extend({    
    el: '.js-filter-genre-id',
    initialize: function(options) {
        console.log("FiltersView.Genre.initialize");

        if (!this.model.isValid()) {
            throw new Error("FiltersView.Genre.render model not valid, model:'" + model.toJSON() + "'");
        }
        this.render();
    },
    render: function() {
        console.log("FiltersView.Genre.render");
        let self = this;

        let attributes = this.model.toJSON();
        attributes.Options.forEach(function(item){
            self.$el.append($('<option>', { 
                value: item.Value,
                text : item.Text 
            }));
        });
		return this;
    }
});