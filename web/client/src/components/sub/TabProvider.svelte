<script lang="ts">
import { createEventDispatcher } from "svelte";
import LoginForm from "./LoginForm.svelte";
import UserProfile from "./UserProfile.svelte";

export let tabList; // props
export let current; // props

let selected = current;
const dispatch = createEventDispatcher() // custom event
// const hanldeClick = () => {
//     dispatch('tabChange', selected)
// }
</script>

<main>
    <div id="tabTitles">
        {#each tabList as tab}
            <li 
                on:click={() => { selected = tab }} 
                class:active={ selected === tab }>
                {tab} &#11167;
            </li>
        {/each}
    </div>

    {#if selected.includes("Login")}
        <LoginForm />
        {:else}
        <UserProfile />
    {/if}
</main>

<style lang="scss">
    @import '../partials/common.scss';
    #tabTitles { 
        @include gridTwoColumns();
        text-align: center;
        background-color: $tabBgColor;
        color: white;
        font-size: $unit *2;
        font-weight: bold;
        padding: $unit;
        cursor: pointer;
    }
    .active { background-color: $subBgColor;}
</style>