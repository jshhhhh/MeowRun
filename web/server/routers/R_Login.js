// router for login 
const {HandleLogin, SignUp,HandleRefreshToken} = require('../controllers/C_Login.js')
const express = require('express')
const CheckValidation = require('../middlewares/CheckValidation.js')


const Router = express.Router()

 
  
Router.post('/signup',CheckValidation,SignUp)
Router.post('/login',HandleLogin)
Router.get('/refresh',HandleRefreshToken)




module.exports = Router