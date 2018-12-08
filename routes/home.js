'use strict'

const express = require('express');
const router = express.Router();

router.get('/', function(req, res){
    console.log('Hello world');
    res.render('index', {name: 'Shahar'});
});

router.get('/about', function(req, res){
    console.log('About router');
    res.render('about', {name: 'Shahar'});
});

router.get('/appTest', function(req, res){
    console.log('app test router');
    res.render('appTest', {name: 'Shahar'});
});

router.get('/eventModalTest', function(req, res){
    console.log('eventModalTest test router');
    res.render('eventModalTest', {name: 'Rafael'});
});

router.get('/filtersTest', function(req, res){
    console.log('filters test router');
    res.render('filtersTest', {name: 'Shahar'});
});

module.exports = router;