// mongoose model for user 
import mongoose  from 'mongoose';

const Schema = mongoose.Schema;




const userSchema = new Schema({
    email:{
        type: String,
        required: true,
        minlength:16,
        maxlength:30
    },

    roles: {
        User: {
            type: Number,
            default: 2001
        },
        Editor: Number,
        Admin: Number
    },

    password: {
        type: String,
        required: true,
        trim: true,
        minlength: 8    
    },    
    
    refreshToken: String
});

const User = mongoose.model('User',userSchema);
User.collection.createIndex( { email: 1 }, { unique: true } )

export default User
