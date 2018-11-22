const debug = require('debug')('app:startup');
//const dbDebugger = require('debug')('app:db');
const config = require('config');
const morgan = require('morgan');

const logger = require('./middleware/logger');
const courses = require('./routes/courses');
const home = require('./routes/home');
const express = require('express');
const app = express();

app.set('view engine', 'pug');
app.set('views', './views'); //default

app.use(express.json());
app.use(express.urlencoded({ extended:true }));
app.use(express.static('public'));
app.use('/api/courses', courses);
app.use('/', home);

app.use(logger);

console.log("Application name:" + config.get('name'));
console.log("Mail name:" + config.get('mail.host'));

// if (app.get('env') === 'development') {
//     app.use(morgan('tiny'));
//     startupDebugger("morgan enabled...");
// }

//export DEBUG=app:startup
//DB work...
//dbDebugger('Connected to the database...');



const port = process.env.PORT || 3000;
app.listen(port, ()=>{
    console.log(`Listening on port ${port}`);
});