// router for login 
const {SignUp,HandleLogin,HandleRefreshToken,HandleLogout,D, DeleteUser} = require('../controllers/C_Login.js')
const express = require('express')
const CheckValidation = require('../middlewares/CheckValidation.js')


const Router = express.Router()

 
  
Router.post('/signup',CheckValidation,SignUp)
Router.post('/login',HandleLogin)
Router.get('/refresh',HandleRefreshToken)
Router.get('/logout',HandleLogout)
Router.delete('/delete',DeleteUser)



module.exports = Router