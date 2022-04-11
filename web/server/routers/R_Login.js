// router for login 
import {HandleLogin, SignUp,HandleRefreshToken} from '../controllers/C_Login.js'
import express from 'express'

const Router = express.Router()

Router.post('/signup',SignUp)
Router.post('/login',HandleLogin)
Router.get('/refresh',HandleRefreshToken)


export default Router