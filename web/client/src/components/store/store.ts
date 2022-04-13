import { writable } from "svelte/store";

export const user = writable({
    isLogin : false, 
    username : 'NoID@gmail.com' 
})

export const storeVoteCounts = writable({
    garfield: 70, 
    fishCat: 222, 
    tricolor: 100, 
})