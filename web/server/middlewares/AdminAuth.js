const User = require('../models/M_User.js')
const bcrypt = require('bcrypt')
const dotenv =require('dotenv')
dotenv.config()

const auth = {
  authenticate: async (email, password) => {
    const user = await User.findOne({ email })
    if (user) {
      const matched = await bcrypt.compare(password, user.encryptedPassword)
      if (matched) {
        return user
      }
    }
    return false
  },
  cookieName: 'adminBro',
  cookiePassword: process.env.COOKIE_SECRET
}

module.exports = auth