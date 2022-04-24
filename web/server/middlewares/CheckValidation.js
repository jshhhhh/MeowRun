const {check} = require('express-validator')


const CheckValidation = [
    check('email')
        .exists()
        .withMessage('please enter the email')
        .isEmail()
        .withMessage('email must follow this form  ex) example.example.com')
        .isLength({min:15,max:30}),

    check('pwd')
        .exists()
        .withMessage('please enter the password')
        .isLength({min:8})
        .withMessage('password is must be above eight letters')
]   
module.exports = CheckValidation
