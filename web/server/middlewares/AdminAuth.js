const model = require('../models')
const bcrypt = require('bcrypt')
const dotenv =require('dotenv')
dotenv.config()

const auth = {
  authenticate: async (email, password) => {
    const user = await model.User.findOne({where:{ email}})
    if (user) {
      const matched = await bcrypt.compare(password, user.encryptedPassword)
      if (matched && user.role == 'admin') {
        return user
      }
    }
    return false
  },
  cookiePassword: process.env.COOKIE_SECRET
}

module.exports = auth