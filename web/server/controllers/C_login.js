// controller for login 
import User from "../models/M_User.js"
import bcrypt from "bcrypt"

const UserInfo = async(req,res) => {
    const {user,pwd,email} = req.body
    if (!user || !pwd || !email) return res.status(400).json({'message': 'user, pwd and email are required'})

    // check for duplicate usernames in the db 
    const duplicate = await User.findOne({ username: user}).exec();
    if (duplicate) return res.sendStatus(409); //Conflict
    try{
        //encrypt the password 
        const hashedPwd = await bcrypt.hash(pwd, 10);
        //create and store the new user
        const result = await User.create({
             "username": user, 
             "password": hashedPwd,
             "email"   : email
        });

        console.log(result);
        res.status(201).json({'success' :`New user ${user} created!`});
    } catch (err){
        res.status(500).json({'message': err.message});
    }
}

const HandleLogin = async(req,res) =>{
    const {user,pwd} = req.body
    
    const IdCheck = await User.findOne({username: user}).exec()
    if (!IdCheck) return res.sendStatus(404);
}







export {
    UserInfo
}




