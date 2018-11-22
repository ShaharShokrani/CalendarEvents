
function log(req, res, next) {
    console.log("Logging...");
    next();
}

module.exports = log;

// //(function (exports, require, module, __filename, __dirname) {
//     console.log(__filename);
//     console.log(__dirname);

// //var x = ;
// var url = 'http://mylogger.io/log';

// function log(message){    
//     console.log(message);
// }

// module.exports = log;
// module.exports.log = log;
// exports.log = log;
// //module.exports.endPoint = log;
// //exports = log // module.exports
// //})