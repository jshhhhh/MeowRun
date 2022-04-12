import { writable } from "svelte/store";

export const tempStoreVariable = writable(0)
export const username = writable('')

// TO DO: add stores for Character votes: Garfield, FishCat, and Tricolor