// controller for login 
const User = require("../models/M_User.js")
const bcrypt =require("bcrypt")
const jwt =require ("jsonwebtoken")
const { validationResult}=require("express-validator")


const StatusCode = {
    OK : 200, 
    CREATED : 201, 
    UNAUTHORIZED : 401,
    BAD_REQUEST : 400,
    CONFLICT : 409,
    INTERNAL_SERVER_ERROR: 500,
    NOT_FOUND: 404,
    FORBIDDEN: 403
}

const SignUp = async(req,res) => {
    // if email and password are invalid  throw the errors
    const errors = validationResult(req).array();
    if (errors && errors.length) {
    console.log(errors);
    res.status(400).json({ errors });
    }else{ 
        const {email,pwd,role} = req.body
        // check for duplicate usernames in the db 
        const Duplicate = await User.findOne({ email: email}).exec();
        if (Duplicate) return res.sendStatus(StatusCode.CONFLICT); //Conflict
        try {
            //encrypt the password 
            const saltRounds = 10 // num of hashing
            const hashedPwd = await bcrypt.hash(pwd, saltRounds);
            //create and store the new user
            const result = await User.create({
                 "email"   : email,
                 "encryptedPassword": hashedPwd,
                 "roles" : role
            });
       
            console.log(result);
            res.status(StatusCode.CREATED).json({'success' :`New user ${email} created!`});
        } catch (err){
            res.status(StatusCode.INTERNAL_SERVER_ERROR).json({'message': err.message});
        } 
    }     
} 
     
const HandleLogin = async(req,res) =>{
    const {email,pwd} = req.body
    
    // check if user exist 
    const foundUser = await User.findOne({email : email}).exec()
    if (!foundUser) return res.sendStatus(StatusCode.NOT_FOUND)

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
    
        res.cookie('jwt', refreshToken, { httpOnly: true, sameSite: 'None', maxAge: 24 * 60 * 60 * 1000}); // secure: true
        res.json({accessToken});
    } else {
        res.sendStatus(StatusCode.UNAUTHORIZED);
    }
}

const HandleRefreshToken = async(req,res) =>{
    const cookies = req.cookies
    if(!cookies?.jwt) res.sendStatus(StatusCode.UNAUTHORIZED)

    // Check refreshToken
    const refreshToken = cookies.jwt
    const foundUser = await User.findOne({refreshToken} ).exec()
    if(!foundUser) res.sendStatus(StatusCode.FORBIDDEN)
    jwt.verify(
        refreshToken,
        process.env.REFRESH_TOKEN_SECRET,
        (err,decoded) =>{
            if(err || foundUser.email !== decoded.email) return res.sendStatus(StatusCode.FORBIDDEN)
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
    if (!cookies?.jwt) return res.sendStatus(204); //No content
    const refreshToken = cookies.jwt;

    // Is refreshToken in db?
    const foundUser = await User.findOne({ refreshToken }).exec();
    if (!foundUser) {
        res.clearCookie('jwt', { httpOnly: true});
        return res.sendStatus(204);
    }

    // Delete refreshToken in db
    foundUser.refreshToken = '';
    const result = await foundUser.save();
    console.log(result);

    res.clearCookie('jwt', { httpOnly: true}); // secure: true - only serves on https
    res.sendStatus(204);

}


module.exports ={SignUp,
    HandleLogin,
    HandleRefreshToken,
    HandleLogout
}




