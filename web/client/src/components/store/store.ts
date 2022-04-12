import { writable } from "svelte/store";

export const username = writable('')
export const storeVoteCount = writable(0)

// TO DO: add stores for Character votes: Garfield, FishCat, and Tricolor