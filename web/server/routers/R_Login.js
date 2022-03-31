// router for login 
import {HandleLogin, UserInfo} from '../controllers/C_Login.js'
import express from 'express'

const Router = express.Router()

Router.post('/signup',UserInfo)
Router.post('/auth',HandleLogin)

export default Router