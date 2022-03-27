// controller for login 
import User from "../models/M_User.js"
import bcrypt from "bcrypt"


const StatusCode = {
    OK : 200, 
    CREATED : 201, 
    UNAUTHORIZED : 401,
    BADREQUEST : 400,
    CONFLICT : 409,
    INTERNELERROR: 500
}

const UserInfo = async(req,res) => {
    const {user,pwd,email} = req.body
    if (!user || !pwd || !email) return res.status(StatusCode.BADREQUEST).json({'message': 'user, pwd and email are required'})

    // check for duplicate usernames in the db 
    const Duplicate = await User.findOne({ username: user}).exec();
    if (Duplicate) return res.sendStatus(StatusCode.CONFLICT); //Conflict
    try{
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
    
    const IdCheck = await User.findOne({username: user}).exec()
    if (!IdCheck) return res.sendStatus(404);

    const match = await bcrypt.compare(pwd,IdCheck.password)

}







export {
    UserInfo
}







