<script lang="ts">
// set props
export let title: string | string[]; 
export let paragraphs;
export let imageSource="";
export let isImage = true;
export let videoSource = "";
export let shouldImageComeFirst = false;
</script>


<main id="gridContent">
    <div id={shouldImageComeFirst && "textsLater"}>
        <h1 id="title">
            {#if typeof title !== "string"}
                {#each title as line}
                    <p>{line}</p>
                {/each}
                {:else}
                {title}
            {/if}
        </h1>
        <p id="paragraphs">{paragraphs}</p>
    </div>
    <div id={shouldImageComeFirst && "imageFirst"}>
        {#if isImage}
            <img src={imageSource} id="imageSource" alt="grid content" loading='lazy'/>  
            {:else}
            <iframe 
                src={videoSource} 
                title="MeowRun Play" 
                width="600"
                height="400"
                frameborder="0" 
                allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" 
                allowfullscreen>
            </iframe>
        {/if}
    </div>
    <slot name="buttons"></slot>
</main>


<style lang="scss">
    @import '../partials/common.scss';

    #gridContent {
        // grid default: left text, right image
        @include gridTwoColumns();
        justify-content: center;
        align-items: center;
        margin: 0 auto;
        padding: $unit*4;
        text-align: center;
        #title {
            font-size: $unit*3;
            line-height: $unit;
            text-align: left;
        }
        #paragraphs {
            line-height: $unit*1.5;
            text-align: left;
        }
        img { 
            max-width: 75%;
            height: auto;
        }
        // variation: when image should come first
        #textsLater { 
            order: 1;
        }
        #imageFirst {
            order: 0;
        }
    }
</style>