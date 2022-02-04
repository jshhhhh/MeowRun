// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;

contract MyStruct {
    // Struct is a custom data type
    struct NFT {
        string name;
        uint256 dna;
    }

    mapping(uint256 => NFT) nftMapping;
    NFT[] public nftList;

    // use memory type for intermediate calculation(costing less gas)
    // and use storage type for final result
    function addNFT(string memory _name, uint256 _dna) public {

        // instantiation method 1
        NFT memory newNFT = NFT(_name, _dna);

        // instantiation method 2
        // newNFT.name = _name;
        // newNFT.dna = _dna;
        nftList.push(newNFT);
    }

    function getNFTname(uint256 _index) public view returns (string memory) {
        return nftList[_index].name;
    }
}