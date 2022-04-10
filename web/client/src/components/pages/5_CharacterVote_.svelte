<script lang="ts">
import GridContent from "../sub/GridContent.svelte"
import PlayButton from "../sub/PlayButton.svelte";
import Vote from "../sub/Vote.svelte";

</script>


<main id="characterVote">
    <h1>Vote the coolest characters</h1>
    <Vote />
</main>


<style lang="scss">
    @import '../partials/common.scss';
    h1 {
        text-align: center;
        font-size: $unit *3;
    }
</style>