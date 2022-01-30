# Learning How to Code Generative Images
Took below course and summarized essentials.

- [HashLips NFT - Create 10000 generative NFTs](https://www.youtube.com/watch?v=vFY_E3IP6OU&list=PLvfQp12V0hS1PWDxlrfASk0Mq6AbC5n5f&index=1)

Note that this tutorial is based on original [release version 1.0.1.](https://github.com/HashLips/hashlips_art_engine/releases/tag/v1.0.0_update)

## Original release version 1.0.0
> In this release, users can add their images (layers) in the layers folder and generate multiple different instances based on the combination of the image layers. Metadata (json) is also created for each image so that it will fit with the NFT standards.

## Usage
The usage of HashLips is very simple. Only two things to bear in mind : layer order and edition size. The layer order **should be the same** with your layers folder structure. 

```js 
// should be matched with layer folder order. 
const layersOrder = [
  { name : "background"}, 
  { name : "paw"}, 
];
```

<img src="../reference/layer-order.png" height=429 width=247 alt="layer folder structure" />

And then decide the number of images you are going to create. 

```js
// decide how many images to create. 
// run node command to execute and then check build folder for result. 
const editionSize = 10;
```

## Layer configuration
In HashLips version 1.0.2, layer configuration is added. Stacked layered by object now will be differentiated and will create the number of editions it is assigned to. 

```js
const layerConfigurations = [
  {
    growEditionSizeTo: 5,
    layersOrder: [
      { name: "background" , opacity : 0.5 },
      { name: "edge", blend : MODE.color },
      { name: "paw" },
    ],
  },
  {
    // Total number of eiditions are now 10.
    growEditionSizeTo: 10,
    layersOrder: [
      { name: "background" , opacity : 0.5 },
      { name: "edge" },
      { name: "fish" },
    ],
  },
];

```

## Adding rarity
Adjust rarity by changing layer asset image file name. For example, from "BlackRect.png" to ""BlackRect(rarity unit)80.png".

The rarity unit by default is set to asterisk. If your OS is Window, which does not support the symbol as file name, should be replaced to something else. 

```js
const getRarityWeight = (_str) => {
  let nameWithoutExtension = _str.slice(0, -4);
  console.log(_str.slice(0,-4))
  // changed symbol here
  var nameWithoutWeight = Number(nameWithoutExtension.split(/[@ ]+/).pop());
  if (isNaN(nameWithoutWeight)) {
    nameWithoutWeight = 0;
  }
  return nameWithoutWeight;
};

const cleanName = (_str) => {
  let nameWithoutExtension = _str.slice(0, -4);
  // changed symbol here
  var nameWithoutWeight = nameWithoutExtension.split(/[@ ]+/).shift();
  return nameWithoutWeight;
};
```

## Updating Base URI for future IPFS uploading
In version 1.0.4, there had been added utility function that updates JSON file's meta data. For example, 

```js 
// update this url then changes will be reflected to all json files. 
const baseUri = "ipfs://QmNfPMWLPTEbFpBtPFy4wkYEHRVWcz8dzjziTcPbebzF53";
```

Change above baseUri with below updateBaseUri.js. 

```js 
"use strict";

const fs = require("fs");
const path = require("path");
const isLocal = typeof process.pkg === "undefined";
const basePath = isLocal ? process.cwd() : path.dirname(process.execPath);

const { baseUri } = require("../src/config.js");

// read json data
let rawdata = fs.readFileSync(`${basePath}/build/json/_metadata.json`);
let data = JSON.parse(rawdata);

data.forEach((item) => {
  item.image = `${baseUri}/${item.edition}.png`;
  fs.writeFileSync(
    `${basePath}/build/json/${item.edition}.json`,
    JSON.stringify(item, null, 2)
  );
});

fs.writeFileSync(
  `${basePath}/build/json/_metadata.json`,
  JSON.stringify(data, null, 2)
);

console.log(`Updated baseUri for images to ===> ${baseUri}`);

```

## Reference
- [HashLips release](https://github.com/HashLips/hashlips_art_engine/releases?page=2)