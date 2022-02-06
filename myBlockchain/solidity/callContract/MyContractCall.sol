// SPDX-License-Identifier: MIT
pragma solidity ^0.8.10;

contract MyContractCall { 
    uint256 public age = 29; 

    function getAge() public view returns(uint256) {
        return age;
    }
}

// If contract address known, and function/state in the contract
// are public, you can call it in your contract
contract MyAnotherContract {
    function getAgeFromAboveContract(address _contractAddress) public view returns(uint256) {
        // create a contract instance : (Contract) (instance name)
        MyContractCall importedContract = MyContractCall(_contractAddress);

        uint256 age = importedContract.getAge();
        return age;
    }
}