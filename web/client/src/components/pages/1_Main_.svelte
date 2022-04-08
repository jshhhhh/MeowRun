<script lang="ts">
import GridContent from "../sub/GridContent.svelte";
import PlayButton from "../sub/PlayButton.svelte";

</script>


<main id="main">
    <GridContent 
    title={["Run into wildness.", "Jump with heart"]}
    paragraphs="
    A piece of great game is a great bread for our soul - if a human is destined to work all day without any rest,
    that would be a mean life, truly. Your body needs rest, so is your brain. MeowRun is a great game for you to
    refresh yourself by playing around with cute cat character. 
    "
    imageSource="/banner/mainBanner.png">

    <div slot="buttons" id="buttons">
        <PlayButton 
            buttonText="Play MeowRun"
            jumpTo="playMeowRun"/>
        <PlayButton
            buttonText="Sign Up"
            jumpTo="loginPage"/>
    </div>
    </GridContent>
</main>


<style lang="scss">
@import '../partials/common.scss';

#buttons {
    @include flexRow();
    gap: $unit*1.2;
    // grid-row-start: 0;
}
</style>