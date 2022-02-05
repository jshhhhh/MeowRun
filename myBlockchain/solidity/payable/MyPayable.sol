// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;

contract MyPayable {
    address payable public myAddress; 

    constructor() payable {
        myAddress = payable(msg.sender);
    }

    function deposit() public payable {

    }

    function getBalance() public view returns(uint256) {
        uint256 amount = address(myAddress).balance ;
        return amount;
    }
}