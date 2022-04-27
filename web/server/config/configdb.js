const {Sequelize} = require('sequelize')
const dotenv = require('dotenv')
dotenv.config()

//prod
// const sequelize = new Sequelize(process.env.NAME, process.env.USER, p, {
//     host: 'klbcedmmqp7w17ik.cbetxkdyhwsb.us-east-1.rds.amazonaws.com',
//     dialect: 'mariadb'
//   });

////develop
// const sequelize = new Sequelize(process.env.MNAME, process.env.MUSER, process.env.MPWD, {
//   host: 'localhost',
//   dialect: 'mariadb'
// });


module.exports = sequelize