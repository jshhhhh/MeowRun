<script lang="ts">
import Navigation from "./Navigation.svelte";
import Footer from "./Footer.svelte";
</script>


<main>
<Navigation />
<slot name="1_Main_"></slot>
<slot name="2_About_"></slot>
<slot name="3_WhyMeowRun_"></slot>
<slot name="4_How_it_works_"></slot>
<slot name="5_CharacterVote_"></slot>
<slot name="6_LoginForm_"></slot>

<slot name="temp"></slot>
<Footer />
</main>


<style lang="scss">

</style>