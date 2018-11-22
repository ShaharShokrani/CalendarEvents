const express = require('express');
const router = express.Router();

router.get('/', (req, res)=>{    
    res.render('index', { 
        message:'Message', 
        title:'My title' 
    });
});

module.exports = router;