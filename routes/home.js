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

module.exports = router;