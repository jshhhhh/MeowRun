import express from "express";
import cookieParser from "cookie-parser";
import cors from 'cors';
import dotenv from 'dotenv';
import { ConnectDB } from "./config/dbconn.js";
import UserRoute from "./routers/R_Login.js";
import mongoose from 'mongoose';

dotenv.config()
const app = express()
const PORT = process.env.PORT 

ConnectDB() 

// built-in middleware to handle urlencoded data
app.use(express.urlencoded({ extended: false}));

// built-in middleware for json
app.use(express.json());

// ====================== routes ====================== // 
// TO DO : 해당 파트 고쳐쓰기
// 랜딩 페이지 루트
app.get('/', (req, res) => {

    
})

// 로그인 루트
app.get('/auth',UserRoute, (req,res, next) => {
    
    next()
})

// api 루트
app.get('/apis', (req,res) => {
    
})

// ====================== routes ====================== //

// ====================== middlewares ====================== //
// TO DO : fix here
app.use('/user',UserRoute)
// app.use()
// app.use()
// app.use()
// ====================== middlewares ====================== //




mongoose.connection.once('open', () => {
    console.log('Connected to MongoDB')
    app.listen(PORT, () =>  console.log(`Server running on port ${PORT}`));
})