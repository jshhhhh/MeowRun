<script lang="ts">
import Feature from "../sub/Feature.svelte";
import PlayButton from "../sub/PlayButton.svelte";


</script>


<main id="whyMeowRun">
<h1>Why play MeowRun</h1>

<div id="features">
    <Feature 
        iconSource=""
        title="Easy to play"
        paragraph="Lorem ipsum dolor sit amet consectetur, adipisicing elit. Et, voluptatum?" />
    <Feature 
        iconSource=""
        title="Easy to play"
        paragraph="Lorem ipsum dolor sit amet consectetur, adipisicing elit. Et, voluptatum?" />
    <Feature 
        iconSource=""
        title="Easy to play"
        paragraph="Lorem ipsum dolor sit amet consectetur, adipisicing elit. Et, voluptatum?" />
    <Feature 
        iconSource=""
        title="Easy to play"
        paragraph="Lorem ipsum dolor sit amet consectetur, adipisicing elit. Et, voluptatum?" />
    <Feature 
        iconSource=""
        title="Easy to play"
        paragraph="Lorem ipsum dolor sit amet consectetur, adipisicing elit. Et, voluptatum?" />
    <Feature 
        iconSource=""
        title="Easy to play"
        paragraph="Lorem ipsum dolor sit amet consectetur, adipisicing elit. Et, voluptatum?" />
    <PlayButton />
</div>
</main>


<style lang="scss">
    @import '../partials/common.scss';
    h1 {
        text-align: center;
    }
    #features {
        display: grid;
        grid-template-columns: repeat(3, 1fr);
        grid-template-rows: repeat(2 1fr);
        gap: $unit*2;
        padding: $unit;
    }
</style>