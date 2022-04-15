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
    imageSource="https://mir-s3-cdn-cf.behance.net/project_modules/1400/6d77bb139168569.62313b59c7519.jpg">

    <div slot="buttons" id="buttons">
        <PlayButton 
            buttonText="Play MeowRun"
            jumpTo="playMeowRun"/>
        <PlayButton
            isTransparent={false}
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