<script lang="ts">
export let iconSource;
export let title;
export let paragraph;
</script>


<main id="feature">
<div id="icon">
    <img src={iconSource} id="iconSource" alt="icon" loading='lazy' />
</div>
<div id="texts">
    <span id="title">{title}</span>
    <p id="paragraph">{paragraph}</p>
</div>
</main>


<style lang="scss">
    @import '../partials/common.scss';
    #feature {
        display: flex;
        flex-flow: column nowrap;
        padding: $unit;
        text-align: center;
        #iconSource {
            max-width: 25%;
            height: auto;
            border-radius: 50%;
        }
        #texts {
            #title {
                font-size: $unit *1.5;
            }
            #paragraph {
                text-align: left;
            }
        }
    }
</style>