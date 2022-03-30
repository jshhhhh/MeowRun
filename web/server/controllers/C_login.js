// controller for login 
import User from "../models/M_User.js"
import bcrypt from "bcrypt"
import jwt from "jsonwebtoken"


const StatusCode = {
    OK : 200, 
    CREATED : 201, 
    UNAUTHORIZED : 401,
    BADREQUEST : 400,
    CONFLICT : 409,
    INTERNELERROR: 500,
    NOTFOUND: 404
}

const UserInfo = async(req,res) => {
    const {user,pwd,email} = req.body
    if (!user || !pwd || !email) return res.status(StatusCode.BADREQUEST).json({'message': 'user, pwd and email are required'})

    // check for duplicate usernames in the db 
    const Duplicate = await User.findOne({ username: user}).exec();
    if (Duplicate) return res.sendStatus(StatusCode.CONFLICT); //Conflict
    try {
        //encrypt the password 
        const saltRounds = 10 // num of hashing
        const hashedPwd = await bcrypt.hash(pwd, saltRounds);
        //create and store the new user
        const result = await User.create({
             "username": user, 
             "password": hashedPwd,
             "email"   : email
        });

        console.log(result);
        res.status(StatusCode.CREATED).json({'success' :`New user ${user} created!`});
    } catch (err){
        res.status(StatusCode.INTERNELERROR).json({'message': err.message});
    }
}

const HandleLogin = async(req,res) =>{
    const {user,pwd} = req.body
    
    // check if user exist 
    const UserCheck = await User.findOne({username: user}).exec()
    if (!UserCheck) return res.sendStatus(StatusCode.NOTFOUND);

    const match = await bcrypt.compare(pwd,UserCheck.password)
    if (match) {
        // create JWTs
        const accessToken = jwt.sign(
            { "username": UserCheck.username,
              "email": UserCheck.email},
            process.env.ACCESS_TOKEN_SECRET,
            { expiresIn: '30s' }
        );
        const refreshToken = jwt.sign(
            { "username": UserCheck.username,
              "email": UserCheck.email },
            process.env.REFRESH_TOKEN_SECRET,
            { expiresIn: '1d' }
        );
        // Saving refreshToken with current user
        UserCheck.refreshToken = refreshToken;
        const result = await UserCheck.save();
        console.log(result);
        
        res.cookie('jwt', refreshToken, { httpOnly: true, sameSite: 'None', maxAge: 24 * 60 * 60 * 1000}); // secure: true
        res.json({accessToken});
    } else {
        res.sendStatus(StatusCode.UNAUTHORIZED);
    }
}




    









export {
    UserInfo,
    HandleLogin
}




