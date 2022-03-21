import express from "express";
import cookieParser from "cookie-parser";
import cors from 'cors';
import dotenv from 'dotenv';

const app = express()

// ====================== routes ====================== // 
// TO DO : 해당 파트 고쳐쓰기
// 랜딩 페이지 루트
app.get('/', (req, res) => {

})

// 로그인 루트
app.get('/auth', (req,res, next) => {
    
    next()
})

// api 루트
app.get('/apis', (req,res) => {
    
})

// ====================== routes ====================== //

// ====================== middlewares ====================== //
// TO DO : fix here
app.use()
app.use()
app.use()
app.use()
app.use()
// ====================== middlewares ====================== //