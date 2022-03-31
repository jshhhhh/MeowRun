// mongoose model for user 
import mongoose  from 'mongoose';
const Schema = mongoose.Schema;

const userSchema = new Schema({
    username: {
        type: String,
        required: true
    },

    password: {
        type: String,
        required: true,
        trim: true,
        minlength: 8    
    },

    email:{
        type: String,
        required: true,
        minlength: 10
    },
    refreshToken: String
});

const User = mongoose.model('User',userSchema);
User.collection.createIndex( { email: 1 }, { unique: true } )

export default User
