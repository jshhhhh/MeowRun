// controller for login 
const bcrypt =require("bcrypt")
const jwt =require ("jsonwebtoken")
const { validationResult}=require("express-validator")
const User = require('../models/user.js')
const sequelize =require('../config/configdb.js')
const dotenv       = require('dotenv')
dotenv.config()




const statusCode = {
    OK : 200, 
    CREATED : 201, 
    UNAUTHORIZED : 401,
    BAD_REQUEST : 400,
    CONFLICT : 409,
    INTERNAL_SERVER_ERROR: 500,
    NOT_FOUND: 404,
    FORBIDDEN: 403,
    NO_CONTENT: 204
}

const SignUp = async(req,res) => {
    // if email and password are invalid  throw the errors
    const errors = validationResult(req).array();
    if (errors && errors.length) {
    console.log(errors);
    res.status(400).json({ errors });
    }else{ 
        try {
            //check for duplicate usernames in the db 
            
            //if (duplicate) return res.sendStatus(statusCode.CONFLICT)
            const {email,pwd} = req.body
            //encrypt the password 
            const saltRounds = 10 // num of hashing
            const hashedPwd = await bcrypt.hash(pwd, saltRounds);
            //create and store the new user
            await sequelize.sync()
            const result = await User.create({
                email: email,
                encryptedPassword: hashedPwd
            })
            console.log(result);
            res.status(statusCode.CREATED).json({'success' :`New user ${email} created!`});
        } catch (err){
            res.status(statusCode.INTERNAL_SERVER_ERROR).json({'message': err.message});
        } 
    }     
} 
   
const HandleLogin = async(req,res) =>{
    const {email,pwd} = req.body
    
    // check if user exist 
    const foundUser = await User.findOne({where: {email: email}})
    if (!foundUser) return res.sendStatus(statusCode.NOT_FOUND)

    const match = await bcrypt.compare(pwd,foundUser.password)
    if (match) {
        // create JWTs
        const accessToken = jwt.sign(
            {"email": foundUser.email},
            process.env.ACCESS_TOKEN_SECRET,
            { expiresIn: '30s'}
        )
        const refreshToken = jwt.sign(
            { "email": foundUser.email },

            process.env.REFRESH_TOKEN_SECRET,
            { expiresIn: '1d' }
        )
        // Saving refreshToken with current user
        foundUser.refreshToken = refreshToken
        const result = await foundUser.save()
        console.log(result)
    
        res.cookie('jwt', refreshToken, { httpOnly: true, sameSite: 'None', maxAge: 24 * 60 * 60 * 1000},); // secure: true only https
        res.json({accessToken});
    } else {
        res.sendStatus(statusCode.UNAUTHORIZED);
    }
}

const HandleRefreshToken = async(req,res) =>{
    const cookies = req.cookies
    if(!cookies?.jwt) return res.sendStatus(statusCode.UNAUTHORIZED)

    // Check refreshToken
    const refreshToken = cookies.jwt
    const foundUser = await User.findOne({where:{refreshToken: refreshToken}} )
    if(!foundUser) return res.sendStatus(statusCode.FORBIDDEN)
    jwt.verify(
        refreshToken,
        process.env.REFRESH_TOKEN_SECRET,
        (err,decoded) =>{
            if(err || foundUser.email !== decoded.email) return res.sendStatus(statusCode.FORBIDDEN)
            const accessToken = jwt.sign(
                {
                  "email": decoded.email,               
                },
                process.env.ACCESS_TOKEN_SECRET,
                {expiresIn: "30s"}
            )
            res.json({accessToken})
        },
    )
}

const HandleLogout =  async (req, res) => {
    // On client, also delete the accessToken
        

    // Is refreshToken in db?
    const foundUser = await User.findOne({where:{refreshToken: refreshToken}} )
    if (!foundUser) {
        res.clearCookie('jwt', { httpOnly: true});
        return res.sendStatus(statusCode.NO_CONTENT);
    }

    // Delete refreshToken in db
    foundUser.refreshToken = '';
    const result = await foundUser.save();
    console.log(result);

    res.clearCookie('jwt', { httpOnly: true}); // secure: true - only serves on https
    res.sendStatus(statusCode.NO_CONTENT);
}

const DeleteUser = async(req,res) =>{

    const{email,pwd} =req.body
    const foundUser = await User.findOne({where: {email: email}})
    if (!foundUser) {
        res.clearCookie('jwt', { httpOnly: true});
        return res.sendStatus(statusCode.NO_CONTENT);
    }
    try{
        const password = await bcrypt.compare(pwd,foundUser.encryptedPassword)
        if(password){
            const result = await User.destroy({where:{email: email}})
            console.log(result)
            return res.sendStatus(statusCode.OK)
        }
    }catch(err){
        console.log(err)
    }   
    res.clearCookie('jwt', { httpOnly: true});  
}


module.exports ={SignUp,
    HandleLogin,
    HandleRefreshToken,
    HandleLogout,
    DeleteUser
}




