'use strict';
const {
  Model
} = require('sequelize');
module.exports = (sequelize, DataTypes) => {
  class voting extends Model {
    /**
     * Helper method for defining associations.
     * This method is not a part of Sequelize lifecycle.
     * The `models/index` file will call this method automatically.
     */
    static associate(models) {
      // define association here
    }
  }
  voting.init({
    charname: DataTypes.STRING,
    votes: {
      type: DataTypes.INTEGER,
      allowNull: false,
      get(){
        return this.getDataValue('votes')
      }
    }
  }, {
    sequelize,
    modelName: 'voting',
  });
  return voting;
};