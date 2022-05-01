
const express = require('express')
const router = express.Router()
const {updateVotes,currentVotes} = require('../controllers/C_voting.js')


router.post('/update',updateVotes)
router.get('/votes',currentVotes)


module.exports =router