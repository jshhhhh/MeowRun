const User = require('../models/user.js')
const bcrypt = require('bcrypt')
const dotenv =require('dotenv')
dotenv.config()

const auth = {
  authenticate: async (email, password) => {
    const user = await User.findOne({where:{ email}})
    if (user) {
      const matched = await bcrypt.compare(password, user.encryptedPassword)
      if (matched && user.role == 'admin') {
        return user
      }
    }
    return false
  },
  cookieName: 'adminBro',
  cookiePassword: process.env.COOKIE_SECRET
}

module.exports = auth