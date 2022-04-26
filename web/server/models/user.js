const {Model,DataTypes} = require('sequelize')
const sequelize = require('../config/configdb.js')
// class User extends Model {}
// User.init({
//     username: DataTypes.STRING,
//     birthday: DataTypes.DATE
// }, { sequelize, modelName: 'userad'});

class User extends Model {}
User.init({
    email: {
        type: DataTypes.STRING,
        allowNull: false,    
    },
    encryptedPassword: {
        type: DataTypes.STRING,
        allowNull: false
    },
    role:{
        type: DataTypes.ENUM('admin','restricted'),
        defaultValue: 'restricted'
    },
    refreshToken: DataTypes.STRING
},{sequelize})

module.exports = User

