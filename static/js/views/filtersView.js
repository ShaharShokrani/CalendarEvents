'use strict';

window.App = window.App || {};

App.FiltersView = Backbone.View.extend({
    $genreId: null,

    initialize: function() {
        console.log("FiltersView.initialize");
        this.template = $(".js-filters-template").html();
    },
    render: function(options) {
        console.log("FiltersView.render");

        if (!options || !options.collection)
        {
            throw new Error("FiltersView.render missing params, options:'" + options + "'");
        }
        this.collection = options.collection;

		this.$el.html(Mustache.render(this.template));
        this.$el.append(this.template);

        this.$genreId = this.$el.find('.js-genre-id');
        this.collection.forEach(function(model, index, list) {
            if (!model.isValid()) {
                throw new Error("FiltersView.render: collection missing options property. model:" + model);
            }

            let options = model.get("Options");
            console.log(model);
            console.log(index);
            console.log(list);
        });
		return this;
    },
    renderGenreId: function() {
        console.log("FiltersView.renderGenreId");
    }
});