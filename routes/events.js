'use strict'

const fs = require('fs');
const express = require('express');
const router = express.Router();
const Joi = require('joi');
const moment = require('moment');

const eventsJson = fs.readFileSync('static/events.json');
const events = JSON.parse(eventsJson).items;
console.log(eventsJson);
console.log(events);

router.get('/', (req, res) => {
    res.send(events);
});

router.get('/:year?/:month?/:day?/:id?', function (req, res) {
    var result = events.filter(event => {
        if (req.params.day) {
            return event.id === req.params.day;
        }
        else if (req.params.day) {
            return event.year.includes(parseInt(req.params.year)) &&
                event.month.includes(parseInt(req.params.month)) &&
                event.day.includes(parseInt(req.params.day));
        }
        else if (req.params.month) {
            return event.month.includes(parseInt(req.params.month)) &&
                event.day.includes(parseInt(req.params.day));
        }
        else if (req.params.year) {
            return event.year.includes(parseInt(req.params.year));
        }
        else {
            return true;
        }
    });

    console.log(req.params);
    res.send(result);
});

var getIdFromReq = function () {
    const event = events.find(c => c.id === parseInt(req.params.id));
    if (!event) return res.status(404).send('The given id was not found...');
    res.send(event);
}

router.post('/', (req, res) => {
    const { error } = validateCourse(req.body);
    if (error) {
        res.status(400).send(error.details[0].message);
        return;
    }
    const event = {
        id: events.length + 1,
        name: req.body.name
    };
    events.push(event);
    res.send(event);
});

router.delete('/:id', (req, res) => {
    const event = events.find(c => c.id === parseInt(req.params.id));
    if (!event) return res.status(404).send('The given id was not found...');

    const index = events.indexOf(event);
    events.splice(index, 1);

    res.send(event);
});

router.put('/:id', (req, res) => {
    const course = events.find(c => c.id === parseInt(req.params.id));
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