/* import { Stats } from 'fs'; */

const Joi = require('joi');
const express = require('express');
const app = express();
app.use(express.json());
/* app.configure(function(){
    app.use(express.bodyParser());
    app.use(app.router);
  }); */
const courses = [
    { id: 1, name: 'course1'},
    { id: 2, name: 'course2'},
    { id: 3, name: 'course3'},
];

app.get('/', (req, res)=>{
    res.send('Ido is the king!');
});

app.get('/api/courses', (req, res)=>{
    //res.send(JSON.stringify([1,2,3]));
    res.send(courses);
});

// /api/courses/1
app.get('/api/courses/:id', (req, res) => {
    const course = courses.find(c => c.id === parseInt(req.params.id));
    if (!course) return res.status(404).send('The given id was not found...');
    res.send(course);
    //res.send(req.params.id);    
});

app.post('/api/courses', (req, res) => {
    const { error } = validateCourse(req.body);
    //if (result.error) {
    if (error) {
        res.status(400).send(error.details[0].message);
        return;
    }
/*     if (!req.body.name || req.body.name < 3) {
        res.status(400).send('invalid name');
        return;
    } */    
    const course = {
        id: courses.length + 1,
        name: req.body.name
    };
    courses.push(course);
    res.send(course);
});

app.delete('/api/courses/:id', (req, res) => {
    const course = courses.find(c => c.id === parseInt(req.params.id));
    if (!course) return res.status(404).send('The given id was not found...');

    const index = courses.indexOf(course);
    courses.splice(index,1);

    res.send(course);
});

app.put('/api/courses/:id', (req, res) => {
    const course = courses.find(c => c.id === parseInt(req.params.id));
    if (!course) return res.status(404).send('The given id was not found...');

    const { error } = validateCourse(req.body); //result.error    
    //const result = validateCourse(req.body); //result.error    
    if (error) {
        res.status(400).send(error.details[0].message);
        return;
    }
    
    course.name = req.body.name;
    res.send(course);
    // Look up the course
    // If not existing, return 404

    // Validate
    // If invalid, return 400 - Bad request.
    
    // Update course
    // Return the updated course
});

function validateCourse(course) {
    const schema = {
        name:Joi.string().min(3).required()
    };
    const result = Joi.validate(course, schema);
    return result;
}

/* // /api/posts/2018/09?sortBy=name
app.get('/api/posts/:year/:month', (req, res)=>{
    res.send(req.params); // 2019/08
    res.send(req.query); // sortBy
}); */

//console.log(process.env);
//PORT
const port = process.env.PORT || 3000;
app.listen(port, ()=>{
    console.log(`Listening on port ${port}`);
});

//app.post();
//app.delete();
//app.put();

//var _ = require('underscore');

//module.exports.add = function(a, b) { return a + b; };

//const http = require('http');

//const server = http.createServer((req, res) => {
//    if (req.url === '/') {
//        res.write('Hello World');
//        res.end();
//    }
//    if (req.url === '/api/courses') {
//        res.write(JSON.stringify([1,2,3]));
//        res.end();
//    }
//});

//server.listen(3000);
//console.log('listening on port 3000');
//Core module
//File or folder
//node_modules

//var result = _.contains([1,2,3],2);
//console.log(result);

//const EventEmitter = require('events');
//const emitter = new EventEmitter();

//emitter.emit();

//const path = require('path');
//const os = require('os');
//const fs = require('fs');

//const files = fs.readdirSync('./');
//var mem = os.totalmem();
//var freeMem = os.freemem();

//fs.readdir('$', function(err, files) {
//    if (err) console.log('Error', err);
//    else console.log('Result', files);
//});

//console.log(files);
//console.log(mem);
//console.log(freeMem);

//var pathObj = path.parse(__filename);
//console.log(pathObj);

//const log = require('./logger');

//logger = 1;
//logger.log('message');
//log('message');

//require('./subFolder/logger');
//require('../logger'); parent folder.

//console.log(); //global

//var message = '';
//console.log(global.message);

//console.log(module);

//setTimeout();
//clearTimeout();

//setInterval();
//clearInterval();

//global.console.log('hello');