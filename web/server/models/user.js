'use strict';
const {
  Model,DataTypes
} = require('sequelize');
module.exports = (sequelize) => {
  class User extends Model {
    /**
     * Helper method for defining associations.
     * This method is not a part of Sequelize lifecycle.
     * The `models/index` file will call this method automatically.
     */
    static associate(models) {
      // define association here
    }
  }
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
    }, {
      sequelize,
      modelName: 'User',
    });
  return User;
};