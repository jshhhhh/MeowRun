import Web3 from "web3";
import Transaction from "@ethereumjs/tx";
import { web3Key, dummies } from "../../config/key.js";

// provide ethereum testnet ropsten
const web3 = new Web3(web3Key.INFURA.ROPSTEN)

// import dummy accounts
const account1 = dummies.account1.address
const account2 = dummies.account2.address

// const privateky1 = Buffer.from(account1.privateKey)
// const privateky2 = Buffer.from(account2.privateKey)

web3.eth.getBalance(account1, (err, bal) => {
    console.log(web3.utils.fromWei(bal, 'ether'))
})

// build transaction
const txObject = {
    to : account2,
    value : , 
    gasLimit : , // transaction commission threshold
    gasPrice : 
}

// sign transaction


// broadcast transaction
web3.eth.sendSignedTransaction(raw, (err, txhash) => {
    console.log("txhash: ",txhash)
})