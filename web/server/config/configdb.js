const {Sequelize} = require('sequelize')


const sequelize = new Sequelize('c6lx5nfj2q3hbcep', 'o9ixfskzdri04c45', 'tom95wy3vbs9pi08', {
    host: 'klbcedmmqp7w17ik.cbetxkdyhwsb.us-east-1.rds.amazonaws.com',
    dialect: 'mariadb'
  });


module.exports = sequelize