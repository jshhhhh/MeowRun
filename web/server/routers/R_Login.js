// router for login 
import {HandleLogin, SignUp,HandleRefreshToken} from '../controllers/C_Login.js'
import express from 'express'
import {CheckValidation} from '../middlewares/CheckValidation.js'


const Router = express.Router()

 
// Router.post('/signup'
// ,[check('password').exists()]
// ,SignUp)    
Router.post('/signup',CheckValidation,SignUp)
Router.post('/login',HandleLogin)
Router.get('/refresh',HandleRefreshToken)





export default Router