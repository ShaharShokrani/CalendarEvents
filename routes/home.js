const express = require('express');
const router = express.Router();

app.get('/', function(req, res){
    console.log('Hello world');
    res.render('index', {name: 'Shahar'});
});

app.get('/about', function(req, res){
    console.log('About router');
    res.render('about', {name: 'Shahar'});
});

module.exports = router;