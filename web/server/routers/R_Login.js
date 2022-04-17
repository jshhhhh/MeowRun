// router for login 
const {SignUp,HandleLogin,HandleRefreshToken,HandleLogout} = require('../controllers/C_Login.js')
const express = require('express')
const CheckValidation = require('../middlewares/CheckValidation.js')


const Router = express.Router()

 
  
Router.post('/signup',CheckValidation,SignUp)
Router.post('/login',HandleLogin)
Router.get('/refresh',HandleRefreshToken)
Router.get('/logout',HandleLogout)
Router.delete('/delete',)



module.exports = Router