const User = require('../models/M_User.js')

const options = {
    resources: [{
      resource: User,
      options: {
        properties: {
          encryptedPassword: {
            isVisible: false,
          },
          password: {
            type: 'string',
            isVisible: {
              list: false, edit: true, filter: false, show: false,
            },
          },
        },
        actions: {
          new: {
            before: async (request) => {
              if(request.payload.record.password) {
                request.payload.record = {
                  ...request.payload.record,
                  encryptedPassword: await bcrypt.hash(request.payload.record.password, 10),
                  password: undefined,
                }
              }
              return request
            },
          }
        }
      }
    }],
    rootPath: '/admin',
  }


module.exports = options