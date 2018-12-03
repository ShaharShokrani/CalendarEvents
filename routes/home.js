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

router.get('/mainTest', function(req, res){
    console.log('main test router');
    res.render('mainTest', {name: 'Shahar'});
});

router.get('/filtersTest', function(req, res){
    console.log('filters test router');
    res.render('filtersTest', {name: 'Shahar'});
});

module.exports = router;