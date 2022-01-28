# Learning How to Code Generative Images
Took below course and summarized essentials.

- [HashLips NFT - Create 10000 generative NFTs](https://www.youtube.com/watch?v=vFY_E3IP6OU&list=PLvfQp12V0hS1PWDxlrfASk0Mq6AbC5n5f&index=1)

Note that this tutorial is based on original [release version 1.0.1.](https://github.com/HashLips/hashlips_art_engine/releases/tag/v1.0.0_update)

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

### Adding rarity
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