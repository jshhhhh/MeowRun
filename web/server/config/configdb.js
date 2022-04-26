const {Sequelize} = require('sequelize')
const dotenv = require('dotenv')
dotenv.config()

//prod
// const sequelize = new Sequelize('c6lx5nfj2q3hbcep', 'o9ixfskzdri04c45', 'tom95wy3vbs9pi08', {
//     host: 'klbcedmmqp7w17ik.cbetxkdyhwsb.us-east-1.rds.amazonaws.com',
//     dialect: 'mariadb'
//   });

//develop
const sequelize = new Sequelize(process.env.MNAME, process.env.MUSER, process.env.MPWD, {
  host: 'localhost',
  dialect: 'mariadb'
});


module.exports = sequelize