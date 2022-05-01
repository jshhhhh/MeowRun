
const model = require('../models')
const statusCode = require('../config/statuscode.js')
const { sequelize } = require('../models')
const updateVotes = async(req,res) =>{

    const {charname,votes} = req.body
    // parameter check
    if(!(charname && votes)) return res.status(statusCode.BAD_REQUEST).json({'message':'please enter charname and votes'})
    // if votes is not number 
    if(typeof votes !== 'number') return res.status(statusCode.BAD_REQUEST).json({'message':'votes must be number'})
    await model.sequelize.sync()
    const result = await model.voting.create({
        charname: charname,
        votes: votes
    })
    res.status(statusCode.CREATED).json({'success' : 'voting  saved!'});
    console.log(result)   
}



const currentVotes = async(req,res) =>{
    try{
        const votes = await model.voting.findAll({
        attributes:['charname',[sequelize.fn('sum',sequelize.col('votes')),'votes']],
        raw:true,
        group: ['charname']
        })
        console.log(votes)
        res.status(statusCode.OK).json(votes)
    }catch(err){
        console.log(err)
        res.status(statusCode.NOT_FOUND).json({"message":err})
    }

}

module.exports ={updateVotes,currentVotes}