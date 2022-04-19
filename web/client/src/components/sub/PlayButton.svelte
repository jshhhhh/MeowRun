<script lang="ts">
// set props
export let buttonText;
export let jumpTo;
export let isTransparent = true;
export let callback = function(){};
</script>


<main id="playButton">
    {#if isTransparent}
        <a href={`#${jumpTo}`} on:click={()=>callback()}><button id="transparent">{buttonText}</button></a>
        {:else}
        <a href={`#${jumpTo}`} on:click={()=>callback()}><button id="hasBackground">{buttonText}</button></a>
    {/if}
</main>



<style lang="scss">
    @import '../partials/common.scss';
    #playButton {
        #transparent {
            font-size : $unit;
            padding : $unit;
            border-image : linear-gradient(to right, #1B51F2 0%, #F263DA 100%);
            border-image-slice : 4;
            border-image-width : $unit *0.2;
            background-color: transparent;
            color: white;
            font-weight: bold;
            cursor: pointer;
        }
        #hasBackground {
            font-size : $unit;
            padding : $unit;
            background-image : linear-gradient(to right, #1B51F2 0%, #F263DA 100%);
            border-image : linear-gradient(to right, #1B51F2 0%, #F263DA 100%);
            border-image-slice : 4;
            border-image-width : $unit *0.2;
            color: white;
            font-weight: bold;
            cursor: pointer;
        }
    }
</style>