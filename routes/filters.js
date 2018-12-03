'use strict'

const fs = require('fs');
const express = require('express');
const router = express.Router();
const Joi = require('joi');
const moment = require('moment');

const filtersJson = fs.readFileSync('static/filters.json');
const filters = JSON.parse(filtersJson).items;
console.log(filtersJson);
console.log(filters);

router.get('/', (req, res) => {
    res.send(filters);
});

router.get('/:year?/:month?/:day?/:id?', function (req, res) {
    var result = filters.filter(filter => {
        if (req.params.day) {
            return filter.id === req.params.day;
        }
        else if (req.params.day) {
            return filter.year.includes(parseInt(req.params.year)) &&
                filter.month.includes(parseInt(req.params.month)) &&
                filter.day.includes(parseInt(req.params.day));
        }
        else if (req.params.month) {
            return filter.year.includes(parseInt(req.params.year)) &&
                filter.month.includes(parseInt(req.params.month));
        }
        else if (req.params.year) {
            return filter.year.includes(parseInt(req.params.year));
        }
        else {
            return true;
        }
    });

    console.log(req.params);
    res.send(result);
});

var getIdFromReq = function () {
    const filter = filters.find(c => c.id === parseInt(req.params.id));
    if (!filter) return res.status(404).send('The given id was not found...');
    res.send(filter);
}

router.post('/', (req, res) => {
    const { error } = validateCourse(req.body);
    if (error) {
        res.status(400).send(error.details[0].message);
        return;
    }
    const filter = {
        id: filters.length + 1,
        name: req.body.name
    };
    filters.push(filter);
    res.send(filter);
});

router.delete('/:id', (req, res) => {
    const filter = filters.find(c => c.id === parseInt(req.params.id));
    if (!filter) return res.status(404).send('The given id was not found...');

    const index = filters.indexOf(filter);
    filters.splice(index, 1);

    res.send(filter);
});

router.put('/:id', (req, res) => {
    const course = filters.find(c => c.id === parseInt(req.params.id));
    if (!course) return res.status(404).send('The given id was not found...');

    const { error } = validateCourse(req.body); //result.error    
    if (error) return res.status(400).send(error.details[0].message);

    course.name = req.body.name;
    res.send(course);
});

function validateCourse(course) {
    const schema = {
        name: Joi.string().min(3).required()
    };
    const result = Joi.validate(course, schema);
    return result;
}

module.exports = router;