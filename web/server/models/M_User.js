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
        minlength: 8    
    },

    email:{
        type: String,
        required: true,
        minlength: 10
    }
    
});

const User = mongoose.model('User',userSchema);

export default User