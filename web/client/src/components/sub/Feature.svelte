<script lang="ts">
export let iconSource;
export let title;
export let paragraph;
</script>


<main id="feature">
<div id="icon">
    <img src={iconSource} alt="icon">
</div>
<div id="texts">
    <span id="title">{title}</span>
    <p id="paragraph">{paragraph}</p>
</div>
</main>


<style lang="scss">

</style>