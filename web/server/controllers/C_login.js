// controller for login 
const bcrypt =require("bcrypt")
const jwt =require ("jsonwebtoken")
const { validationResult}=require("express-validator")



const dotenv    = require('dotenv')
dotenv.config()

const model = require('../models')
const sequelize =require('sequelize')
// const DataTypes = sequelize.DataTypes
// let sequelize = db.sequelize
// const DataTypes = sequelize.DataTypes;





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
            await model.sequelize.sync()
        
            const {email,pwd,role} = req.body
            const emailcheck = await model.User.findOne({
                where: {
                    email: email
                }
            })
            if(emailcheck) return res.status(400).json({"err":'email already exists'})
            //encrypt the password
            const saltRounds = 10 // num of hashing
            const hashedPwd = await bcrypt.hash(pwd, saltRounds);
            //create and store the new user
            const result = await model.User.create({
                email: email,
                encryptedPassword: hashedPwd,
                role: role
            })
            console.log(result);
            res.status(statusCode.CREATED).json({'success' :`New user ${email} created!`});
        } catch (err){
            res.status(statusCode.INTERNAL_SERVER_ERROR)
                .json({'type':'auth',
                       'message': err.message});
        } 
    }     
} 
   
const HandleLogin = async(req,res) =>{
    const {email,pwd} = req.body
    
    // check if user exist 
    const foundUser = await model.User.findOne({where:{email: email}})
    if (!foundUser) return res.status(statusCode.NOT_FOUND).json({"message": "The email is not exist"})

    const match = await bcrypt.compare(pwd,foundUser.encryptedPassword)
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
    
        res.cookie('jwt', refreshToken, { httpOnly: true, sameSite: 'None', maxAge: 24 * 60 * 60 * 1000},); // secure:true only https
        res.json({accessToken});
    } else {
        res.status(statusCode.UNAUTHORIZED).json({"message":"email or password is not correct"});
    }
}

const HandleRefreshToken = async(req,res) =>{
    const cookies = req.cookies
    if(!cookies?.jwt) return res.sendStatus(statusCode.UNAUTHORIZED)

    // Check refreshToken
    const refreshToken = cookies.jwt
    const foundUser = await model.User.findOne({where:{refreshToken:refreshToken}} )
    if(!foundUser) return res.status(statusCode.FORBIDDEN).json({"message":"authentication error"})
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
        
    const cookies = req.cookies
    if (!cookies?.jwt) return res.sendStatus(statusCode.NO_CONTENT); 
    const refreshToken = cookies.jwt;

    // Is refreshToken in db?
    const foundUser = await model.User.findOne({ refreshToken })
    if (!foundUser) {
        res.clearCookie('jwt', { httpOnly: true,secure:true});
        return res.sendStatus(statusCode.NO_CONTENT);
    }

    // Delete refreshToken in db
    foundUser.refreshToken = '';
    const result = await foundUser.save();
    console.log(result);

    res.clearCookie('jwt', { httpOnly: true,secure:true}); // secure: true - only serves on https
    res.sendStatus(statusCode.NO_CONTENT);
}

const DeleteUser = async(req,res) =>{

    const{email,pwd} = req.body
     
    try{
        const foundUser = await model.User.findOne({where:{email:email}} )
        if (!foundUser) return res.status(statusCode.NOT_FOUND).json({'message': 'ID is not exist.'})
        const pwdCheck = bcrypt.compare(pwd,foundUser.encryptedPassword)
        if (pwdCheck) {
        //res.clearCookie('jwt', { httpOnly: true,});
            const result = await model.User.destroy({where:{email:email}})
            console.log(result)
            return res.status(statusCode.OK).json({'message':'Successfully deleted.'});
        }else{
            return res.status(statusCode.BAD_REQUEST).json({'message':'Please check your password.'})
    }
    }catch(err){
    console.log(err)
    }   
    //res.clearCookie('jwt', { httpOnly: true,});  
}



module.exports ={SignUp,
    HandleLogin,
    HandleRefreshToken,
    HandleLogout,
    DeleteUser
}




