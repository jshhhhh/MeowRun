import mongoose from "mongoose";

const ConnectDB = async() =>{
    try{
        await mongoose.connect(process.env.MONGO_URI)
    }catch(err){
        console.log(err);
    }
}

export {ConnectDB} 