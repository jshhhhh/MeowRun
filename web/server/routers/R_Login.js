// router for login 
import {UserInfo} from '../controllers/C_Login.js'
import express from 'express'

const Router = express.Router()

Router.post('/signup',UserInfo)

export default Router