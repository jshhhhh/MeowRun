<script lang="ts">
export let title; 
export let paragraphs;
export let imageSource;
export let isImage = true;
export let shouldImageComeFirst = false;
</script>


<main id="gridContent">
    <div id={shouldImageComeFirst && "textsLater"}>
        <h1>{title}</h1>
        <p>{paragraphs}</p>
    </div>
    <div id={shouldImageComeFirst && "imageFirst"}>
        {#if isImage}
            <img src={imageSource} alt="grid content" />  
            {:else}
            <iframe 
                src="https://www.youtube.com/embed/UprcpdwuwCg" 
                title="MeowRun Play" 
                frameborder="0" 
                allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" 
                allowfullscreen></iframe>
        {/if}
    </div>
</main>


<style lang="scss">
    @import '../partials/common.scss';

    #gridContent {
        // grid default: left text, right image
        @include gridTwoColumns();
        justify-content: center;
        align-items: center;
        margin: 0 auto;
        padding: $unit;
        // variation: when image should come first
        #textsLater { 
        order: 1;
        }
        #imageFirst {
            order: 0;
        }
    }
</style>