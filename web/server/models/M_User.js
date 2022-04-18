// // mongoose model for user 
 const mongoose = require( 'mongoose')

 const Schema = mongoose.Schema;




const userSchema = new Schema({
    email:{
        type: String,
        required: true,
        minlength:16,
        maxlength:30
    },

    roles: { type: String, enum:['admin', 'restricted'],required: true },

    encryptedPassword: {
        type: String,
        required: true,
        trim: true,
        minlength: 8    
    },    
    
    refreshToken: String
});

const User = mongoose.model('User',userSchema);
User.collection.createIndex( { email: 1 }, { unique: true } )

module.exports= User


