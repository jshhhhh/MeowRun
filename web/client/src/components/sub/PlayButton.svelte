<script lang="ts">
// set props
export let buttonText;
export let jumpTo;
</script>


<main id="playButton">
    <a href={`#${jumpTo}`}><button>{buttonText}</button></a>
</main>


<style lang="scss">
    @import '../partials/common.scss';
    #playButton {
        button {
            font-size : $unit;
            padding : $unit;
            border-image : linear-gradient(to right, #1B51F2 0%, #F263DA 100%);
            border-image-slice : 4;
            border-image-width : 0.2rem;
            background-color: transparent;
            color: white;
            font-weight: bold;
            cursor: pointer;
        }
    }
</style>